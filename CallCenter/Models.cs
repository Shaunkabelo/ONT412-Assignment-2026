using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace CallCenterApp.Models
{
    public enum UserRole { Student, Technician, Manager }
    public enum PhoneDevice { Smartphone, CallCenterTelephone }

    // STATE: the context delegates all call behaviour to its current state.
    public interface ICallState
    {
        string Name { get; }
        bool CanSpeak { get; }
        void Hold(Call call);
        void Resume(Call call);
        void HangUp(Call call);
    }
    public sealed class OnCallState : ICallState
    {
        public string Name => "On call";
        public bool CanSpeak => true;
        public void Hold(Call call) => call.Transition(new OnHoldState());
        public void Resume(Call call) { }
        public void HangUp(Call call) => call.Transition(new HungUpState());
    }
    public sealed class OnHoldState : ICallState
    {
        public string Name => "On hold";
        public bool CanSpeak => false;
        public void Hold(Call call) { }
        public void Resume(Call call) => call.Transition(new OnCallState());
        public void HangUp(Call call) => call.Transition(new HungUpState());
    }
    public sealed class HungUpState : ICallState
    {
        public string Name => "Hung up";
        public bool CanSpeak => false;
        public void Hold(Call call) => throw new InvalidOperationException("This call has ended.");
        public void Resume(Call call) => throw new InvalidOperationException("Create a new call to reconnect.");
        public void HangUp(Call call) { }
    }
    public sealed class Call
    {
        private readonly Func<TimeSpan> clock;
        private TimeSpan startedSegment;
        private TimeSpan accumulated;
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Caller { get; }
        public string Receiver { get; }
        public UserRole Role { get; }
        public PhoneDevice Device { get; }
        public DateTime StartedAt { get; private set; } = DateTime.Now;
        public DateTime? EndedAt { get; private set; }
        public Guid? ReturnOf { get; }
        public ICallState State { get; private set; } = new OnCallState();
        public bool IsEnded => State is HungUpState;
        public TimeSpan Duration => accumulated + (State.CanSpeak ? clock() - startedSegment : TimeSpan.Zero);
        public Call(string caller, string receiver, UserRole role, PhoneDevice device,
            Guid? returnOf = null, Func<TimeSpan> clock = null)
        {
            if (string.IsNullOrWhiteSpace(caller) || string.IsNullOrWhiteSpace(receiver))
                throw new ArgumentException("Enter both the caller and receiver names or numbers.");
            Caller = caller.Trim(); Receiver = receiver.Trim(); Role = role; Device = device; ReturnOf = returnOf;
            var watch = Stopwatch.StartNew();
            this.clock = clock ?? (() => watch.Elapsed);
            startedSegment = this.clock();
        }
        internal void Transition(ICallState next)
        {
            if (State.CanSpeak) accumulated += clock() - startedSegment;
            State = next;
            if (next.CanSpeak) startedSegment = clock();
            if (next is HungUpState) EndedAt = DateTime.Now;
        }
        public string Speak() => State.CanSpeak ? "Speaking is allowed." : "Speaking is blocked while " + State.Name.ToLowerInvariant() + ".";
        public void Hold() => State.Hold(this);
        public void Resume() => State.Resume(this);
        public void HangUp() => State.HangUp(this);
        public CallRecord ToRecord() => new CallRecord { Id = Id, Caller = Caller, Receiver = Receiver,
            Role = Role, Device = Device, StartedAt = StartedAt, EndedAt = EndedAt,
            DurationTicks = Duration.Ticks, ReturnOf = ReturnOf };
        public static Call Restore(CallRecord r)
        {
            var call = new Call(r.Caller, r.Receiver, r.Role, r.Device, r.ReturnOf);
            call.Id = r.Id; call.StartedAt = r.StartedAt; call.EndedAt = r.EndedAt;
            call.accumulated = TimeSpan.FromTicks(r.DurationTicks); call.State = new HungUpState();
            return call;
        }
    }
    public sealed class CallRecord
    {
        public Guid Id { get; set; }
        public string Caller { get; set; }
        public string Receiver { get; set; }
        public UserRole Role { get; set; }
        public PhoneDevice Device { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public long DurationTicks { get; set; }
        public Guid? ReturnOf { get; set; }
    }
    // COMPOSITE: a leaf and a group expose the same recursive traversal operation.
    public abstract class CallLogComponent
    {
        public string Name { get; }
        protected CallLogComponent(string name) { Name = name; }
        public abstract IEnumerable<SingleCall> GetCalls();
    }
    public sealed class SingleCall : CallLogComponent
    {
        public Call Call { get; }
        public SingleCall(Call call) : base(call.Caller) { Call = call; }
        public override IEnumerable<SingleCall> GetCalls() { yield return this; }
        public override string ToString() => $"{Call.StartedAt:MM-dd HH:mm} | {Call.Caller} → {Call.Receiver} | {Call.Role} | {Call.State.Name} | {Call.Duration:hh\\:mm\\:ss} | {Call.Id.ToString().Substring(0, 8)}";
    }
    public sealed class CallGroup : CallLogComponent
    {
        private readonly List<CallLogComponent> children = new List<CallLogComponent>();
        public IReadOnlyList<CallLogComponent> Children => children.AsReadOnly();
        public CallGroup(string name) : base(name) { }
        public void Add(CallLogComponent child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));
            if (Contains(child, this)) throw new InvalidOperationException("A group cannot contain itself or an ancestor.");
            if (!children.Contains(child)) children.Add(child);
        }
        private static bool Contains(CallLogComponent item, CallLogComponent target) =>
            ReferenceEquals(item, target) || (item is CallGroup group && group.Children.Any(c => Contains(c, target)));
        public override IEnumerable<SingleCall> GetCalls() => children.SelectMany(c => c.GetCalls());
    }
    // ITERATOR: snapshot navigation keeps the UI independent of the tree structure.
    public interface ICallIterator
    {
        void First();
        void Next();
        void Previous();
        bool IsDone();
        SingleCall CurrentItem();
    }
    public sealed class CallLogIterator : ICallIterator
    {
        private readonly List<SingleCall> items;
        private int position;
        public CallLogIterator(IEnumerable<SingleCall> calls) { items = calls.ToList(); }
        public void First() => position = 0;
        public void Next() { if (position < items.Count) position++; }
        public void Previous() { if (position > 0) position--; }
        public bool IsDone() => position >= items.Count;
        public SingleCall CurrentItem() => IsDone() ? null : items[position];
    }
    public sealed class CallLog
    {
        public CallGroup Root { get; } = new CallGroup("All calls");
        private readonly Dictionary<UserRole, CallGroup> groups = new Dictionary<UserRole, CallGroup>();
        public CallLog()
        {
            foreach (UserRole role in Enum.GetValues(typeof(UserRole)))
            { var group = new CallGroup(role + " calls"); groups.Add(role, group); Root.Add(group); }
        }
        public SingleCall Add(Call call)
        {
            var item = new SingleCall(call); groups[call.Role].Add(item); return item;
        }
        public CallLogIterator CreateIterator(string query = "") => new CallLogIterator(Root.GetCalls().Where(c =>
            string.IsNullOrWhiteSpace(query) || (c.Call.Caller + " " + c.Call.Receiver + " " + c.Call.Id + " " + c.Call.Role)
            .IndexOf(query.Trim(), StringComparison.OrdinalIgnoreCase) >= 0));
    }
    public sealed class CallLogStore
    {
        private readonly string path;
        public CallLogStore(string path) { this.path = path; }
        public void Save(CallLog log)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(path)));
            var records = log.Root.GetCalls().Where(c => c.Call.IsEnded).Select(c => c.Call.ToRecord()).ToList();
            File.WriteAllText(path + ".tmp", JsonSerializer.Serialize(records, new JsonSerializerOptions { WriteIndented = true }));
            File.Move(path + ".tmp", path, true);
        }
        public void Load(CallLog log)
        {
            if (!File.Exists(path)) return;
            var records = JsonSerializer.Deserialize<List<CallRecord>>(File.ReadAllText(path))
                ?? throw new InvalidDataException("Call history is empty or invalid.");
            if (records.Any(r => r == null || r.EndedAt == null || r.DurationTicks < 0 ||
                string.IsNullOrWhiteSpace(r.Caller) || string.IsNullOrWhiteSpace(r.Receiver) ||
                !Enum.IsDefined(typeof(UserRole), r.Role) || !Enum.IsDefined(typeof(PhoneDevice), r.Device)) ||
                records.Select(r => r.Id).Distinct().Count() != records.Count)
                throw new InvalidDataException("Call history contains invalid records.");
            foreach (var record in records) log.Add(Call.Restore(record));
        }
    }
    // PROXY: both services implement one contract; the proxy guards manager callbacks.
    public interface ICallService
    {
        SingleCall MakeCall(string caller, string receiver, UserRole role, PhoneDevice device);
        SingleCall ReturnCall(SingleCall original, string manager, UserRole role, PhoneDevice device);
    }
    public sealed class RealCallService : ICallService
    {
        private readonly CallLog log;
        public RealCallService(CallLog log) { this.log = log; }
        public SingleCall MakeCall(string caller, string receiver, UserRole role, PhoneDevice device) =>
            log.Add(new Call(caller, receiver, role, device));
        public SingleCall ReturnCall(SingleCall original, string manager, UserRole role, PhoneDevice device) =>
            log.Add(new Call(manager, original.Call.Caller, role, device, original.Call.Id));
    }
    public sealed class CallProxy : ICallService
    {
        private readonly ICallService real;
        private readonly CallLog log;
        public CallProxy(CallLog log) { this.log = log; real = new RealCallService(log); }
        private void EnsureAvailable()
        {
            if (log.Root.GetCalls().Any(c => !c.Call.IsEnded))
                throw new InvalidOperationException("Hang up the active call first.");
        }
        public SingleCall MakeCall(string caller, string receiver, UserRole role, PhoneDevice device)
        { EnsureAvailable(); return real.MakeCall(caller, receiver, role, device); }
        public SingleCall ReturnCall(SingleCall original, string manager, UserRole role, PhoneDevice device)
        {
            if (role != UserRole.Manager) throw new UnauthorizedAccessException("Only the manager can return a call from the log.");
            if (original == null || !original.Call.IsEnded || !log.Root.GetCalls().Contains(original))
                throw new InvalidOperationException("Select a completed call from this log.");
            EnsureAvailable(); return real.ReturnCall(original, manager, role, device);
        }
    }
}

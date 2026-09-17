using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace CallCenterApp.Models
{
    public enum UserRole { Student, Technician, Manager }
    public enum PhoneDevice { Smartphone, CallCenterTelephone }

    // STATE Call is Context; StateBase defines behaviour.
    // State objects hold no per-call data; timing and identity stay in Call
    public abstract class StateBase
    {
        public abstract string Name { get; }
        public abstract bool CanSpeak { get; }
        public abstract bool CanHold { get; }
        public abstract bool CanResume { get; }
        public virtual bool IsEnded => false;
        public abstract string Speak(Call context);
        public abstract void Hold(Call context);
        public abstract void Resume(Call context);
        public abstract void HangUp(Call context);
    }
    public sealed class OnCallState : StateBase
    {
        public override string Name => "On call";
        public override bool CanSpeak => true;
        public override bool CanHold => true;
        public override bool CanResume => false;
        public override string Speak(Call context) => "Speaking is allowed.";
        public override void Hold(Call context) => context.Transition(new OnHoldState());
        public override void Resume(Call context) { }
        public override void HangUp(Call context) => context.Transition(new HungUpState());
    }
    public sealed class OnHoldState : StateBase
    {
        public override string Name => "On hold";
        public override bool CanSpeak => false;
        public override bool CanHold => false;
        public override bool CanResume => true;
        public override string Speak(Call context) => "Speaking is blocked while on hold.";
        public override void Hold(Call context) { }
        public override void Resume(Call context) => context.Transition(new OnCallState());
        public override void HangUp(Call context) => context.Transition(new HungUpState());
    }
    public sealed class HungUpState : StateBase
    {
        public override string Name => "Hung up";
        public override bool CanSpeak => false;
        public override bool CanHold => false;
        public override bool CanResume => false;
        public override bool IsEnded => true;
        public override string Speak(Call context) => "Speaking is blocked while hung up.";
        public override void Hold(Call context) => throw new InvalidOperationException("This call has ended.");
        public override void Resume(Call context) => throw new InvalidOperationException("Create a new call to reconnect.");
        public override void HangUp(Call context) { }
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
        public StateBase State { get; private set; } = new OnCallState();
        public bool IsEnded => State.IsEnded;
        public bool CanHold => State.CanHold;
        public bool CanResume => State.CanResume;
        public string StateName => State.Name;
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
        internal void Transition(StateBase next)
        {
            if (State.CanSpeak) accumulated += clock() - startedSegment;
            State = next;
            if (next.CanSpeak) startedSegment = clock();
            if (next.IsEnded) EndedAt = DateTime.Now;
        }
        public string Speak() => State.Speak(this);
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


    // COMPOSITE  Component, Composite and Leaf.
    public abstract class CallLogComponent : IEnumerable<CallLogComponent>
    {
        public string Name { get; }
        protected CallLogComponent(string name) { Name = name; }
        public abstract void Operation(Action<SingleCall> action);
        public virtual void Add(CallLogComponent child) => throw new NotSupportedException("A single call cannot contain children.");
        public virtual void Remove(CallLogComponent child) => throw new NotSupportedException("A single call cannot contain children.");
        public abstract IEnumerator<CallLogComponent> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerable<SingleCall> GetCalls()
        {
            var calls = new List<SingleCall>();
            Operation(calls.Add);
            return calls;
        }
    }
    public sealed class SingleCall : CallLogComponent
    {
        public Call Call { get; }
        public SingleCall(Call call) : base(call.Caller) { Call = call; }
        public override void Operation(Action<SingleCall> action) => action(this);
        
        public override IEnumerator<CallLogComponent> GetEnumerator() =>
            Enumerable.Empty<CallLogComponent>().GetEnumerator();
        public override string ToString() => $"{Call.StartedAt:yyyy-MM-dd HH:mm:ss} | {Call.Caller} -> {Call.Receiver} | {Call.Role} | {Call.StateName} | {Call.Duration:hh\\:mm\\:ss} | {Call.Id.ToString().Substring(0, 8)}";
    }
    public sealed class CallGroup : CallLogComponent
    {
        private readonly List<CallLogComponent> children = new List<CallLogComponent>();
        public IReadOnlyList<CallLogComponent> Children => children.AsReadOnly();
        public CallGroup(string name) : base(name) { }
        public override void Add(CallLogComponent child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));
            if (Contains(child, this)) throw new InvalidOperationException("A group cannot contain itself or an ancestor.");
            if (!children.Contains(child)) children.Add(child);
        }
        public override void Remove(CallLogComponent child) => children.Remove(child);
        private static bool Contains(CallLogComponent item, CallLogComponent target) =>
            ReferenceEquals(item, target) || (item is CallGroup group && group.Children.Any(c => Contains(c, target)));
        public override IEnumerator<CallLogComponent> GetEnumerator() => children.GetEnumerator();
        public override void Operation(Action<SingleCall> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            using (IEnumerator<CallLogComponent> iterator = GetEnumerator())
                while (iterator.MoveNext()) iterator.Current.Operation(action);
        }
    }
    // ITERATOR   aggregate and iterator base classes.
    public abstract class AggregateBase
    {
        public abstract IteratorBase CreateIterator();
    }
    public abstract class IteratorBase
    {
        public abstract SingleCall First();
        public abstract SingleCall Next();
        public abstract SingleCall Previous(); // Nav item required by the UI.
        public abstract bool IsDone();
        public abstract SingleCall CurrentItem();
    }
    public sealed class CallCollection : AggregateBase
    {
        private readonly List<SingleCall> items;
        public CallCollection(IEnumerable<SingleCall> calls) { items = calls.ToList(); }
        public int Count => items.Count;
        public SingleCall this[int index] => items[index];
        public override IteratorBase CreateIterator() => new CallLogIterator(this);
    }
    public sealed class CallLogIterator : IteratorBase
    {
        private readonly CallCollection aggregate;
        private int position;
        public CallLogIterator(CallCollection aggregate) { this.aggregate = aggregate; }
        public override SingleCall First() { position = 0; return CurrentItem(); }
        public override SingleCall Next()
        { if (position < aggregate.Count) position++; return CurrentItem(); }
        public override SingleCall Previous()
        { if (position > 0) position--; return CurrentItem(); }
        public override bool IsDone() => position >= aggregate.Count;
        public override SingleCall CurrentItem() => IsDone() ? null : aggregate[position];
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
        

        public IEnumerable<SingleCall> GetChronologicalCalls() => Root.GetCalls()
            .OrderBy(c => c.Call.StartedAt).ThenBy(c => c.Call.Id);
        public IteratorBase CreateIterator(string query = "")
        {
            AggregateBase aggregate = new CallCollection(GetChronologicalCalls().Where(c =>
                string.IsNullOrWhiteSpace(query) ||
                (c.Call.Caller + " " + c.Call.Receiver + " " + c.Call.Id + " " + c.Call.Role)
                    .IndexOf(query.Trim(), StringComparison.OrdinalIgnoreCase) >= 0));
            return aggregate.CreateIterator();
        }
    }
    public sealed class CallLogStore
    {
        private readonly string path;
        public CallLogStore(string path) { this.path = path; }
        public void Save(CallLog log)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(path)));
            var records = log.GetChronologicalCalls().Where(c => c.Call.IsEnded).Select(c => c.Call.ToRecord()).ToList();
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
                throw new InvalidDataException("Call history contains invalid records");
            foreach (var record in records) log.Add(Call.Restore(record));
        }
    }
    


    // PROXY  shared SubjectBase and an injected RealSubject ===
    // The manager check is the Protection Proxy variant
    public abstract class SubjectBase
    {
        public abstract SingleCall MakeCall(string caller, string receiver, UserRole role, PhoneDevice device);
        public abstract SingleCall ReturnCall(SingleCall original, string manager, UserRole role, PhoneDevice device);
    }
    public sealed class RealCallService : SubjectBase
    {
        private readonly CallLog log;
        internal CallLog Log => log;
        public RealCallService(CallLog log) { this.log = log ?? throw new ArgumentNullException(nameof(log)); }
        public override SingleCall MakeCall(string caller, string receiver, UserRole role, PhoneDevice device) =>
            log.Add(new Call(caller, receiver, role, device));
        public override SingleCall ReturnCall(SingleCall original, string manager, UserRole role, PhoneDevice device) =>
            log.Add(new Call(manager, original.Call.Caller, role, device, original.Call.Id));
    }
    public sealed class CallProxy : SubjectBase
    {
        private readonly RealCallService real;
        private readonly CallLog log;
        public CallProxy(RealCallService real)
        {
            this.real = real ?? throw new ArgumentNullException(nameof(real));
            log = real.Log;
        }
        private void EnsureAvailable()
        {
            if (log.Root.GetCalls().Any(c => !c.Call.IsEnded))
                throw new InvalidOperationException("Hang up the active call first");
        }
        public override SingleCall MakeCall(string caller, string receiver, UserRole role, PhoneDevice device)
        { EnsureAvailable(); return real.MakeCall(caller, receiver, role, device); }
        public override SingleCall ReturnCall(SingleCall original, string manager, UserRole role, PhoneDevice device)
        {
            if (role != UserRole.Manager) throw new UnauthorizedAccessException("Only the manager can return a call from the log");
            if (original == null || !original.Call.IsEnded || !log.Root.GetCalls().Contains(original))
                throw new InvalidOperationException("Select a completed call from this log");
            EnsureAvailable(); return real.ReturnCall(original, manager, role, device);
        }
    }
}

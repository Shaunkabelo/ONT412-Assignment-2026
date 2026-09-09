namespace CallCenterApp.Models
{
    // Leaf node in the Composite tree - one real call.
    public class SingleCall : ICallComponent
    {
        private readonly Call call;

        public SingleCall(Call call) => this.call = call;

        public Call GetCall() => call;

        public string Info() =>
            $"{call.CallerName} - {call.GetStateName()} ({call.GetDurationSummary()})";
    }
}

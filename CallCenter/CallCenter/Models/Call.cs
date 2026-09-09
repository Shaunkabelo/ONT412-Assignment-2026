namespace CallCenterApp.Models
{
    // The "Context" in the State pattern. It delegates behaviour to whichever
    // ICallState is currently active instead of using if/else on a status flag.
    public class Call
    {
        private ICallState state;

        public string CallerName { get; }
        public int TalkDurationSeconds { get; set; }
        public int HoldDurationSeconds { get; set; }

        public Call(string callerName)
        {
            CallerName = callerName;
            state = new OnCallState();
        }

        public void SetState(ICallState newState) => state = newState;
        public string GetStateName() => state.StateName;

        public void Speak() => state.Speak(this);
        public void Hold() => state.Hold(this);
        public void HangUp() => state.HangUp(this);

        // Advances whichever duration counter belongs to the current state.
        public void Tick() => state.Tick(this);

        public string GetDurationSummary() =>
            $"Talk: {TalkDurationSeconds}s | Hold: {HoldDurationSeconds}s";
    }
}


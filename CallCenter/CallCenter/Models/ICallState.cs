namespace CallCenterApp.Models
{
    // ---------------- STATE PATTERN ----------------
    // Each concrete state controls what happens when the call is spoken on,
    // held, hung up, or ticked by the duration timer.
    public interface ICallState
    {
        string StateName { get; }
        void Speak(Call call);
        void Hold(Call call);
        void HangUp(Call call);
        void Tick(Call call);
    }
}
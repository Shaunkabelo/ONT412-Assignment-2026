using System.Windows.Forms;

namespace CallCenterApp.Models
{
    public class OnHoldState : ICallState
    {
        public string StateName => "On Hold";

        public void Speak(Call call) => MessageBox.Show("Cannot speak while on hold.");
        public void Hold(Call call) => MessageBox.Show("Already on hold.");
        public void HangUp(Call call) => call.SetState(new HungUpState());
        public void Tick(Call call) => call.HoldDurationSeconds++;
    }
}
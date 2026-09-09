using System.Windows.Forms;

namespace CallCenterApp.Models
{
    public class OnCallState : ICallState
    {
        public string StateName => "On Call";

        public void Speak(Call call) => MessageBox.Show($"{call.CallerName} is speaking...");
        public void Hold(Call call) => call.SetState(new OnHoldState());
        public void HangUp(Call call) => call.SetState(new HungUpState());
        public void Tick(Call call) => call.TalkDurationSeconds++;
    }
}

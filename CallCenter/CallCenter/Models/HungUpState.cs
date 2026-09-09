using System.Windows.Forms;

namespace CallCenterApp.Models
{
    public class HungUpState : ICallState
    {
        public string StateName => "Hung Up";

        public void Speak(Call call) => MessageBox.Show("Cannot speak, call has ended.");
        public void Hold(Call call) => MessageBox.Show("Cannot hold, call has ended.");
        public void HangUp(Call call) => MessageBox.Show("Call already ended.");
        public void Tick(Call call) { /* duration stops accumulating once ended */ }
    }
}


using System.Windows.Forms;

namespace CallCenterApp.Models
{
    // ---------------- PROXY PATTERN ----------------
    // Stands in front of the real Call object. Here it logs/announces access,
    // but the same slot could later add permission checks before returning
    // the real call to a technician or manager.
    public class CallProxy
    {
        private readonly Call realCall;

        public CallProxy(Call call)
        {
            realCall = call;
        }

        public Call AccessCall()
        {
            MessageBox.Show($"Accessing call for {realCall.CallerName} through proxy...");
            return realCall;
        }
    }
}

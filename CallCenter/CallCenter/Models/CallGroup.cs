using System.Collections.Generic;
using System.Text;

namespace CallCenterApp.Models
{
    // Composite node - a named batch that can hold SingleCall entries (or
    // even other CallGroups), but still exposes the same Info() contract.
    public class CallGroup : ICallComponent
    {
        private readonly List<ICallComponent> members = new List<ICallComponent>();

        public string GroupName { get; }

        public CallGroup(string groupName) => GroupName = groupName;

        public void Add(ICallComponent component) => members.Add(component);

        public string Info()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Group [{GroupName}]:");
            foreach (var member in members)
            {
                sb.AppendLine("  " + member.Info());
            }
            return sb.ToString().TrimEnd();
        }
    }
}


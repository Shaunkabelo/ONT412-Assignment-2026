using System.Collections.Generic;

namespace CallCenterApp.Models
{
    // ---------------- ITERATOR PATTERN ----------------
    // Supports moving forward AND backward through the log, so the manager
    // can step in between calls before deciding which one to return.
    public class CallLogIterator
    {
        private readonly List<ICallComponent> items;
        private int index = -1; // position "before" the first element

        public CallLogIterator(List<ICallComponent> items)
        {
            this.items = items;
        }

        public bool HasNext() => index + 1 < items.Count;
        public ICallComponent Next() => items[++index];

        public bool HasPrevious() => index - 1 >= 0;
        public ICallComponent Previous() => items[--index];

        public void Reset() => index = -1;
    }
}

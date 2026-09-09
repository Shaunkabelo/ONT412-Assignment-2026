using System;
using System.Collections.Generic;

namespace CallCenterApp.Models
{
    // The "Aggregate" in the Iterator pattern - hides its internal List<T>
    // and only ever hands out a CallLogIterator to walk through it.
    public class CallLog
    {
        private readonly List<ICallComponent> calls = new List<ICallComponent>();

        public void AddCall(ICallComponent call) => calls.Add(call);
        public CallLogIterator GetIterator() => new CallLogIterator(calls);
        public int Count => calls.Count;

        // Linear search built entirely on top of the iterator, so the log
        // never exposes its backing collection to the search feature either.
        public ICallComponent FindByCallerName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;

            var it = GetIterator();
            while (it.HasNext())
            {
                var component = it.Next();
                if (component is SingleCall single &&
                    single.GetCall().CallerName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return component;
                }
            }
            return null;
        }
    }
}


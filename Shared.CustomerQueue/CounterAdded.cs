using System;
using System.Collections.Generic;

namespace Shared.CustomerQueue
{
    public sealed class CounterAdded : CustomerQueueEvent
    {
        public string CounterName { get; }

        public CounterAdded(
            Guid aggregateRootId,
            string counterName) : base(aggregateRootId)
        {
            CounterName = counterName;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return CounterName;
        }
    }
}
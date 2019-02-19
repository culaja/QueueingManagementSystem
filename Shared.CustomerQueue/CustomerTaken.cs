using System;
using System.Collections.Generic;

namespace Shared.CustomerQueue
{
    public sealed class CustomerTaken : CustomerQueueEvent
    {
        public Guid CounterId { get; }
        public Guid TickedId { get; }
        public DateTime Timestamp { get; }

        public CustomerTaken(
            Guid aggregateRootId,
            Guid counterId,
            Guid tickedId,
            DateTime timestamp) : base(aggregateRootId)
        {
            CounterId = counterId;
            TickedId = tickedId;
            Timestamp = timestamp;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var item in base.GetEqualityComponents()) yield return item;
            yield return CounterId;
            yield return TickedId;
            yield return Timestamp;
        }
    }
}
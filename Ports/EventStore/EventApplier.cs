using System.Linq;
using Common;
using Common.Messaging;

namespace Ports.EventStore
{
    public static class EventApplier
    {
        public static int ApplyAllTo<T, Tk>(this IEventStore eventStore, IRepository<T, Tk> repository) 
            where T : AggregateRoot 
            where Tk: AggregateRootCreated => eventStore
            .LoadAllFor<T>()
            .Select(domainEvent => HandleBasedOnType(domainEvent, repository))
            .Count();

        private static IDomainEvent HandleBasedOnType<T, Tk>(
            IDomainEvent domainEvent,
            IRepository<T, Tk> repository) 
            where T : AggregateRoot
            where Tk: AggregateRootCreated
        {
            switch (domainEvent)
            {
                case Tk aggregateRootCreated:
                    repository.CreateFrom(aggregateRootCreated);
                    break;
                default:
                    repository.BorrowBy(
                        domainEvent.AggregateRootId,
                        t => ApplyToAggregate(t, domainEvent));
                    break;
            }

            return domainEvent;
        }

        private static T ApplyToAggregate<T>(T aggregateRoot, IDomainEvent e) where T : AggregateRoot
        {
            aggregateRoot.ApplyFrom(e);
            return aggregateRoot;
        }
    }
}
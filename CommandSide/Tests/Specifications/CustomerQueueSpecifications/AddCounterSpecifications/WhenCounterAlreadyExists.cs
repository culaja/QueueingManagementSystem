using System.Collections.Generic;
using Common.Messaging;
using Domain.Commands;
using DomainServices.CommandHandlers;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static Tests.CustomerQueueTestValues;

namespace Tests.Specifications.CustomerQueueSpecifications.AddCounterSpecifications
{
    public sealed class AddingCounterWhenCounterAlreadyExistsSpecification : CustomerQueueSpecification<AddCounter>
    {
        public AddingCounterWhenCounterAlreadyExistsSpecification() : base(SingleCustomerQueueId)
        {
        }
        
        protected override AddCounter CommandToExecute => new AddCounter(CounterA_Name);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return new CounterAdded(SingleCustomerQueueId, CounterA_Name);
        }

        public override CommandHandler<AddCounter> When() => new AddCounterHandler(CustomerQueueRepository);

        [Fact]
        public void doesnt_contain_counter_added_event() => ProducedEvents.Should().NotContain(new CounterAdded(
            SingleCustomerQueueId,
            CounterA_Name));

        [Fact]
        public void returns_failure() => Result.IsFailure.Should().BeTrue();
    }
}
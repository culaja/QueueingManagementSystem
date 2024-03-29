using System.Collections.Generic;
using Common.Messaging;
using Domain.Commands;
using DomainServices.CommandHandlers;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static Tests.CustomerQueueTestValues;

namespace Tests.Specifications.CustomerQueueSpecifications.RevokeCustomerSpecifications
{
    public sealed class WhenCustomerIsBeingServed : CustomerQueueSpecification<RevokeCustomer>
    {
        public WhenCustomerIsBeingServed() : base(SingleCustomerQueueId)
        {
        }

        protected override RevokeCustomer CommandToExecute => new RevokeCustomer(CounterA_Name);

        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return new CounterAdded(SingleCustomerQueueId, CounterA_Name);
            yield return new TicketAdded(SingleCustomerQueueId, Ticket1_Id, Ticket1_Number);
            yield return new CustomerTaken(SingleCustomerQueueId, CounterA_Name, Ticket1_Id);
        }

        public override CommandHandler<RevokeCustomer> When() => new RevokeCustomerHandler(CustomerQueueRepository);

        [Fact]
        public void customer_revoked_event_is_produced() => ProducedEvents.Should().Contain(new CustomerRevoked(
            SingleCustomerQueueId,
            CounterA_Name,
            Ticket1_Id));

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();
    }
}
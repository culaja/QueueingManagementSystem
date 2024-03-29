using System.Collections.Generic;
using Common.Messaging;
using Domain.Commands;
using DomainServices.CommandHandlers;
using FluentAssertions;
using Shared.CustomerQueue;
using Xunit;
using static Tests.CustomerQueueTestValues;

namespace Tests.Specifications.CustomerQueueSpecifications.AddTicketSpecifications
{
    public sealed class WhenAvailableCounterExistsAndItIsServingATicket : CustomerQueueSpecification<AddTicket>
    {
        public WhenAvailableCounterExistsAndItIsServingATicket() : base(SingleCustomerQueueId)
        {
        }
        
        protected override AddTicket CommandToExecute => new AddTicket(Ticket2_Id, Ticket2_Number);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield return new CounterAdded(SingleCustomerQueueId, CounterA_Name);
            yield return new TicketAdded(SingleCustomerQueueId, Ticket1_Id, Ticket1_Number);
            yield return new CustomerTaken(SingleCustomerQueueId, CounterA_Name, Ticket1_Id);
        }

        public override CommandHandler<AddTicket> When() => new AddTicketHandler(CustomerQueueRepository);

        [Fact]
        public void customer_taken_is_not_produced() => ProducedEvents.Should().NotContain(
            new CustomerTaken(SingleCustomerQueueId, CounterA_Name, Ticket2_Id));
    }
}
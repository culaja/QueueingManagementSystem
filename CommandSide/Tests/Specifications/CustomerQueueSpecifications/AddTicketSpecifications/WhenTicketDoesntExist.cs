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
    public sealed class WhenTicketDoesntExist : CustomerQueueSpecification<AddTicket>
    {
        public WhenTicketDoesntExist() : base(SingleCustomerQueueId)
        {
        }
        
        protected override AddTicket CommandToExecute => new AddTicket(Ticket1_Id, Ticket1_Number);
        
        public override IEnumerable<CustomerQueueEvent> Given()
        {
            yield break;
        }

        public override CommandHandler<AddTicket> When() => new AddTicketHandler(CustomerQueueRepository);

        [Fact]
        public void ticket_added_event_exists() => ProducedEvents.Should().Contain(new TicketAdded(
            SingleCustomerQueueId,
            Ticket1_Id,
            Ticket1_Number));

        [Fact]
        public void returns_success() => Result.IsSuccess.Should().BeTrue();
    }
}
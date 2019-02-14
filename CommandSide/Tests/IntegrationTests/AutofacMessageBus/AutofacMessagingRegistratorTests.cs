using System.Linq;
using Autofac;
using AutofacApplicationWrapUp;
using AutofacMessageBus;
using FluentAssertions;
using Xunit;
using static Tests.StudentsValues;

namespace Tests.IntegrationTests.AutofacMessageBus
{
    public sealed class AutofacMessagingRegistratorTests
    {
        private readonly IContainer _container;

        public AutofacMessagingRegistratorTests()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<MainRegistrator>();
            _container = containerBuilder.Build();
        }
        
        [Fact]
        public void _1() => new AutofacMessageResolver(_container)
            .GetMessageHandlersFor(StankoMovedToNoviSad)
                .Select(h => h.GetType())
                .Should().OnlyHaveUniqueItems();
    }
}
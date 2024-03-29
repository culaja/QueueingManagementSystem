using Autofac;
using MongoDbEventStore;
using Ports.EventStore;

namespace AutofacApplicationWrapup
{
    public sealed class EventStoreRegistrator : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(new DatabaseContext("mongodb://localhost:27017/", "CustomerQueue")).SingleInstance();
            builder.RegisterType<EventStore>().As<IEventStore>().SingleInstance();
        }
    }
}
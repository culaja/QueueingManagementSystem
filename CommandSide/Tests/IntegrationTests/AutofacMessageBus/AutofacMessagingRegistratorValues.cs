using System.Collections.Generic;
using System.Reflection;
using AutofacMessageBus;

namespace Tests.IntegrationTests.AutofacMessageBus
{
    public static class AutofacMessagingRegistratorValues
    {
        public static readonly AutofacMessagingRegistrator CompleteAutofacMessagingRegistrator = new AutofacMessagingRegistrator(
            new List<Assembly>
            {
            },
            new List<Assembly>()
            {
            });
    }
}
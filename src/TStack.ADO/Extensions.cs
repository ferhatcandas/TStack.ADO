using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TStack.ADO.Connection;

namespace TStack.ADO
{
    public static class Extensions
    {
        public static IServiceCollection UseADO(this IServiceCollection serviceCollection, Func<ADOConnection, ADOConnection> function)
        {
            ADOConnection adoConnection = null;
            ADOManager baseManager = new ADOManager(function(adoConnection));
            serviceCollection.AddSingleton<ADOManager>(baseManager);
            return serviceCollection;
        }
    }
}

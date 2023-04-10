using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.SqlServer;

namespace DataAccess
{
    public class EventConfiguration : DbConfiguration
    {
        public EventConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());

            DbInterception.Add(new EventInterceptorTransientErrors());
            DbInterception.Add(new EventInterceptorLogging());
        }
    }
}

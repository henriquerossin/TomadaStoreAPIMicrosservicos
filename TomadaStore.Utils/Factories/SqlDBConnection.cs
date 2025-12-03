using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomadaStore.Utils.Factories
{
    public class SqlDBConnection : DbConnectionFactory
    {
        public override IDBConnection CreateDBConnection()
        {
            return new SqlDBConnectionImpl();
        }
    }
}

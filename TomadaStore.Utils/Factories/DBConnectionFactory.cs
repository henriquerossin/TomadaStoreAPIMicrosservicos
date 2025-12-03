namespace TomadaStore.Utils.Factories
{
    internal abstract class DbConnectionFactory
    {
        public abstract IDBConnection CreateDBConnection();

        public string GetConnectionString()
        {
            var dbConnection = CreateDBConnection();
            return dbConnection.ConnectionString();
        }
    }
}

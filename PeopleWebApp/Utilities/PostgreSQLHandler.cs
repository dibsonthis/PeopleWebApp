using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleWebApp.Utilities
{
    public static class PostgreSQLHandler
    {
        public static string ParsePostgresConnectionString(string PostgresConnectionString)
        {
            PostgresConnectionString = PostgresConnectionString.Replace("postgres://", string.Empty);

            var pgUserPass = PostgresConnectionString.Split("@")[0];
            var pgHostPortDb = PostgresConnectionString.Split("@")[1];
            var pgHostPort = pgHostPortDb.Split("/")[0];

            var pgDb = pgHostPortDb.Split("/")[1];
            var pgUser = pgUserPass.Split(":")[0];
            var pgPass = pgUserPass.Split(":")[1];
            var pgHost = pgHostPort.Split(":")[0];
            var pgPort = pgHostPort.Split(":")[1];

            var connectionString = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};SSL Mode=Require;TrustServerCertificate=True;";

            return connectionString;
        }
    }
}

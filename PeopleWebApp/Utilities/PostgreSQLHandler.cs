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
            var uri = new Uri(PostgresConnectionString);

            var username = uri.UserInfo.Split(':')[0];

            var password = uri.UserInfo.Split(':')[1];

            var connectionString =

            "; Database=" + uri.AbsolutePath.Substring(1) +

            "; Username=" + username +

            "; Password=" + password +

            "; Port=" + uri.Port +

            "; SSL Mode=Require; Trust Server Certificate=true;";

            return connectionString;
        }
    }
}

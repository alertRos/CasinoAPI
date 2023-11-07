using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    using System.Data.Common;

    public class Connection
    {
        private string connectionString = string.Empty;
        public Connect()
        {
            var builder = new ConfigurationBuilder().SetBasePath
                (Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            connectionString = builder.GetSection
                ("ConnectionStrings:mainConnection").Value;
        }

        public string getConnection()
        {
            return connectionString;
        }
    }
}

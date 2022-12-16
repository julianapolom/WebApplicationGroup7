using Microsoft.Extensions.Configuration;

namespace Tools
{
    public class Connection
    {
        public Connection(IConfiguration config) =>
            Configuration = config;

        public IConfiguration Configuration { get; }

        public string GetConnection()
        {
            return Configuration.GetConnectionString("Connection");
        }
    }
}

using Castle.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using HomeDevices.Net.Server.Net.Data;

namespace HomeDevices.Net.Server.Web.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContextFactory()
        {
        }
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            string connectionString = "server=192.168.99.200;port=3306;database=ruuviserver;uid=ruuvi;password=K4XJUMHfmDpDFDuOG33c_21";

            ServerVersion sv = ServerVersion.AutoDetect(connectionString);
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySql(connectionString, sv);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}

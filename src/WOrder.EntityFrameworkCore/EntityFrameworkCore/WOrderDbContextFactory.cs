using WOrder.Configuration;
using WOrder.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WOrder.EntityFrameworkCore
{
    /* This class is needed to run EF Core PMC commands. Not used anywhere else */
    public class WOrderDbContextFactory : IDesignTimeDbContextFactory<WOrderDbContext>
    {
        public WOrderDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<WOrderDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DbContextOptionsConfigurer.Configure(
                builder,
                configuration.GetConnectionString(WOrderConsts.ConnectionStringName)
            );

            return new WOrderDbContext(builder.Options);
        }
    }
}
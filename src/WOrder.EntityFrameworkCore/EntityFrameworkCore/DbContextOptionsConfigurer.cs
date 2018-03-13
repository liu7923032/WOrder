using Microsoft.EntityFrameworkCore;

namespace WOrder.EntityFrameworkCore
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure(
            DbContextOptionsBuilder<WOrderDbContext> dbContextOptions, 
            string connectionString
            )
        {
            /* This is the single point to configure DbContextOptions for WOrderDbContext */
            dbContextOptions.UseSqlServer(connectionString,(a)=>
            {
                a.UseRowNumberForPaging(true);
               
            });

        }
    }
}

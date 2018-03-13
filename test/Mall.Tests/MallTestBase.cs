using System;
using System.Threading.Tasks;
using Abp.TestBase;
using WOrder.EntityFrameworkCore;
using WOrder.Tests.TestDatas;

namespace WOrder.Tests
{
    public class WOrderTestBase : AbpIntegratedTestBase<WOrderTestModule>
    {
        public WOrderTestBase()
        {
            UsingDbContext(context => new TestDataBuilder(context).Build());
        }

        protected virtual void UsingDbContext(Action<WOrderDbContext> action)
        {
            using (var context = LocalIocManager.Resolve<WOrderDbContext>())
            {
                action(context);
                context.SaveChanges();
            }
        }

        protected virtual T UsingDbContext<T>(Func<WOrderDbContext, T> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<WOrderDbContext>())
            {
                result = func(context);
                context.SaveChanges();
            }

            return result;
        }

        protected virtual async Task UsingDbContextAsync(Func<WOrderDbContext, Task> action)
        {
            using (var context = LocalIocManager.Resolve<WOrderDbContext>())
            {
                await action(context);
                await context.SaveChangesAsync(true);
            }
        }

        protected virtual async Task<T> UsingDbContextAsync<T>(Func<WOrderDbContext, Task<T>> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<WOrderDbContext>())
            {
                result = await func(context);
                context.SaveChanges();
            }

            return result;
        }
    }
}

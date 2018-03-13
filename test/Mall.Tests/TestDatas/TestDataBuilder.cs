using WOrder.EntityFrameworkCore;

namespace WOrder.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly WOrderDbContext _context;

        public TestDataBuilder(WOrderDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            //create test data here...
        }
    }
}
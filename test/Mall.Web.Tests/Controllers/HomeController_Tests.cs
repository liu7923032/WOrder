using System.Threading.Tasks;
using WOrder.Web.Controllers;
using Shouldly;
using Xunit;

namespace WOrder.Web.Tests.Controllers
{
    public class HomeController_Tests: WOrderWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}

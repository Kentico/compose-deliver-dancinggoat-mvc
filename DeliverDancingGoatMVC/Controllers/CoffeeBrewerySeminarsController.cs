using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;

using KenticoCloud.Deliver;

namespace DeliverDancingGoatMVC.Controllers
{
    public class CoffeeBrewerySeminarsController : AsyncController
    {
        private readonly DeliverClient client = new DeliverClient(ConfigurationManager.AppSettings["ProjectId"], ConfigurationManager.AppSettings["PreviewToken"]);

        [Route("coffee-brewery-seminars")]
        public async Task<ActionResult> Index()
        {
            var response = await client.GetItemAsync("coffee_brewery_seminars");
            return View(response.Item);
        }
    }
}
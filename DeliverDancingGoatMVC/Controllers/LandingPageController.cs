using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Net;

using KenticoCloud.Deliver;

namespace DeliverDancingGoatMVC.Controllers
{
    public class LandingPageController : AsyncController
    {
        private readonly DeliverClient client = new DeliverClient(ConfigurationManager.AppSettings["ProjectId"]);

        [Route("{id}")]
        public async Task<ActionResult> Index(string id)
        {
            try
            {
                var response = await client.GetItemAsync(id, new[] { new EqualsFilter("system.type", "landing_page") });

                return View(response.Item);
            }
            catch (DeliverException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return HttpNotFound();
                }

                throw;
            }
        }
    }
}
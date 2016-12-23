using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Net;
using System.Linq;

using KenticoCloud.Deliver;

namespace DeliverDancingGoatMVC.Controllers
{
    public class LandingPageController : AsyncController
    {
        private readonly DeliverClient client = new DeliverClient(ConfigurationManager.AppSettings["ProjectId"], ConfigurationManager.AppSettings["PreviewToken"]);

        public async Task<ActionResult> View(string urlSlug)
        {
            if (urlSlug == null)
            {
                return Redirect("/");
            }

            try
            {
                var response = 
                    await client.GetItemsAsync(new[] {
                        new EqualsFilter("system.type", "landing_page"),
                        new EqualsFilter("elements.url", urlSlug)
                    });

                var item = response.Items.FirstOrDefault();
                if (item == null)
                {
                    return Redirect("/");
                }

                return View(item);
            }
            catch (DeliverException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return Redirect("/");
                }

                throw;
            }
        }
    }
}
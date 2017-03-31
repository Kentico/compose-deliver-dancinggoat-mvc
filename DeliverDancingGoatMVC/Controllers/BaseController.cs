using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

using KenticoCloud.Deliver;

namespace DeliverDancingGoatMVC.Controllers
{
    public class BaseController : AsyncController
    {
        protected readonly DeliverClient Client = new DeliverClient(ConfigurationManager.AppSettings["ProjectId"], ConfigurationManager.AppSettings["PreviewToken"]);

        protected async Task<ActionResult> ShowPage(string urlSlug, string type, string viewName = null)
        {
            if (urlSlug == null)
            {
                return Redirect("/");
            }

            try
            {
                var response =
                    await Client.GetItemsAsync(new[] {
                        new EqualsFilter("system.type", type),
                        new EqualsFilter("elements.url", urlSlug)
                    });

                var item = response.Items.FirstOrDefault();
                if (item == null)
                {
                    return Redirect("/");
                }

                return View(viewName, item);
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
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Net;
using System.Linq;

using KenticoCloud.Deliver;

namespace DeliverDancingGoatMVC.Controllers
{
    public class LandingPageController : BaseController
    {
        public async Task<ActionResult> Empty(string urlSlug)
        {
            return await ShowPage(urlSlug, "empty_page", "Empty");
        }


        public async Task<ActionResult> View(string urlSlug)
        {
            return await ShowPage(urlSlug, "landing_page");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using KenticoCloud.Deliver;

namespace DeliverDancingGoatMVC.Controllers
{
    [RoutePrefix("articles")]
    public class ArticlesController : BaseController
    {
        [Route]
        public async Task<ActionResult> Index()
        {
            var filters = new List<IFilter> {
                new EqualsFilter("system.type", "article"),
                new Order("elements.post_date", OrderDirection.Descending),
                new ElementsFilter("teaser_image", "post_date", "summary")
            };

            var articles = await Client.GetItemsAsync(filters);

            return View(articles.Items);
        }

        [Route("{id}")]
        public async Task<ActionResult> Show(string id)
        {
            try
            {
                var response = await Client.GetItemAsync(id);
                return View(response.Item);
            }
            catch (DeliverException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new HttpException(404, "Not found");
                }
                else
                {
                    throw;
                }
            }
        }

        [Route("preview/{urlslug}")]
        public async Task<ActionResult> Preview(string urlSlug)
        {
            return await ShowPage(urlSlug, "article", "Show");
        }

    }
}
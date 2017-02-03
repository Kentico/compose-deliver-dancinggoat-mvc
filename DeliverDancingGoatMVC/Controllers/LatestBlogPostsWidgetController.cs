using System.Web.Mvc;
using System.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;
using KenticoCloud.Deliver;
using Newtonsoft.Json.Linq;

namespace DeliverDancingGoatMVC.Controllers
{
    public class LatestBlogPostsWidgetController : Controller
    {
        private const int DISPLAY_LIMIT = 4;
        private const string ORDER_ELEMENT = "elements.post_date";
        private const OrderDirection ORDER_DIRECTION = OrderDirection.Descending;

        private readonly DeliverClient client = new DeliverClient(ConfigurationManager.AppSettings["ProjectId"]);

        [Route("Widgets/LatestBlogPostsWidget")]
        public async Task<ActionResult> Default(string properties)
        {
            var props = JObject.Parse(properties);
            var filters = new List<IFilter> {
                new EqualsFilter("system.type", "article"),
                new Order(ORDER_ELEMENT, ORDER_DIRECTION),
                new ElementsFilter("teaser_image", "post_date", "summary"),
                new LimitFilter(DISPLAY_LIMIT)
            };
            var articles = await client.GetItemsAsync(filters);
            
            return PartialView(articles.Items);
        }
    }
}
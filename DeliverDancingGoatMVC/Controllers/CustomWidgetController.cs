using System;
using System.Web.Mvc;

using DeliverDancingGoatMVC.Models;

using Newtonsoft.Json.Linq;

namespace DeliverDancingGoatMVC.Controllers
{
    public class CustomWidgetController : Controller
    {
        [Route("Widgets/CustomWidget")]
        public ActionResult Default(string properties)
        {
            var model = new CustomWidgetModel
            {
                Time = DateTime.Now,
                Properties = JObject.Parse(properties)
            };

            return PartialView(model);
        }
    }
}
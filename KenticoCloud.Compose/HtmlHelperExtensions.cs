using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace KenticoCloud.Compose
{
    public static class ComposeHtmlHelperExtensions
    {
        private static HttpClient httpClient = new HttpClient();

        private readonly static string ENDPOINT = ConfigurationManager.AppSettings["ComposeEndpoint"] ?? "http://localhost:59691/";

        private readonly static Guid PROJECT_ID = new Guid(ConfigurationManager.AppSettings["ComposeProjectId"] ?? "85e06f31-6a8f-405c-ba9c-dc7aed4ed373");


        public static async Task<HtmlString> EditableAreaAsync(this HtmlHelper helper, string areaId, string itemId)
        {
            var scripts = Scripts.Render(ENDPOINT + "compose.js");

            var url = ENDPOINT + $"widgets/editablearea?location={PROJECT_ID}:{itemId}:{areaId}";
            var ct = helper.ViewContext.HttpContext.Request.TimedOutToken;

            try
            {
                var content = await httpClient.GetAsync(url, ct).ConfigureAwait(false);
                var html = await content.Content.ReadAsStringAsync().ConfigureAwait(false);

                return new HtmlString(scripts + html);
            }
            catch (Exception ex)
            {
                return new HtmlString(ex.Message);
            }
        }


        public static HtmlString EditableArea(this HtmlHelper helper, string areaId, string itemId)
        {
            return helper.EditableAreaAsync(areaId, itemId).Result;
        }
    }
}
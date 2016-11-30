using System;
using System.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace Compose
{
    public static class ComposeHtmlHelperExtensions
    {
        private static HttpClient httpClient = new HttpClient();

        private readonly static int TIMEOUT = 30;
        private readonly static string ENDPOINT = ConfigurationManager.AppSettings["ComposeEndpoint"] ?? "http://localhost:59691/";
        

        public static async Task<HtmlString> EditableAreaAsync(this HtmlHelper helper, string id)
        {
            var scripts = Scripts.Render(ENDPOINT + "compose.js");

            var url = ENDPOINT + "WidgetManager/Zone?design=1&location=data:" + id;
            var ct = new CancellationTokenSource(TIMEOUT * 1000).Token;

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


        public static HtmlString EditableArea(this HtmlHelper helper, string id)
        {
            return helper.EditableAreaAsync(id).Result;
        }
    }
}
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace KenticoCloud.Compose
{
    public static class ComposeHtmlHelperExtensions
    {
        private static HttpClient httpClient;

        private readonly static string ENDPOINT;

        private readonly static Guid PROJECT_ID;
        private readonly static string PREVIEW_TOKEN;


        static ComposeHtmlHelperExtensions()
        {
            PROJECT_ID = GetProjectId();
            PREVIEW_TOKEN = ConfigurationManager.AppSettings["PreviewToken"];
            ENDPOINT = ConfigurationManager.AppSettings["ComposeEndpoint"] ?? "http://localhost:59691/";
            httpClient = CreateHttpClient();
        }


        private static HttpClient CreateHttpClient()
        {
            var http = new HttpClient();

            if (!String.IsNullOrEmpty(PREVIEW_TOKEN))
            {
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", PREVIEW_TOKEN);
            }

            return http;
        }


        private static Guid GetProjectId()
        {
            var id = ConfigurationManager.AppSettings["ProjectId"];
            Guid projectId;

            Guid.TryParse(id, out projectId);

            return projectId;
        }


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
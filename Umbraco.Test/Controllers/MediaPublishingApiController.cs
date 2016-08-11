
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;
namespace Umbraco.e3Portal.Controllers
{
    [IsBackOffice]
    [Umbraco.Web.WebApi.UmbracoAuthorize]
    public class MediaPublishingApiController : UmbracoApiController
    {
        [HttpGet]
        public IEnumerable<string> PublishById(int nodeId, bool includeChildren)
        {
            System.Threading.Thread.SpinWait(10000);
            for (decimal i = 0; i < 10000000; i++) { }
            return new List<string>() {  "", "1", "2" };
        }
    }
}
using System.Collections.Generic;
using Umbraco.Test.Controllers.Models;
using Umbraco.Web.WebApi;




namespace Umbraco.Test.Controllers
{
    [Umbraco.Web.Mvc.PluginController("MediapublishApi")]
    public class MediaPublishController : Umbraco.Web.WebApi.UmbracoApiController
    {
        [Umbraco.Web.WebApi.UmbracoAuthorize]
        // [System.Web.Http.HttpGet]
        public IEnumerable<MediaPublishModel> PublishById(int nodeID)
        {
            return null;
        }
    }
}
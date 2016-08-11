using System;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Models.EntityBase;
using Umbraco.Core.Services;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi.Filters;
using umbraco;
using umbraco.BusinessLogic.Actions;
using Umbraco.Web.Trees;

namespace Umbraco.SQLFileSystem.App_Code
{


    public class PreviewHandler : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            TreeControllerBase.MenuRendering += TreeControllerBase_MenuRendering;
            TreeControllerBase.TreeNodesRendering += TreeControllerBase_TreeNodesRendering;

            base.ApplicationStarted(umbracoApplication, applicationContext);
        }

        private void TreeControllerBase_TreeNodesRendering(TreeControllerBase sender, TreeNodesRenderingEventArgs e)
        {
            if (!sender.TreeAlias.Equals("media"))
                return;

            e.Nodes.CheckPublishingStatus();
        }

        void TreeControllerBase_MenuRendering(TreeControllerBase sender, MenuRenderingEventArgs e)
        {
            if (sender.TreeAlias == "media")
            {
                e.Menu.Items.Add(new MenuItem("publish", "Publish") {
                    Icon = "globe"
                });
                e.Menu.Items.Add(new MenuItem("history", "View history") {
                    Icon = "globe"
                });
                e.Menu.Items.Add(new MenuItem("purge", "Purge hisory") {
                 Icon  = "globe"
                });
            }
        }
    }
}


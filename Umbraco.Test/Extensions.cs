using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web.Models.Trees;

namespace Umbraco.SQLFileSystem
{
    public static class Extensions
    {
        public static void CheckPublishingStatus(this TreeNodeCollection Nodes)
        {
            Action<TreeNode> validate = (node) => {
                System.Diagnostics.Debug.WriteLine(node.NodeType);
                if (node.Trashed == false &&
                    !node.NodeType.Equals("Folder") && 
                    !node.Icon.Equals("icon-trash"))
                {
                    if (SQLMediaProvider.CheckPublishStatus(node.Id) == SQLMediaProviderStatus.Draft )
                    {
                        node.CssClasses.Add("not-published");
                    }
                }
            };


            if (Nodes.Count() < 20)
                Nodes.ForEach(validate);
            else
                Nodes.AsParallel().ForAll(validate);

        }

    }


}
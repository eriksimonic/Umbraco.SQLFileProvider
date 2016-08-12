using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Umbraco.SQLFileSystem.Models
{
    public class MediaPublishRequestModel
    {
        public int nodeId { get; set; }
        public int versionId { get; set; }
        public bool includeChildren { get; set; }

    }
}
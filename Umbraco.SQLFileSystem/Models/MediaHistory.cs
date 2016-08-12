using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Umbraco.SQLFileSystem.Models
{
    public class MediaHistory
    {
        public string Comment { get; internal set; }
        public DateTime Created { get; internal set; }
        public Guid Id { get; internal set; }
        public string Publisher { get; internal set; }
        public SQLMediaProviderStatus StatusCode { get; internal set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Umbraco.SQLFileSystem
{
    public class SQLMediaProvider
    {
        public static SQLMediaProviderStatus CheckPublishStatus(object id)
        {




            return SQLMediaProviderStatus.Draft;
        }
    }
}
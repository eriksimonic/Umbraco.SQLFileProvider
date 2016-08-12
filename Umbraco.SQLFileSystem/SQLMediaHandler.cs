using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Umbraco.Core;

namespace Umbraco.SQLFileSystem.Logic
{
    // A handler to return the image. The hander is installed via the web.config to match the handler path that the IFileSystem code
    // is initiated with, eg /SQLMedia.axd?path=
    class SQLMediaHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            using (FilestreamRepository fr = new FilestreamRepository())
            {
                var path = context.Request.Url.AbsolutePath;
                if (path.StartsWith("/media/", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (String.IsNullOrEmpty(path))
                    {
                        context.Response.StatusCode = 404;
                    }

                    string mimeType;
                    MemoryStream s = null;
                    try
                    {
                        s = fr.GetFileStream(path, out mimeType);
                    }
                    catch
                    {
                        context.Response.StatusCode = 404;
                        context.Response.End();
                        return;
                    }


                    context.Response.ContentType = mimeType;
                    context.Response.AppendHeader("Content-Length", s.Length.ToString());
                    context.Response.BinaryWrite(s.ToArray());
                }
            }
        }
    }
}

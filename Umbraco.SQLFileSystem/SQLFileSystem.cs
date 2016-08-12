
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using Umbraco.Core;
using Umbraco.Core.IO;

namespace Umbraco.SQLFileSystem.Logic
{
    public class SQLFileSystem
    {
        public string VirtualRoot { get; set; }
      //  public ILogger Logger { get; set; }

        public SQLFileSystem(string virtualRoot)
        {
            this.VirtualRoot = virtualRoot;
        }

        public void AddFile(string path, System.IO.Stream stream, bool overrideIfExists)
        {

            int directory;
            string filename;

            if (UmbracoPath.MediaPathParse(path, out directory, out filename))
            {
                string mimeType = MimeTypes.GetMimeType(Path.GetExtension(path));

                using (FilestreamRepository fsr = new FilestreamRepository())
                {
                    fsr.AddFile(directory, path, mimeType, filename, stream);
                }
            }
        }

        public IEnumerable<string> GetFiles(string path, string filter = null)
        {
            path = UmbracoPath.MediaUrlParse(this.VirtualRoot, path);

            using (var fsr = new FilestreamRepository())
            {
                return fsr.GetFiles(path, filter);
            }

        }

        public void AddFile(string path, System.IO.Stream stream)
        {
            this.AddFile(path, stream, true);
        }

        public void DeleteDirectory(string directory, bool recursive)
        {

            int dir;

            if (int.TryParse(directory, out dir))
            {
                using (FilestreamRepository fsr = new FilestreamRepository())
                {
                    fsr.DeleteDirectory(dir);
                }
            }
            else
            {
                throw new Exception(String.Format("Cannot parse directory '{0}' as a number", directory));
            }
        }

        public void DeleteDirectory(string directory)
        {
          //  this.Logger.Information("DeleteDirectory({0})", directory);

            this.DeleteDirectory(directory, true);
        }

        // Sometimes this gets called with n\x where n is directory and x is file, and sometimes
        // it gets called with what I returned from GetUrl
        public bool FileExists(string path)
        {
          //  this.Logger.Information("FileExists({0})", path);

            path = UmbracoPath.MediaUrlParse(this.VirtualRoot, path);

            using (FilestreamRepository fsr = new FilestreamRepository())
            {
                return fsr.FileExists(path);
            }
        }

        public IEnumerable<string> GetDirectories(string path)
        {
         //   this.Logger.Information("GetDirectories({0})", path);
            using (FilestreamRepository fsr = new FilestreamRepository())
            {
                List<string> found = fsr.GetDirectories();
                if (found != null)
                {
                    return found;
                }
            }

            return new List<string>();
        }

        public DateTimeOffset GetLastModified(string path)
        {
         //   this.Logger.Information("GetLastModified({0})", path);

            path = UmbracoPath.MediaUrlParse(this.VirtualRoot, path);

            using (FilestreamRepository fsr = new FilestreamRepository())
            {
                return fsr.GetModifiedDate(path);
            }
        }

        internal string GetServerRelativePath(string path)
        {
            path = path.Replace(@"\", "/");

            var mediaFolder = IOHelper.ResolveUrl(this.VirtualRoot);
            if (path.StartsWith(mediaFolder)) return path;

            if (path.StartsWith(this.VirtualRoot)) return IOHelper.ResolveUrl(path);

            return IOHelper.ResolveUrl(string.Concat(this.VirtualRoot.TrimEnd('/'), "/", path.TrimStart('/')));
        }


        public string GetFullPath(string path)
        {
            return this.GetServerRelativePath(path);
        }

        public string GetRelativePath(string fullPathOrUrl)
        {
            return this.GetServerRelativePath(fullPathOrUrl);
        }
        public string GetUrl(string path)
        {
            return this.GetServerRelativePath(path);
        }

        public System.IO.Stream OpenFile(string path)
        {
          //  this.Logger.Information("OpenFile({0})", path);

            path = UmbracoPath.MediaUrlParse(this.VirtualRoot, path);

            using (FilestreamRepository fsr = new FilestreamRepository())
            {
                string mimeType;
                return fsr.GetFileStream(path, out mimeType);
            }
        }
    }
}

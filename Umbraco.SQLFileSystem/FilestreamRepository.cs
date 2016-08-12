using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Umbraco.Core;
using Umbraco.Core.Persistence;
using Umbraco.Web;

namespace Umbraco.SQLFileSystem.Logic
{
    // Do the work off adding and retrieving files from the database
    internal class FilestreamRepository : IDisposable
    {
        private DatabaseContext db;

        internal FilestreamRepository()
        {
            this.db = ApplicationContext.Current.DatabaseContext;
        }

        // I did make this class create and use a transaction, but didn't go down that route in the end.
        public void Dispose()
        {
        }

        // Add file to the media table
        internal Guid AddFile(int directory, string path, string mimeType, string filename, System.IO.Stream stream)
        {
            var item = new pocos.MediaObjectStorage();

            int streamLength = (int)stream.Length;

            stream.Seek(0, SeekOrigin.Begin);
            byte[] fileData = null;
            using (BinaryReader rdr = new BinaryReader(stream, Encoding.UTF8, true))
            {
                fileData = rdr.ReadBytes(streamLength);
            }
            item.MediaId = Guid.NewGuid();
            item.VersionId = Guid.NewGuid();
            item.Filename = filename;
            item.MimeType = mimeType;
            item.Path = path;
            item.Directory = directory;
            item.Data = fileData;
            item.DataLength = streamLength;
            item.Created = DateTime.Now;
            item.PublishingStatus = (int)SQLMediaProviderStatus.Draft;
            item.CreatorId = UmbracoContext.Current.Security.CurrentUser.Id;
            item.PublisherId = -1;
            item.Modified = item.Created;
            item.ContentNodeId = -1;

            db.Database.Insert(item);

            return item.MediaId;
        }

       

        internal IEnumerable<string> GetFiles(string path, string filter)
        {
            int dir = 0;
            string fileName = string.Empty;

            UmbracoPath.MediaPathParse(path, out dir, out fileName);

            return this.db.Database.Query<pocos.MediaObjectStorage>(new Sql().Select(new object[] { "Path" })
                                                                      .From(pocos.TABLE_MediaObjectStorage)
                                                                      .Where("Directory=@0", new object[] { dir }
                                                                      ))
                                    .Select(x => x.Path)
                                    .ToList();
        }

        internal void UpdateMediaWithId(string path, int id)
        {
            if (string.IsNullOrEmpty(path)) return;
            if (id == 0) return;

            var res =
            this.db.Database.Update<pocos.MediaObjectStorage>(new Sql().From(pocos.TABLE_MediaObjectStorage).Where("Path=@0", new object[] {
                path
            }));

            
        }


        internal void DeleteByMediaId(int id)
        {
           


        }

        // Update the file in the database
        //internal void UpdateFile(string path, Stream stream, string filename, string mimeType)
        //{
        //    AddFile()
        //    string sql = String.Format(@"
        //        UPDATE [{0}]
        //        SET filename = @filename, mimeType = @mimetype, data = @data, datalength = @datalength, modified = @modified
        //        WHERE Path = @path
        //    ", this.TableName);

        //    int streamLength = (int)stream.Length;

        //    stream.Seek(0, SeekOrigin.Begin);
        //    byte[] fileData = null;
        //    using (BinaryReader rdr = new BinaryReader(stream, Encoding.UTF8, true))
        //    {
        //        fileData = rdr.ReadBytes(streamLength);
        //    }

        //    using (SqlConnection con = new SqlConnection(this.ConnectionString))
        //    using (SqlCommand cmd = new SqlCommand(sql, con))
        //    {
        //        cmd.Parameters.Add("@path", SqlDbType.VarChar).Value = path;
        //        cmd.Parameters.Add("@mimetype", SqlDbType.VarChar).Value = mimeType;
        //        cmd.Parameters.Add("@filename", SqlDbType.VarChar).Value = filename;
        //        cmd.Parameters.Add("@data", SqlDbType.VarBinary, fileData.Length).Value = fileData;
        //        cmd.Parameters.Add("@datalength", SqlDbType.Int).Value = streamLength;
        //        cmd.Parameters.Add("@modified", SqlDbType.DateTime2).Value = DateTime.Now;

        //        con.Open();

        //        cmd.ExecuteNonQuery();
        //    }
        //}

        // Delete any file that has the specified directory
        internal void DeleteDirectory(int directory)
        {
            var count = this.db.Database.Delete(new Sql().From(pocos.TABLE_MediaObjectStorage).Where("direcoty=@0", new object[] { directory }));
        }

        // Returns true if specified file exists
        internal bool FileExists(string path)
        {
            var resoults = this.db.Database.Query<pocos.MediaObjectStorage>(new Sql().Select("Id").From(pocos.TABLE_MediaObjectStorage).Where("[Path]=@0", new object[] {
                    path
            })).Count();

            return resoults > 0;

            //string sql = String.Format(@"
            //    SELECT Path
            //    FROM [{0}]
            //    WHERE Path = @path
            //", this.TableName);

            //using (SqlConnection con = new SqlConnection(this.ConnectionString))
            //using (SqlCommand cmd = new SqlCommand(sql, con))
            //{
            //    cmd.Parameters.Add("@path", SqlDbType.VarChar).Value = path;

            //    con.Open();

            //    using (SqlDataReader reader = cmd.ExecuteReader())
            //    {
            //        return reader.HasRows;
            //    }
            //}
        }

        // Returns a list of the 'directories' in the database
        internal List<string> GetDirectories()
        {
            return
            this.db.Database.Query<pocos.MediaObjectStorage>(new Sql().Select("directory").From(pocos.TABLE_MediaObjectStorage))
                .Select(x => x.Directory.ToString()).ToList();

            //List<string> directories = new List<string>();

            //string sql = String.Format(@"
            //    SELECT Directory FROM [{0}]", this.TableName);

            //using (SqlConnection con = new SqlConnection(this.ConnectionString))
            //using (SqlCommand cmd = new SqlCommand(sql, con))
            //{
            //    con.Open();

            //    using (SqlDataReader reader = cmd.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            directories.Add(reader.GetInt32(0).ToString());
            //        }

            //        return directories;
            //    }
            //}
        }

        // Returns the modified date of the file
        internal DateTimeOffset GetModifiedDate(string path)
        {
            return new DateTimeOffset();

            //string sql = String.Format(@"
            //    SELECT Modified, Created
            //    FROM [{0}]
            //    WHERE Path = @path
            //", this.TableName);

            //using (SqlConnection con = new SqlConnection(this.ConnectionString))
            //using (SqlCommand cmd = new SqlCommand(sql, con))
            //{
            //    cmd.Parameters.Add("@path", SqlDbType.VarChar).Value = path;

            //    con.Open();

            //    using (SqlDataReader reader = cmd.ExecuteReader())
            //    {
            //        if (reader.HasRows == false)
            //        {
            //            throw new Exception(String.Format("Specified file '{0}' doesn't exist", path));
            //        }

            //        reader.Read();

            //        if (reader.IsDBNull(0))
            //        {
            //            return reader.GetDateTime(1);
            //        }
            //        else
            //        {
            //            return reader.GetDateTime(0);
            //        }
            //    }
            //}
        }

        // Returns a memory stream of the file, using FILESTREAM technology
        internal MemoryStream GetFileStream(string path, out string mimeType)
        {

            //UmbracoPath.MediaUrlParse(this.

            var item = 
            this.db.Database.Query<pocos.MediaObjectStorage>(new Sql().Select(new object[] { "MimeType", "Data" })
                .From(pocos.TABLE_MediaObjectStorage)
                .Where("Path=@0", new object[] { path })
                ).Single();

            mimeType = item.MimeType;

            return new MemoryStream(item.Data);

            //string sql = String.Format(@"
            //        SELECT
            //            MimeType,
            //            [Data].PathName(),
            //            GET_FILESTREAM_TRANSACTION_CONTEXT()
            //        FROM [{0}]
            //        WHERE Path = @path", this.TableName);

            //using (TransactionScope tx = new TransactionScope())
            //using (SqlConnection con = new SqlConnection(this.ConnectionString))
            //using (SqlCommand cmd = new SqlCommand(sql, con))
            //{
            //    cmd.Parameters.AddWithValue("@path", path);

            //    con.Open();

            //    using (SqlDataReader reader = cmd.ExecuteReader())
            //    {
            //        reader.Read();
            //        mimeType = reader.GetFieldValue<string>(0);
            //        string filePath = reader.GetFieldValue<string>(1);
            //        byte[] txnToken = reader.GetFieldValue<byte[]>(2);
            //        reader.Close();

            //        using (SqlFileStream sqlFileStream = new SqlFileStream(filePath, txnToken, FileAccess.Read))
            //        {
            //            MemoryStream ms = new MemoryStream();
            //            sqlFileStream.Seek(0, SeekOrigin.Begin);
            //            sqlFileStream.CopyTo(ms);

            //            tx.Complete();

            //            return ms;
            //        }
            //    }
            //}
        }
    }
}
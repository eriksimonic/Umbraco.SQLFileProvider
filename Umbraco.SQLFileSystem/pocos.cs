using System;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;


namespace Umbraco.SQLFileSystem
{
    public class pocos
    {
        public const string TABLE_MediaObjectStorage = "MediaObjectStorage";


        [TableName(TABLE_MediaObjectStorage)]
        [PrimaryKey("Id", autoIncrement = true)]
        public class MediaObjectStorage
        {
            
            [PrimaryKeyColumn(AutoIncrement = true)]
            public int Id { get; set; }

            [IndexAttribute(IndexTypes.NonClustered, Name = "idx_Path")]
            public string Path { get; set; }

            [IndexAttribute(IndexTypes.NonClustered, Name = "idx_MediaId")]
            public Guid MediaId { get; set; }

            public int Directory { get; set; }

            public string Filename { get; set; }

            public byte[] Data { get; set; }

            public int DataLength { get; set; }

            public DateTime Modified { get; set; }

            public string MimeType { get; set; }
            public int PublishingStatus { get; set; }
            public DateTime Created { get; set; }
            public int CreatorId { get; set; }
            public int PublisherId { get; set; }
            [IndexAttribute(IndexTypes.UniqueNonClustered, Name = "idx_VersionId")]
            public Guid VersionId { get; set; }

            [IndexAttribute(IndexTypes.NonClustered, Name = "idx_ContentNodeId")]
            public Guid ContentNodeId { get; set; }
        }

    }
}
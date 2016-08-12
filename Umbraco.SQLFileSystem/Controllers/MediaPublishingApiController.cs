using System;
using System.Collections.Generic;
using System.Web.Http;
using Umbraco.SQLFileSystem.Models;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace Umbraco.SQLFileSystem.Controllers
{
    //[IsBackOffice]
    //[Umbraco.Web.WebApi.UmbracoAuthorize]
    [PluginController("mediaPublish")]
    public class MediaPublishingApiController : UmbracoAuthorizedApiController
    {
        [HttpPost]
        public IEnumerable<string> PublishById(MediaPublishRequestModel model)
        {
            System.Threading.Thread.SpinWait(10000);
            for (decimal i = 0; i < 10000000; i++) { }
            return new List<string>() { "", "1", "2" };
        }

        [HttpPost]
        public bool RevertVersion(MediaPublishRequestModel model)
        {
            return true;
        }

        [HttpPost]
        public bool PurgeHistory(MediaPublishRequestModel model)
        {
            return true;
        }

        [HttpPost]
        public IEnumerable<MediaHistory> GetHistory(MediaPublishRequestModel model)
        {
            return new List<MediaHistory>()
            {
                new MediaHistory() {
                        Created = DateTime.Now.AddDays(-2),
                        Publisher = "Erik Simonič",
                        StatusCode  = SQLMediaProviderStatus.Published,
                        Id = Guid.NewGuid(),
                        Comment = "Komentar"
                },
                new MediaHistory() {
                        Created = DateTime.Now.AddDays(-160),
                        Publisher = "Dean podgornik",
                        StatusCode  = SQLMediaProviderStatus.Draft,
                        Id = Guid.NewGuid(),
                        Comment = "Komentar 2"
                },
                new MediaHistory() {
                        Created = DateTime.Now.AddDays(-60),
                        Publisher = "Dean podgornik",
                        StatusCode  = SQLMediaProviderStatus.Rejected,
                        Id = Guid.NewGuid(),
                        Comment = "Neprimerna vsebina"
                }
                ,
                new MediaHistory() {
                        Created = DateTime.Now.AddDays(-60),
                        Publisher = "Dean podgornik",
                        StatusCode  = SQLMediaProviderStatus.Rejected,
                        Id = Guid.NewGuid(),
                        Comment = "Neprimerna vsebina"
                }
                ,
                new MediaHistory() {
                        Created = DateTime.Now.AddDays(-60),
                        Publisher = "Dean podgornik",
                        StatusCode  = SQLMediaProviderStatus.Rejected,
                        Id = Guid.NewGuid(),
                        Comment = "Neprimerna vsebina"
                },
                new MediaHistory() {
                        Created = DateTime.Now.AddDays(-60),
                        Publisher = "Dean podgornik",
                        StatusCode  = SQLMediaProviderStatus.Rejected,
                        Id = Guid.NewGuid(),
                        Comment = "Neprimerna vsebina"
                },
                new MediaHistory() {
                        Created = DateTime.Now.AddDays(-60),
                        Publisher = "Dean podgornik",
                        StatusCode  = SQLMediaProviderStatus.Rejected,
                        Id = Guid.NewGuid(),
                        Comment = "Neprimerna vsebina"
                },
                new MediaHistory() {
                        Created = DateTime.Now.AddDays(-60),
                        Publisher = "Dean podgornik",
                        StatusCode  = SQLMediaProviderStatus.Rejected,
                        Id = Guid.NewGuid(),
                        Comment = "Neprimerna vsebina"
                },
                new MediaHistory() {
                        Created = DateTime.Now.AddDays(-60),
                        Publisher = "Dean podgornik",
                        StatusCode  = SQLMediaProviderStatus.Rejected,
                        Id = Guid.NewGuid(),
                        Comment = "Neprimerna vsebina"
                },
                new MediaHistory() {
                        Created = DateTime.Now.AddDays(-60),
                        Publisher = "Dean podgornik",
                        StatusCode  = SQLMediaProviderStatus.Rejected,
                        Id = Guid.NewGuid(),
                        Comment = "Neprimerna vsebina"
                },
                new MediaHistory() {
                        Created = DateTime.Now.AddDays(-60),
                        Publisher = "Dean podgornik",
                        StatusCode  = SQLMediaProviderStatus.Rejected,
                        Id = Guid.NewGuid(),
                        Comment = "Neprimerna vsebina"
                },
                new MediaHistory() {
                        Created = DateTime.Now.AddDays(-60),
                        Publisher = "Dean podgornik",
                        StatusCode  = SQLMediaProviderStatus.Rejected,
                        Id = Guid.NewGuid(),
                        Comment = "Neprimerna vsebina"
                },
                new MediaHistory() {
                        Created = DateTime.Now.AddDays(-60),
                        Publisher = "Dean podgornik",
                        StatusCode  = SQLMediaProviderStatus.Rejected,
                        Id = Guid.NewGuid(),
                        Comment = "Neprimerna vsebina"
                },
                new MediaHistory() {
                        Created = DateTime.Now.AddDays(-60),
                        Publisher = "Dean podgornik",
                        StatusCode  = SQLMediaProviderStatus.Rejected,
                        Id = Guid.NewGuid(),
                        Comment = "Neprimerna vsebina"
                },
                new MediaHistory() {
                        Created = DateTime.Now.AddDays(-60),
                        Publisher = "Dean podgornik",
                        StatusCode  = SQLMediaProviderStatus.Rejected,
                        Id = Guid.NewGuid(),
                        Comment = "Neprimerna vsebina"
                },
                new MediaHistory() {
                        Created = DateTime.Now.AddDays(-60),
                        Publisher = "Dean podgornik",
                        StatusCode  = SQLMediaProviderStatus.Rejected,
                        Id = Guid.NewGuid(),
                        Comment = "Neprimerna vsebina"
                },
                new MediaHistory() {
                        Created = DateTime.Now.AddDays(-60),
                        Publisher = "Dean podgornik",
                        StatusCode  = SQLMediaProviderStatus.Rejected,
                        Id = Guid.NewGuid(),
                        Comment = "Neprimerna vsebina"
                },
                new MediaHistory() {
                        Created = DateTime.Now.AddDays(-60),
                        Publisher = "Dean podgornik",
                        StatusCode  = SQLMediaProviderStatus.Rejected,
                        Id = Guid.NewGuid(),
                        Comment = "Neprimerna vsebina"
                },
                new MediaHistory() {
                        Created = DateTime.Now.AddDays(-60),
                        Publisher = "Dean podgornik",
                        StatusCode  = SQLMediaProviderStatus.Rejected,
                        Id = Guid.NewGuid(),
                        Comment = "Neprimerna vsebina"
                },
                new MediaHistory() {
                        Created = DateTime.Now.AddDays(-60),
                        Publisher = "Dean podgornik",
                        StatusCode  = SQLMediaProviderStatus.Rejected,
                        Id = Guid.NewGuid(),
                        Comment = "Neprimerna vsebina"
                }
            };
        }
    }
}
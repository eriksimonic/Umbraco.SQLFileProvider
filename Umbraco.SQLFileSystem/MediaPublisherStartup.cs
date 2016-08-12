using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;
using Umbraco.Core.Services;
using Umbraco.SQLFileSystem.Logic;
using Umbraco.Core.Models;

namespace Umbraco.SQLFileSystem
{
    public class MediaPublisherStartup : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            var logger = LoggerResolver.Current.Logger;
            var dbContext = ApplicationContext.Current.DatabaseContext;
            var helper = new DatabaseSchemaHelper(dbContext.Database, logger, dbContext.SqlSyntax);
            if(!helper.TableExist("MediaObjectStorage"))
            {
                helper.CreateTable<pocos.MediaObjectStorage>(false);
            }

            
            MediaService.Created += MediaService_Created;
            MediaService.Saving += MediaService_Saving;
            MediaService.Saved += MediaService_Saved;
            MediaService.Deleted += MediaService_Deleted;
            

        }

        private void MediaService_Created(IMediaService sender, Core.Events.NewEventArgs<Core.Models.IMedia> e)
        {
            throw new NotImplementedException();
        }

        private void MediaService_Saving(IMediaService sender, Core.Events.SaveEventArgs<Core.Models.IMedia> e)
        {
            throw new NotImplementedException();
        }

        private void MediaService_Deleted(IMediaService sender, Core.Events.DeleteEventArgs<Core.Models.IMedia> e)
        {
            e.DeletedEntities.ForEach(entity =>
            {
                if (entity.HasProperty("umbracoFile"))
                {

                    using (var fsr = new FilestreamRepository())
                    {
                        fsr.DeleteByMediaId(entity.Id);
                    }
                }

            });
        }

        private void MediaService_Saved(IMediaService sender, Core.Events.SaveEventArgs<IMedia> e)
        {
            e.SavedEntities.ForEach(entity =>
            {
                if (entity.HasProperty("umbracoFile"))
                {
                    ((IMedia)entity).SetValue("", "", HttpContext.Current.Request.Files[0].InputStream);
                    using (var fsr = new FilestreamRepository())
                    {
                        fsr.UpdateMediaWithId(entity.GetValue("umbracoFile").ToString() , entity.Id);
                    }
                }

            });
            //throw new NotImplementedException();
        }
    }
}
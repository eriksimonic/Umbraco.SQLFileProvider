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


//using umbraco.cms.businesslogic.web;
//using umbraco.NodeFactory;
//using umbraco.cms.businesslogic.member;
//using umbraco.cms.businesslogic.datatype;

namespace Umbraco.SQLFileSystem
{
    public class MediaPublisherStartup : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {

            CheckMediaTypes();


            var logger = LoggerResolver.Current.Logger;
            var dbContext = ApplicationContext.Current.DatabaseContext;
            var helper = new DatabaseSchemaHelper(dbContext.Database, logger, dbContext.SqlSyntax);
            if (!helper.TableExist("MediaObjectStorage"))
            {
                helper.CreateTable<pocos.MediaObjectStorage>(false);
            }

            //MediaService.Creating += MediaService_Creating;
            MediaService.Created += MediaService_Created;
            //MediaService.Saving += MediaService_Saving;
            MediaService.Saved += MediaService_Saved;
            MediaService.Deleted += MediaService_Deleted;


        }

     

        private void CheckMediaTypes()
        {
            var svc = ApplicationContext.Current.Services.ContentTypeService;
            svc.GetAllMediaTypes().ForEach(mType => {

                if (!mType.PropertyTypeExists(Constants.MEDIA_ONVARIANT_ID) && 
                    mType.PropertyTypeExists(Umbraco.Core.Constants.Conventions.Media.File))
                {
                    var property = new PropertyType(new DataTypeDefinition(-1, "Umbraco.NoEdit"), Constants.MEDIA_ONVARIANT_ID);
                    property.Name = "Invariant ID";
                    mType.AddPropertyType(property,mType.PropertyGroups.FirstOrDefault().Name);
                    svc.Save(mType);
                }
            });

            //ContentTypeService.
        }

        private void MediaService_Created(IMediaService sender, Core.Events.NewEventArgs<Core.Models.IMedia> e)
        {
            if (e.Entity.HasProperty(Constants.MEDIA_ONVARIANT_ID) &&
               string.IsNullOrEmpty(e.Entity.GetValue<string>(Constants.MEDIA_ONVARIANT_ID)))
            {
                e.Entity.SetValue(Constants.MEDIA_ONVARIANT_ID, Guid.NewGuid().ToString());
            }
        }

        private void MediaService_Deleted(IMediaService sender, Core.Events.DeleteEventArgs<Core.Models.IMedia> e)
        {
            e.DeletedEntities.ForEach(entity =>
            {
                if (entity.HasProperty(Umbraco.Core.Constants.Conventions.Media.File) &&
                    entity.HasProperty(Constants.MEDIA_ONVARIANT_ID))
                {
                    using (var fsr = new FilestreamRepository())
                    {
                        fsr.DeleteByMediaId(Guid.Parse( entity.GetValue<string>(Constants.MEDIA_ONVARIANT_ID) ));
                    }
                }

            });
        }

        private void MediaService_Saved(IMediaService sender, Core.Events.SaveEventArgs<IMedia> e)
        {
            e.SavedEntities.ForEach(entity =>
            {
                if (entity.HasProperty(Umbraco.Core.Constants.Conventions.Media.File))
                {
                    using (var fsr = new FilestreamRepository())
                    {
                        fsr.UpdateMediaWithId(entity.GetValue(Umbraco.Core.Constants.Conventions.Media.File).ToString() , 
                                entity.GetValue<Guid>(Constants.MEDIA_ONVARIANT_ID));
                    }
                }

            });
            //throw new NotImplementedException();
        }
    }
}
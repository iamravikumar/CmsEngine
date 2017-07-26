﻿using AutoMapper;
using CmsEngine.Data.AccessLayer;
using CmsEngine.Data.EditModels;
using CmsEngine.Data.Models;
using CmsEngine.Data.ViewModels;
using CmsEngine.Utils;
using System;
using System.Linq;

namespace CmsEngine.Services
{
    public class WebsiteService : BaseService<Website>
    {
        public WebsiteService(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {
        }

        public override ReturnValue BulkDelete(Guid[] id)
        {
            var returnValue = new ReturnValue();
            try
            {
                Repository.BulkUpdate(q => id.Contains(q.VanityId), u => new Website { IsDeleted = true });

                returnValue.IsError = false;
                returnValue.Message = string.Format("Selected items deleted at {0}.", DateTime.Now.ToString("d"));
            }
            catch
            {
                returnValue.IsError = true;
                returnValue.Message = "An error has occurred while deleting the website";
                throw;
            }

            return returnValue;
        }

        public override ReturnValue Delete(Guid id)
        {
            var returnValue = new ReturnValue();
            try
            {
                var website = this.GetAll().Where(q => q.VanityId == id).FirstOrDefault();
                returnValue = this.Delete(website);
            }
            catch
            {
                returnValue.IsError = true;
                returnValue.Message = "An error has occurred while deleting the website";
                throw;
            }

            return returnValue;
        }

        public override ReturnValue Delete(int id)
        {
            var returnValue = new ReturnValue();
            try
            {
                var website = this.GetAll().Where(q => q.Id == id).FirstOrDefault();
                returnValue = this.Delete(website);
            }
            catch
            {
                returnValue.IsError = true;
                returnValue.Message = "An error has occurred while deleting the website";
                throw;
            }

            return returnValue;
        }

        public override ReturnValue Save(IEditModel editModel)
        {
            var returnValue = new ReturnValue
            {
                IsError = false,
                Message = $"Website '{((WebsiteEditModel)editModel).Name}' saved."
            };

            try
            {
                PrepareForSaving(editModel);

                UnitOfWork.Save();
            }
            catch (Exception ex)
            {
                returnValue.IsError = true;
                returnValue.Message = "An error has occurred while saving the website";
                returnValue.Exception = ex.Message;
                throw;
            }

            return returnValue;
        }

        public override IEditModel SetupEditModel()
        {
            return new WebsiteEditModel();
        }

        protected override IEditModel SetupEditModel(Website item)
        {
            return Mapper.Map<Website, WebsiteEditModel>(item);
        }

        protected override IViewModel SetupViewModel(Website item)
        {
            return Mapper.Map<Website, WebsiteViewModel>(item);
        }

        protected override ReturnValue Delete(Website item)
        {
            var returnValue = new ReturnValue();
            try
            {
                if (item != null)
                {
                    item.IsDeleted = true;
                    Repository.Update(item);
                }

                UnitOfWork.Save();
                returnValue.IsError = false;
                returnValue.Message = string.Format("Website '{0}' deleted at {1}.", item.Name, DateTime.Now.ToString("d"));
            }
            catch
            {
                returnValue.IsError = true;
                returnValue.Message = "An error has occurred while deleting the website";
                throw;
            }

            return returnValue;
        }

        protected override void PrepareForSaving(IEditModel editModel)
        {
            Website website;

            if (editModel.IsNew)
            {
                website = Mapper.Map<WebsiteEditModel, Website>((WebsiteEditModel)editModel);
                Repository.Insert(website);
            }
            else
            {
                website = GetById(editModel.VanityId);
                Mapper.Map((WebsiteEditModel)editModel, website);

                Repository.Update(website);
            }
        }
    }
}

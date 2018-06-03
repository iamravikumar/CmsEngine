using System.Collections.Generic;
using AutoMapper;
using CmsEngine.Data.AccessLayer;
using CmsEngine.Data.Models;
using CmsEngine.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CmsEngine.Ui.Controllers
{
    public class BaseController : Controller
    {
        protected readonly CmsService service;
        protected readonly InstanceViewModel instance;

        public BaseController(IUnitOfWork uow, IMapper mapper, IHttpContextAccessor hca, UserManager<ApplicationUser> userManager)
        {
            service = new CmsService(uow, mapper, hca, userManager);
            instance = service.Instance;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            instance.PagedPosts = (IEnumerable<PostViewModel>)service.GetAllPostsReadOnly(4);
            instance.LatestPosts = (IEnumerable<PostViewModel>)service.GetAllPostsReadOnly(3);
            instance.Pages = (IEnumerable<PageViewModel>)service.GetAllPagesReadOnly();
            instance.Categories = (IEnumerable<CategoryViewModel>)service.GetAllCategoriesReadOnly();
            instance.Tags = (IEnumerable<TagViewModel>)service.GetAllTagsReadOnly();
        }
    }
}

namespace Steep.Web.Controllers
{
    using System.Web.Mvc;
    using AutoMapper;
    using Infrastructure.Mapping;
    using Services.Web;
    using Microsoft.AspNet.Identity;
    public abstract class BaseController : Controller
    {
        public ICacheService Cache { get; set; }

        public string UserId
        {
            get { return this.User.Identity.GetUserId(); }
        }

        protected IMapper Mapper
        {
            get
            {
                return AutoMapperConfig.Configuration.CreateMapper();
            }
        }
    }
}

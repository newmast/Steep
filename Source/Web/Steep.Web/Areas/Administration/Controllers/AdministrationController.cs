namespace Steep.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using Steep.Common;
    using Steep.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class AdministrationController : BaseController
    {
    }
}

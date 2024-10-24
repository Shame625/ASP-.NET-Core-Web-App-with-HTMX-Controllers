using Htmx;
using HTMXTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace HTMXTest.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult ViewOrPartial(object model)
        {
            string viewName = ControllerContext.ActionDescriptor.ActionName;

            return ViewOrPartial(viewName, model);
        }

        protected IActionResult ViewOrPartial(string viewName = null, object model = null)
        {
            if (viewName == null)
            {
                viewName = ControllerContext.ActionDescriptor.ActionName;
            }

            var _viewLocator = HttpContext.RequestServices.GetRequiredService<IViewLocatorService>();
            var resolvedViewName = _viewLocator.FindView(viewName);
            if (Request.IsHtmx())
            {
                return PartialView(resolvedViewName, model);
            }

            return View(resolvedViewName, model);
        }
    }
}

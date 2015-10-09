using System.Web.Mvc;
using Models;

namespace Validation.Controllers
{

    public class NestedModelController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            NestedVM vm = new NestedVM();
            vm.ContactVM = new ContactVM();

            return View("~/Views/Home/NestedModelBinding.cshtml", vm);
        }

        [HttpPost]
        public ActionResult Index(NestedVM vm)
        {



            if (ModelState.IsValid)
                return Json(new { valid = ModelState.IsValid });
            else
            {
                var errorList = ModelState.ToModelStateJSON();

                return Json(new { valid = ModelState.IsValid, validationErrors = errorList });
            }                
        }

    }
}

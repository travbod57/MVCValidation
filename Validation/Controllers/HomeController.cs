using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Models;

namespace Validation.Controllers
{
    public static class Extensions
    {
        public static IEnumerable<ValidationDto> ToModelStateJSON(this ModelStateDictionary modelStateDictionary)
        {
            return modelStateDictionary.Where(x => x.Value.Errors.Count > 0).Select(x => new ValidationDto() { ReportingLevel = string.IsNullOrEmpty(x.Key) ? "Model" : "Field", ValidationMessages = x.Value.Errors.Select(e => new ValidationMessageDto() { Name = x.Key, Message = e.ErrorMessage }) });
        }
    }

    public class ValidationDto
    {
        public ValidationDto()
	    {
            ValidationMessages = new List<ValidationMessageDto>();
	    }

        public string ReportingLevel { get; set; }
        public IEnumerable<ValidationMessageDto> ValidationMessages { get; set; }
    }

    public class ValidationMessageDto
    {
        public string Name { get; set; }
        public string Message { get; set; }
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            PageVM vm = new PageVM()
            {
                Person = new Person(),
                Book = new Book()
            };

            List<Title> Titles = new List<Title>() { 
                    new Title() { Id = 1, Name = "Mr" },
                    new Title() { Id = 2, Name = "Mrs" }, 
                    new Title() { Id = 3, Name = "Miss" }
                };

            ViewBag.Titles = Titles;

            return View("~/Views/Home/Person.cshtml", vm);
        }

        [HttpPost]
        public ActionResult Index(Person vm)
        {
            //ModelState.AddModelError(string.Empty, "Class level Error");
            //ModelState.AddModelError(string.Empty, "Class level Error 2");

            if (ModelState.IsValid)
                return Json(new { valid = ModelState.IsValid });
            else
            {
                var errorList = ModelState.ToModelStateJSON();

                return Json(new { valid = ModelState.IsValid, validationErrors = errorList });
            }                
        }

        [HttpPost]
        public ActionResult Book(Book vm)
        {
            ModelState.AddModelError(string.Empty, "Class level Error Book");

            if (ModelState.IsValid)
                return Json(new { valid = ModelState.IsValid });
            else
            {
                var errorList = ModelState.ToModelStateJSON();

                return Json(new { valid = ModelState.IsValid, validationErrors = errorList });
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}

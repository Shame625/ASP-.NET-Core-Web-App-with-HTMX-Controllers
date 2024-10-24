using HTMXTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace HTMXTest.Controllers
{
    public class PersonController : BaseController
    {

        [HttpPost]
        public IActionResult SubmitForm(PersonModel model)
        {
            if (!ModelState.IsValid)
            {
                return ViewOrPartial("PersonForm", model);
            }

            TempData["SuccessMessage"] = "Form submitted successfully!";
            return ViewOrPartial("PersonForm", model);
        }

    }
}

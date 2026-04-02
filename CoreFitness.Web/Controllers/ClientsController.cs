using Microsoft.AspNetCore.Mvc;
using CoreFitness.Web.Models;



namespace CoreFitness.Web.Controllers
{
    public class ClientsController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
        

        [HttpPost]
        public IActionResult Create(CreateClientFormModel model)
        {
            if (!ModelState.IsValid)
            return View(model);

            return View();
        }

    }


}

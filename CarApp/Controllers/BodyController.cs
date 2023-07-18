using CarApp.Data;
using CarApp.Dto;
using CarApp.Entities;
using CarApp.Interfaces;
using CarApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarApp.Controllers
{
    public class BodyController : Controller
    {

        private readonly ILogger<BodyController> _logger;
        private readonly IBody bodyType;
        private readonly CarAppContext ctx;

        public BodyController(ILogger<BodyController> logger, IBody bodyType)
        {
            _logger = logger;
            this.bodyType = bodyType;
        }

        public async Task<IActionResult> Index()
        {
            var types = await bodyType.GetAll();
            return View(types);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(CarBodyType body)
        {
            if (ModelState.IsValid)
            {
                await bodyType.AddNew(body.Name);

                return RedirectToAction("Index");
            }
            else
            {
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        // Afișează mesajele de eroare pentru depanare
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }
            return View(body);
        }


        //GET
        public async Task<IActionResult> Update(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var bodyFromDb = await bodyType.GetById(id);

            if (bodyFromDb == null)
            {
                return NotFound();
            }
            return View(bodyFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CarBodyType body)
        {
            if (ModelState.IsValid)
            {
                await bodyType.Update(body);

                return RedirectToAction("Index");
            }
            else
            {
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        // Afișează mesajele de eroare pentru depanare
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }
            return View(body);
        }


        //GET
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var bodyFromDb = await bodyType.GetById(id);

            if (bodyFromDb == null)
            {
                return NotFound();
            }
            return View(bodyFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(CarBodyType body)
        {
            /* var brand = await carBrand.GetById(id);

             if (brand == null)
             {
                 return NotFound();
             } */

            await bodyType.Delete(body);

            return RedirectToAction("Index");

        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

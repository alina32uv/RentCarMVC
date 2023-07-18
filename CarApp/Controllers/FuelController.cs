using CarApp.Data;
using CarApp.Dto;
using CarApp.Entities;
using CarApp.Interfaces;
using CarApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarApp.Controllers
{
    public class FuelController : Controller
    {

        private readonly ILogger<FuelController> _logger;
        private readonly IFuel fuelType;
        private readonly CarAppContext ctx;

        public FuelController(ILogger<FuelController> logger, IFuel fuelType)
        {
            _logger = logger;
            this.fuelType = fuelType;
        }

        public async Task<IActionResult> Index()
        {
            var types = await fuelType.GetAll();
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
        public async Task<IActionResult> CreateAsync(Fuel fuel)
        {
            if (ModelState.IsValid)
            {
                await fuelType.AddNew(fuel.Name);

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
            return View(fuel);
        }


        //GET
        public async Task<IActionResult> Update(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var fuelFromDb = await fuelType.GetById(id);

            if (fuelFromDb == null)
            {
                return NotFound();
            }
            return View(fuelFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Fuel fuel)
        {
            if (ModelState.IsValid)
            {
                await fuelType.Update(fuel);

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
            return View(fuel);
        }


        //GET
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var fuelFromDb = await fuelType.GetById(id);

            if (fuelFromDb == null)
            {
                return NotFound();
            }
            return View(fuelFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(Fuel fuel)
        {
            /* var brand = await carBrand.GetById(id);

             if (brand == null)
             {
                 return NotFound();
             } */

            await fuelType.Delete(fuel);

            return RedirectToAction("Index");

        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

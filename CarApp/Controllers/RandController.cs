using CarApp.Data;
using CarApp.Dto;
using CarApp.Entities;
using CarApp.Interfaces;
using CarApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarApp.Controllers
{
    public class RandController : Controller
    {
        
        private readonly ILogger<RandController> _logger;
        private readonly ICarBrand carBrand;
        private readonly CarAppContext ctx;

        public RandController(ILogger<RandController> logger, ICarBrand carBrand)
        {
            _logger = logger;
            this.carBrand = carBrand;
        }

        public async Task<IActionResult> Index()
        {
            var brands = await carBrand.GetAll();
            return View(brands);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Brand brand)
        {
           if (ModelState.IsValid)
            {
                await carBrand.AddNew(brand.Name);

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
            return View(brand);
        }


        //GET
        public async Task<IActionResult> Update(int id)
        {
            if(id==null || id==0)
            {
                return NotFound();
            }
            var brandFromDb = await carBrand.GetById(id);

            if(brandFromDb == null)
            {
                return NotFound();
            }
            return View( brandFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Brand brand)
        {
            if (ModelState.IsValid)
            {
                await carBrand.Update(brand);

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
                    return View(brand);
        }


        //GET
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var brandFromDb = await carBrand.GetById(id);

            if (brandFromDb == null)
            {
                return NotFound();
            }
            return View(brandFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(Brand brand)
        {
           /* var brand = await carBrand.GetById(id);

            if (brand == null)
            {
                return NotFound();
            } */

            await carBrand.Delete(brand);

                return RedirectToAction("Index");
           
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

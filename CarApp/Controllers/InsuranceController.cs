using CarApp.Data;
using CarApp.Dto;
using CarApp.Entities;
using CarApp.Interfaces;
using CarApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarApp.Controllers
{
    public class InsuranceController : Controller
    {

        private readonly ILogger<InsuranceController> _logger;
        private readonly IInsurance insuranceType;
        private readonly CarAppContext ctx;

        public InsuranceController(ILogger<InsuranceController> logger, IInsurance insuranceType)
        {
            _logger = logger;
            this.insuranceType = insuranceType;
        }

        public async Task<IActionResult> Index()
        {
            var types = await insuranceType.GetAll();
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
        public async Task<IActionResult> CreateAsync(Insurance insurance)
        {
            if (ModelState.IsValid)
            {
                await insuranceType.AddNew(insurance.Name);

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
            return View(insurance);
        }


        //GET
        public async Task<IActionResult> Update(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var insuranceFromDb = await insuranceType.GetById(id);

            if (insuranceFromDb == null)
            {
                return NotFound();
            }
            return View(insuranceFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Insurance insurance)
        {
            if (ModelState.IsValid)
            {
                await insuranceType.Update(insurance);

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
            return View(insurance);
        }


        //GET
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var insuranceFromDb = await insuranceType.GetById(id);

            if (insuranceFromDb == null)
            {
                return NotFound();
            }
            return View(insuranceFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(Insurance insurance)
        {
            /* var brand = await carBrand.GetById(id);

             if (brand == null)
             {
                 return NotFound();
             } */

            await insuranceType.Delete(insurance);

            return RedirectToAction("Index");

        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using CarApp.Data;
using CarApp.Dto;
using CarApp.Entities;
using CarApp.Interfaces;
using CarApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarApp.Controllers
{
    public class TransmissionController : Controller
    {
        
        private readonly ILogger<TransmissionController> _logger;
        private readonly ITransmission transmission;
        private readonly CarAppContext ctx;

        public TransmissionController(ILogger<TransmissionController> logger, ITransmission transmission)
        {
            _logger = logger;
            this.transmission = transmission;
        }

        public async Task<IActionResult> Index()
        {
            var types = await transmission.GetAll();
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
        public async Task<IActionResult> CreateAsync(Transmission transmissiontype)
        {
            if (ModelState.IsValid)
            {
                await transmission.AddNew(transmissiontype.Type);

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
            return View(transmissiontype);
        }

        //GET
        public async Task<IActionResult> Update(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var typeFromDb = await transmission.GetById(id);

            if (typeFromDb == null)
            {
                return NotFound();
            }
            return View(typeFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Transmission transmissiontype)
        {
            if (ModelState.IsValid)
            {
                await transmission.Update(transmissiontype);

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
            return View(transmissiontype);
        }



        //GET
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var typeFromDb = await transmission.GetById(id);

            if (typeFromDb == null)
            {
                return NotFound();
            }
            return View(typeFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(Transmission types)
        {
            /* var brand = await carBrand.GetById(id);

             if (brand == null)
             {
                 return NotFound();
             } */

            await transmission.Delete(types);

            return RedirectToAction("Index");

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

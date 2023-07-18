using CarApp.Data;
using CarApp.Dto;
using CarApp.Entities;
using CarApp.Interfaces;
using CarApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarApp.Controllers
{
    public class StatusController : Controller
    {
        
        private readonly ILogger<StatusController> _logger;
        private readonly IStatus status;
        private readonly CarAppContext ctx;

        public StatusController(ILogger<StatusController> logger, IStatus status)
        {
            _logger = logger;
            this.status = status;
        }

        public async Task<IActionResult> Index()
        {
            var types = await status.GetAll();
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
        public async Task<IActionResult> CreateAsync(Status type)
        {
           if (ModelState.IsValid)
            {
                await status.AddNew(type.Name);

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
            return View(type);
        }


        //GET
        public async Task<IActionResult> Update(int id)
        {
            if(id==null || id==0)
            {
                return NotFound();
            }
            var statusFromDb = await status.GetById(id);

            if(statusFromDb == null)
            {
                return NotFound();
            }
            return View( statusFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Status type)
        {
            if (ModelState.IsValid)
            {
                await status.Update(type);

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
                    return View(type);
        }


        //GET
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var statusFromDb = await status.GetById(id);

            if (statusFromDb == null)
            {
                return NotFound();
            }
            return View(statusFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(Status type)
        {
           /* var brand = await carBrand.GetById(id);

            if (brand == null)
            {
                return NotFound();
            } */

            await status.Delete(type);

                return RedirectToAction("Index");
           
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

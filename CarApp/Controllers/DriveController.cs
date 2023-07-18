using CarApp.Data;
using CarApp.Dto;
using CarApp.Entities;
using CarApp.Interfaces;
using CarApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarApp.Controllers
{
    public class DriveController : Controller
    {
        
        private readonly ILogger<DriveController> _logger;
        private readonly IDrive driveType;
        private readonly CarAppContext ctx;

        public DriveController(ILogger<DriveController> logger, IDrive driveType)
        {
            _logger = logger;
            this.driveType = driveType;
        }

        public async Task<IActionResult> Index()
        {
            var types = await driveType.GetAll();
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
        public async Task<IActionResult> CreateAsync(Drive drive)
        {
           if (ModelState.IsValid)
            {
                await driveType.AddNew(drive.Name);

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
            return View(drive);
        }


        //GET
        public async Task<IActionResult> Update(int id)
        {
            if(id==null || id==0)
            {
                return NotFound();
            }
            var typeFromDb = await driveType.GetById(id);

            if(typeFromDb == null)
            {
                return NotFound();
            }
            return View( typeFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Drive drive)
        {
            if (ModelState.IsValid)
            {
                await driveType.Update(drive);

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
                    return View(drive);
        }


        //GET
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var typeFromDb = await driveType.GetById(id);

            if (typeFromDb == null)
            {
                return NotFound();
            }
            return View(typeFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(Drive drive)
        {
           /* var brand = await carBrand.GetById(id);

            if (brand == null)
            {
                return NotFound();
            } */

            await driveType.Delete(drive);

                return RedirectToAction("Index");
           
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

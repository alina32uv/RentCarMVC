
using CarApp.Data;
using CarApp.Dto;
using CarApp.Interfaces;
using CarApp.Models;
using CarApp.Pages.Drives.Commands;
using CarApp.Pages.Drives.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarApp.Pages.Drives
{
    public class DriveController : Controller
    {

        private readonly ILogger<DriveController> _logger;
        private readonly IDrive driveType;
        private readonly CarAppContext ctx;
        private readonly IMediator _mediator;

        public DriveController(ILogger<DriveController> logger, IDrive driveType, IMediator mediator)
        {
            _logger = logger;
            this.driveType = driveType;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var types = await _mediator.Send(new GetDriveQuery());
            return View("/Pages/Drives/Views/Index.cshtml",types);
        }

        //GET
        public IActionResult Create()
        {
            return View("/Pages/Drives/Views/Create.cshtml");
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Drive drive)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new CreateDriveCommand(drive.Name));

                return RedirectToAction("Index");
            }
            else
            {
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }
            return View(drive);
        }


        //GET
        public async Task<IActionResult> Update(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var typeFromDb = await _mediator.Send(new GetDriveByIdQuery(id));

            if (typeFromDb == null)
            {
                return NotFound();
            }
            return View("/Pages/Drives/Views/Update.cshtml",typeFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Drive drive)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new UpdateDiveCommand(drive.DriveId, drive.Name));

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
            var typeFromDb = await _mediator.Send(new GetDriveByIdQuery(id));

            if (typeFromDb == null)
            {
                return NotFound();
            }
            return View("/Pages/Drives/Views/Delete.cshtml",typeFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(Drive drive)
        {
            await _mediator.Send(new DeleteDriveCommand(drive.DriveId, drive.Name));

            return RedirectToAction("Index");

        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

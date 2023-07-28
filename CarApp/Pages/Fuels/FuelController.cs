using CarApp.Pages.Fuels.Commands;
using CarApp.Data;
using CarApp.Dto;
using CarApp.Interfaces;
using CarApp.Migrations;
using CarApp.Models;
using CarApp.Pages.Fuels.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarApp.Pages.Fuels
{
    public class FuelController : Controller
    {

        private readonly ILogger<FuelController> _logger;
        private readonly IFuel fuelType;
        private readonly CarAppContext ctx;
        private readonly IMediator _mediator;

        public FuelController(ILogger<FuelController> logger, IFuel fuelType, IMediator mediator)
        {
            _logger = logger;
            this.fuelType = fuelType;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {

            var types = await _mediator.Send(new GetFuelQuery());

            return View(types);
        }

        //GET
        public IActionResult Create()
        {
            return View("/Pages/Fuels/Fuel/Create.cshtml");
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Fuel fuel)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new CreateFuelCommand(fuel.Name));

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
            var fuelFromDb = await _mediator.Send(new GetByIdFuelQuery(id));

            if (fuelFromDb == null)
            {
                return NotFound();
            }
            return View("/Pages/Fuels/Fuel/Update.cshtml", fuelFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Fuel fuel)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new UpdateFuelCommand(fuel.FuelId, fuel.Name));

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
            var fuelFromDb = await _mediator.Send(new GetByIdFuelQuery(id));

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


            await _mediator.Send(new DeleteFuelCommand(fuel.FuelId, fuel.Name));

            return RedirectToAction("Index");

        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

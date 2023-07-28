
using CarApp.Data;
using CarApp.Dto;
using CarApp.Entities;
using CarApp.Interfaces;
using CarApp.Migrations;
using CarApp.Models;
using CarApp.Pages.Body.Commands;
using CarApp.Pages.Body.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarApp.Pages.Body.BodyController
{
    public class BodyController : Controller
    {

        private readonly ILogger<BodyController> _logger;
        private readonly IBody bodyType;
        private readonly CarAppContext ctx;
        private readonly IMediator _mediator;

        public BodyController(ILogger<BodyController> logger, IBody bodyType, IMediator mediator)
        {
            _logger = logger;
            this.bodyType = bodyType;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var types = await _mediator.Send(new GetBodyQuery());
            return View("/Pages/Body/Views/Index.cshtml",types);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Entities.CarBodyType body)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new CreateBodyCommands(body.Name));

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
            return View(body);
        }


        //GET
        public async Task<IActionResult> Update(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var bodyFromDb = await _mediator.Send(new GetBodyByIdQuery(id));

            if (bodyFromDb == null)
            {
                return NotFound();
            }
            return View(bodyFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Entities.CarBodyType body)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(new UpdateBodyCommands(body.CarBodyTypeId, body.Name));

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
            var bodyFromDb = await _mediator.Send(new GetBodyByIdQuery(id));

            if (bodyFromDb == null)
            {
                return NotFound();
            }
            return View(bodyFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(Entities.CarBodyType body)
        {
           
            await _mediator.Send(new DeleteBodyCommands(body.CarBodyTypeId, body.Name));

            return RedirectToAction("Index");

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

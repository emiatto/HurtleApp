using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hurtle.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SelfRunning.Models.Hurtle;

namespace SelfRunning.Controllers
{
    public class HurtleController : Controller
    {
        private readonly IAttivitaRepository _attivitaRepository;

        public HurtleController(IAttivitaRepository attivitaRepository)
        {
            _attivitaRepository = attivitaRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Allenamento([FromRoute] int id) // allenamento/nuovoallenamento/2 <-int id from route
        {
            var activity = new AttivitaViewModel(_attivitaRepository.Get(id));

            if (activity == null)
                return NotFound();


            return View(activity);
        }
    }
}
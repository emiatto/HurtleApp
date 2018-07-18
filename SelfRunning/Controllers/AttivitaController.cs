using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoLocation.Data;
using Hurtle.Data;
using Hurtle.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SelfRunning.Models;
using SelfRunning.Models.Hurtle;

namespace SelfRunning.Controllers
{
    public class AttivitaController : Controller
    {
        private readonly IAttivitaRepository _attivitaRepository;
        private readonly IRunnerRepository _runnerRepository;
        private readonly ITelemetriaRepository _telemetriaRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public AttivitaController(IAttivitaRepository attivitaRepository, IRunnerRepository runnerRepository, ITelemetriaRepository telemetriaRepository, UserManager<ApplicationUser> userManager)
        {
            _attivitaRepository = attivitaRepository;
            _runnerRepository = runnerRepository;
            _telemetriaRepository = telemetriaRepository;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult ElencoAttivita()
        {
            Runner runner = _runnerRepository.GetUserByUsername(User.Identity.Name);
                var activities = _attivitaRepository.GetByUser(runner.Id);

                var model = activities.Select(a => new AttivitaViewModel(a));

                return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult NuovaAttivita([FromQuery]string type)
        {

            var model = new AttivitaInsertViewModel();
            model.Tipologia = type.ToLower() == "allenamento" ? 1 : 2;
            
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult NuovaAttivita(AttivitaInsertViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.GetUserId(User);
                Runner runner = _runnerRepository.GetUserByUsername(User.Identity.Name);

                var activity = new Attivita()
                {
                    Titolo = model.Titolo,
                    IdRunner = runner.Id,
                    DataCreazione = model.DataCreazione,
                    Localita = model.Localita,
                    Tipologia = model.Tipologia
                };
                _attivitaRepository.Insert(activity);

                return RedirectToAction("ElencoAttivita");
            }

            ModelState.AddModelError("", "Errore generico");

            return View(model);

        }

        [Authorize]
        public IActionResult Delete([FromRoute]int id)
        {
            var activity = _attivitaRepository.Get(id);

            if (activity == null)
                return NotFound();

            _attivitaRepository.Delete(activity.Id);

            return RedirectToAction("ElencoAttivita");
        }

        [Authorize]
        public IActionResult DettaglioAttivita ([FromRoute]int id)
        {
            var activity = _attivitaRepository.Get(id);
            if (activity == null)
                return NotFound();

            var telemetries = _telemetriaRepository.GetByIdAttivita(id);
            if (telemetries == null)
                return NotFound();

            Runner runner = _runnerRepository.GetUserByUsername(User.Identity.Name);
            var model = new AttivitaDettaglioViewModel()
             {
                 Id = activity.Id,
                 Titolo = activity.Titolo,
                 IdRunner = runner.Id,
                 DataCreazione = activity.DataCreazione,
                 Localita = activity.Localita,
                 Tipologia = activity.Tipologia,
                 UriGara = activity.UriGara,
                 TelemetrieArray = telemetries

             };

            return View(model);
        }
    }
}
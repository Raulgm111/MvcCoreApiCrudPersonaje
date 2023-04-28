using Microsoft.AspNetCore.Mvc;
using MvcCoreApiCrudPersonaje.Models;
using MvcCoreApiCrudPersonaje.Services;

namespace MvcCoreApiCrudPersonaje.Controllers
{
    public class PersonajesController : Controller
    {
        private ServiceApiPersonaje service;

        public PersonajesController(ServiceApiPersonaje service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Personaje> personajes =
                await this.service.GetPersonajesASync();
            return View(personajes);
        }

        public async Task<IActionResult> Details(int idperso)
        {
            Personaje personaje =
                await this.service.FindPersonajeAsync(idperso);
            return View(personaje);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Personaje per)
        {
            await this.service.InsertPersonajesAsync
                (per.NombrePersonaje,per.Imagen,per.IdSerie);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int idperso)
        {
            Personaje personaje =
                await this.service.FindPersonajeAsync(idperso);
            return View(personaje);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Personaje per)
        {
            await this.service.UpdatePErsonajeASync
                (per.IdPersonaje,per.NombrePersonaje,per.Imagen,per.IdSerie);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int idperso)
        {
            await this.service.DeletePersonajeASync(idperso);
            return RedirectToAction("Index");
        }
    }
}

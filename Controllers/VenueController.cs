using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TEServerTest.Data;
using TEServerTest.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TEServerTest.Controllers
{
    public class VenueController : Controller
    {
        private readonly IVenueRepository venueRepository;

        public VenueController(IVenueRepository venueRepository)
        {
            this.venueRepository = venueRepository;
        }
        public async Task<IActionResult> Index()
        {
            var model = await venueRepository.GetVenuesAsync();
            return View(model);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Venue venue)
        {
            if (ModelState.IsValid)
            {
                await venueRepository.Create(venue);
                return RedirectToAction("index");
            }
            return View(venue);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await venueRepository.GetVenueAsync(id);

            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Venue venue)
        {
            if (id != venue.ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var updatedVenue = await venueRepository.Update(venue);
                return RedirectToAction("details", new { id = updatedVenue.ID });
            }

            return View(venue);

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await venueRepository.GetVenueAsync(id);

            if (venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await venueRepository.GetVenueAsync(id);

            if(venue == null)
            {
                return NotFound();
            }

            return View(venue);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venue = await venueRepository.GetVenueAsync(id);
            await venueRepository.Delete(venue);
            return RedirectToAction("index");
        }
    }
}
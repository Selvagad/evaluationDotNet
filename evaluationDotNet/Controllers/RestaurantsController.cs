using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using app.data.Model;
using evaluationDotNet.Models;
using evaluationDotNet.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace evaluationDotNet.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly RestaurantContext _context;
        
        public RestaurantsController(RestaurantContext context)
        {
            _context = context;
        }

        // GET: Restaurants
        public async Task<IActionResult> Index()
        {
            var restaurantList = new RestaurantService().getAllRestaurant();
            return View(await restaurantList);
        }

        // GET: Restaurants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var resultQwery = new RestaurantService().getRestaurantById(id);
            var restaurant = await resultQwery ;
            
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // GET: Restaurants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,name,phoneNumber,comment,email,address,note")] Restaurant restaurant)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (errors != null)
            {
                new RestaurantService().createRestaurant(restaurant);
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultQwery = new RestaurantService().getRestaurantById(id);
            var restaurant = await resultQwery; 
            
            if (restaurant == null)
            {
                return NotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,name,phoneNumber,comment,email,address")] Restaurant restaurant)
        {
            if (id != restaurant.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    new RestaurantService().editRestaurant(restaurant);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(restaurant.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultQwery = new RestaurantService().getRestaurantById(id);
            var restaurant = await resultQwery;
            
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            new RestaurantService().deleteRestaurant(id);
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurants.Any(e => e.ID == id);
        }

        public async Task<IActionResult> Best()
        {
            var restoListQwery = new RestaurantService().BestResto();
            var bestRestaurants = await restoListQwery;
            return View(bestRestaurants);
        }
        
        public async Task<IActionResult> NoteDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Notes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }
        
        public async Task<IActionResult> NoteEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Notes.FirstOrDefaultAsync(i => i.ID == id.Value);
            if (note == null)
            {
                return NotFound();
            }
            return View(note);
        }

        // POST: Note/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NoteEdit(int id, [Bind("ID,lastDate,notes,comment")] Note note)
        {
            if (id != note.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    new RestaurantService().editNote(note);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(note.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(NoteIndex));
            }
            return View(note);
        }
        
        public async Task<IActionResult> NoteIndex()
        {
            var restaurantList = new RestaurantService().getAllRestaurant();
            return View(await restaurantList);
        }


        public async Task<IActionResult> ExportDatabase()
        {
            new JSONService().ExportDataBase();
            return RedirectToAction(nameof(Index));
        }
        
        

    }
}

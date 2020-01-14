using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.data.Model;
using evaluationDotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace evaluationDotNet.Services
{
    public class RestaurantService
    {
        
        private readonly RestaurantContext _context;

        public RestaurantService()
        {
            _context = new RestaurantContext();
        }
        public async Task<List<Restaurant>> getAllRestaurant()
        {
            _context.Database.EnsureCreated();
            var restaurantList = await _context.Restaurants.Include(p => p.address).Include(p => p.note).ToListAsync();
            return restaurantList;
        }
        
        public async Task<Restaurant> getRestaurantById(int? id)
        {
            _context.Database.EnsureCreated();
            var restaurant = await _context.Restaurants.Include(p => p.address).Include(p => p.note)
                .FirstOrDefaultAsync(m => m.ID == id);
            return restaurant;
        }

        public async void createRestaurant(Restaurant restaurant)
        {
            _context.Database.EnsureCreated();
            _context.Add(restaurant);
            await _context.SaveChangesAsync();
        }
        
        public async void editRestaurant(Restaurant restaurant)
        {
            _context.Database.EnsureCreated();
            _context.Update(restaurant);
            await _context.SaveChangesAsync();
        }
        
        public async void deleteRestaurant(int id)
        {
            _context.Database.EnsureCreated();
            var restaurant = await _context.Restaurants.FindAsync(id);
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
        }

        public async void editNote(Note note)
        {
            _context.Database.EnsureCreated();
            _context.Update(note);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Restaurant>> BestResto()
        {
            _context.Database.EnsureCreated();
            var restaurantList = _context.Restaurants.Include(p => p.address).Include(p => p.note).ToList();
            var best = restaurantList.OrderByDescending(restaurant => restaurant.note.notes ).Take(5);

            return best;
        }
    }
}
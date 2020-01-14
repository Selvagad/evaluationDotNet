using System.Linq;
using System.Threading.Tasks;
using app.data.Model;
using evaluationDotNet.Models;
using evaluationDotNet.Services;
using NUnit.Framework;

namespace TestEvaluationDotNet
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test, Order(1)]
        public void Create1Restaurant()
        {
            var resto = new Restaurant();
            resto.name = "test";
            resto.phoneNumber = "047633431";
            resto.comment = "bon";
            resto.email = "test@free.fr";
            resto.note = new Note();
            resto.note.notes = 5;
            resto.note.lastDate = "24 Janvier 2005";
            resto.note.comment = "test";
            resto.address = new Address();
            resto.address.rue = "test rue";
            resto.address.ville = "Grenoble";
            resto.address.zipCode = 38000;
            
            var beforeCreation = new RestaurantService().getAllRestaurant().Result.Count;
            
            new RestaurantService().createRestaurant(resto);
            
            var afterCreation = new RestaurantService().getAllRestaurant().Result.Count;

            Assert.AreEqual(beforeCreation + 1 , afterCreation);
        }
        
        
        [Test, Order(2)]
        public async Task Modifier1Restaurant()
        {

            var lastBeforeModify = new RestaurantService().getAllRestaurant().Result.Last().ID;

            var restaurantQwery = new RestaurantService().getRestaurantById(lastBeforeModify);
            
            var restaurant = await restaurantQwery;
            
            Assert.AreEqual("test" , restaurant.name);

            restaurant.name = "test2";
            
            new RestaurantService().editRestaurant(restaurant);
            
            var lastAfterModify = new RestaurantService().getAllRestaurant().Result.Last().ID;
            
            var restaurantQweryAfterModify = new RestaurantService().getRestaurantById(lastAfterModify);
            
            var restaurantAfterModify = await restaurantQweryAfterModify;
            
            Assert.AreEqual("test2" , restaurantAfterModify.name);
        }
        
        [Test, Order(4)]
        public async Task Supprimer1Restaurant()
        {
            
            var countBeforeDelete  = new RestaurantService().getAllRestaurant().Result.Count();
            
            var lastBeforeDelete = new RestaurantService().getAllRestaurant().Result.Last().ID;

            var restaurantQwery = new RestaurantService().getRestaurantById(lastBeforeDelete);
            
            var restaurant = await restaurantQwery;
            
            new RestaurantService().deleteRestaurant(restaurant.ID);
            
            var countAfterDelete  = new RestaurantService().getAllRestaurant().Result.Count();
            
            Assert.AreEqual(countBeforeDelete - 1 , countAfterDelete);
        }
        
        [Test, Order(3)]
        public async Task Modifier1Note()
        {

            var lastBeforeModify = new RestaurantService().getAllRestaurant().Result.Last().ID;

            var restaurantQwery = new RestaurantService().getRestaurantById(lastBeforeModify);
            
            var restaurant = await restaurantQwery;
            
            Assert.AreEqual(5 , restaurant.note.notes);

            restaurant.note.notes = 3;
            
            new RestaurantService().editRestaurant(restaurant);
            
            var lastAfterModify = new RestaurantService().getAllRestaurant().Result.Last().ID;
            
            var restaurantQweryAfterModify = new RestaurantService().getRestaurantById(lastAfterModify);
            
            var restaurantAfterModify = await restaurantQweryAfterModify;
            
            Assert.AreEqual(3 , restaurantAfterModify.note.notes);
        }

    }
}
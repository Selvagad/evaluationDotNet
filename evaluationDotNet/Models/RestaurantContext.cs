using System;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using app.data.Model;
using Microsoft.EntityFrameworkCore;


namespace evaluationDotNet.Models
{
    public class RestaurantContext : DbContext
    {
        private string connectionString { get; set; }
        
        public RestaurantContext()
        {
        }
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var absoluteAppPath = Directory.GetParent(Directory.GetCurrentDirectory());
            Assembly? assembly = Assembly.GetEntryAssembly();
            string assemblyMessage = assembly.ToString();
            
            if (assemblyMessage.Contains("testhost"))
            {
                optionsBuilder.UseSqlite(@"Data Source="+absoluteAppPath+"/netcoreapp3.0/myDbRestaurant.db");
            }
            else
            {
                optionsBuilder.UseSqlite(@"Data Source="+absoluteAppPath+"/TestEvaluationDotNet/bin/Debug/netcoreapp3.0/myDbRestaurant.db");
            }
            
            
        }
        

        public DbSet<Restaurant> Restaurants { get; set; }
        
        public DbSet<Address> Addresses { get; set; }
         
        public DbSet<Note> Notes { get; set; }
        
        
    }
}
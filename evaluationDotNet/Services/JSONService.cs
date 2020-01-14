using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using app.data.Model;
using evaluationDotNet.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace evaluationDotNet.Services
{
    public class JSONService
    {
        
        private readonly RestaurantContext _context;
        
        public JSONService()
        {
            _context = new RestaurantContext();
        }
        
        
        public async void ExportDataBase()
        {
            using var dbCtxt = new RestaurantContext();
            dbCtxt.Database.EnsureCreated();
            var listeResto = dbCtxt.Restaurants.Include(p => p.address).Include(p => p.note).ToList();
            string json = JsonConvert.SerializeObject(listeResto);
            var absoluteAppPath = Directory.GetParent(Directory.GetCurrentDirectory());
            string path = "";
            Assembly? assembly = Assembly.GetEntryAssembly();
            string assemblyMessage = assembly.ToString();
            if (assemblyMessage.Contains("testhost"))
            {
                absoluteAppPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(absoluteAppPath.ToString()).ToString()).ToString());
                path = absoluteAppPath +"/evaluationDotNet/JSON/dumps/dump"+ $"{DateTime.Now:dd-MM-yyyy_HH-mm-ss-fff}" +".json";
            }
            else
            {
                path = absoluteAppPath +"/evaluationDotNet/JSON/dumps/dump"+ $"{DateTime.Now:dd-MM-yyyy_HH-mm-ss-fff}" +".json";
                
            }
            
            using (TextWriter tw = new StreamWriter(path))
            {
                tw.WriteLine(json);
            };
        }

        public async void importDatabase()
        {
            using var dbCtxt = new RestaurantContext();
            dbCtxt.Database.EnsureCreated();
            var json = File.ReadAllText("../../../../evaluationDotNet/JSON/databaseImport.json");
            var importData = JsonConvert.DeserializeObject<List<Restaurant>>(json);
            dbCtxt.Restaurants.AddRange(importData);
            dbCtxt.SaveChanges();
        }
    }
}
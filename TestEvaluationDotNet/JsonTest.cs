using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using evaluationDotNet.Models;
using app.data.Model;
using evaluationDotNet.Controllers;
using evaluationDotNet.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;

namespace TestEvaluationDotNet
{
    public class JsonTest
    {
        [SetUp]
        public void Setup()
        {
            
        }
        
        [Test,Order(1)]
        public void ImportTest()
        {
            var beforeImport = new RestaurantService().getAllRestaurant().Result.Count;
            
            new JSONService().importDatabase();
            
            var afterImport = new RestaurantService().getAllRestaurant().Result.Count;
            
            Assert.AreEqual(beforeImport + 7 , afterImport);
        }

        [Test,Order(2)]
        public async Task ExportTest()
        {
            var absoluteAppPath = Directory.GetParent(Directory.GetCurrentDirectory());
            absoluteAppPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(absoluteAppPath.ToString()).ToString()).ToString());
            var fullPath = absoluteAppPath + "/evaluationDotNet/JSON/dumps/";
            var beforeExport = Directory.GetFiles(fullPath , "*.json").Length;

            new JSONService().ExportDataBase();
            
            var afterExport = Directory.GetFiles(fullPath , "*.json").Length;
            
            Assert.AreEqual(beforeExport + 1 , afterExport);
        }
    }
}
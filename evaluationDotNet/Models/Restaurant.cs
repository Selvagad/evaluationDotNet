using System;
using System.Collections.Generic;
using evaluationDotNet.Models;

namespace app.data.Model
{
    public class Restaurant
    {
        public Restaurant()
        {
        }

        public int ID { get; set; }
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public string comment { get; set; }
        public string email { get; set; }
        public Address address { get; set; }
        public Note note { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Text;

namespace EdaVPuti.Models
{
    public class Food
    {
        public int id { get; set; }
        public int restaurantsId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public string cookingTime { get; set; }
        public int? cartId { get; set; }
    }
}

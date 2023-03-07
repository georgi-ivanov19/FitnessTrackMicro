using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealsAPI.Models
{
    public class Meal
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
        public string applicationUserId { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public double TotalCalories { get; set; }
        public double Protein { get; set; }
        public double Fats { get; set; }
        public double Carbohydrates { get; set; }
        public DateTime Date { get; set; }
    }
}

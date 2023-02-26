using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTrackMicro.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public double TotalCalories { get; set; }
        public double Protein { get; set; }
        public double Fats { get; set; }
        public double Carbohydrates { get; set; }
        public DateTime Date { get; set; }
    }
}

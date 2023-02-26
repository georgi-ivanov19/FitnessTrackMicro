using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTrackMicro.Models
{
    public class MealMacros
    {
        public double Calories;
        public double Protein;
        public double Carbs;
        public double Fats;

        public MealMacros(double cals, double protein, double carbs, double fats)
        {
            Calories = cals;
            Protein = protein;
            Carbs = carbs;
            Fats = fats;
        }
    }
}

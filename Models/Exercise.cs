using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTrackMicro.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int WorkoutId { get; set; }
        public int DefaultNumberOfSets { get; set; }
        public string TargetMuscle { get; set; } = string.Empty;
    }
}
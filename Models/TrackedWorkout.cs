using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTrackMicro.Models
{
    public class TrackedWorkout
    {
        public int Id { get; set; }
        public double TotalVolume { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsCompleted { get; set; }
        public List<ExerciseSet> ExerciseSetsCompleted { get; set; } = new List<ExerciseSet>();
        public int WorkoutId { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTrackMicro.Models
{
    public class Workout
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string DayOfWeek { get; set; } = string.Empty;
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();
        public DateTime DateLastCompleted { get; set; }
        public List<TrackedWorkout> TrackedWorkouts { get; set; } = new List<TrackedWorkout>();
    }
}
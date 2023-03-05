using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTrackMicro.Models
{
    public class AverageResults
    {
        public double? CurrentAverage { get; set; }
        public int CurrentCount { get; set; }
        public double? PreviousAverage { get; set; }
        public int PreviousCount { get; set; }
        public double? Difference { get; set; }
        public string ChangeDirection { get; set; }

        public AverageResults(double? currentAverage, int currentCount, double? previousAverage)
        {
            CurrentAverage = currentAverage != null ? Math.Round((double)currentAverage, 2) : null;
            CurrentCount = currentCount;
            PreviousAverage = previousAverage != null ? Math.Round((double)previousAverage, 2) : null;
            Difference = CurrentAverage != null && PreviousAverage != null ? Math.Round(Math.Abs((double)CurrentAverage - (double)PreviousAverage), 2) : 0;
            if(CurrentAverage > PreviousAverage)
            {
                ChangeDirection = "up";
            } else if (CurrentAverage < PreviousAverage)
            {
                ChangeDirection = "down";
            } else if (CurrentAverage == PreviousAverage && CurrentAverage != null && PreviousAverage != null)
            {
                ChangeDirection = "none";
            } else
            {
                ChangeDirection = "non-deterministic";
            }
        }
    }


}
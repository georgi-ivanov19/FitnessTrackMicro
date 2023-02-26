using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessTrackMicro.Models
{
    public class Measurement
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public double Value { get; set; }
        public string Unit { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tram.Common.Models.ZTP
{
    public class StopTimesZTP // stop_times.txt
    {
        public string TripID { get; set; } // trip_id
        public string Arrival { get; set; } // arrival_time
        public TimeSpan Departure { get; set; } // departure_time
        public string StopID { get; set; } // stop_id
    }
}

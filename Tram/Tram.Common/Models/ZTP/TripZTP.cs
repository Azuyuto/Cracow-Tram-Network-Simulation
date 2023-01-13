using System;
using System.Collections.Generic;
using System.Text;
using Tram.Common.Models.ZTP;

namespace TramNetwork.Common.Models.ZTP
{
    public class TripZTP
    {
        public LineZTP Line { get; set; }
        public string TripID { get; set; } // trip_id
        public string RouteID { get; set; } // route_id
        public string ServiceID { get; set; } // service_id
        public string Destination { get; set; } // trip_headsign

        public StopTimesZTP FirstStart { get; set; }
    }
}

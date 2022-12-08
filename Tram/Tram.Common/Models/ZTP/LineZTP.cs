using System;
using System.Collections.Generic;
using System.Text;
using Tram.Common.Models.Map;
using Tram.Common.Models.ZTP;

namespace TramNetwork.Common.Models.ZTP
{
    public class LineZTP // routes.txt
    {
        public string RouteID { get; set; } // route_id
        public string LineName { get; set; } // route_short_name
        public TramRoute TramRoute { get; set; }

        public List<TripZTP> Trips { get; set; }
        public HashSet<string> StopDictionary { get; set; }

        public LineZTP()
        {
            Trips = new List<TripZTP>();
            StopDictionary = new HashSet<string>();
            TramRoute = new TramRoute();
        }
    }
}

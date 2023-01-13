using System;
using System.Collections.Generic;

namespace Tram.Common.Models
{
    public class TramLine : ModelBase
    {
        public string TripID { get; set; }

        public string Name { get; set; }

        public List<Node> MainNodes { get; set; }
        
        public List<Departure> Departures { get; set; }

        public Departure LastDeparture { get; set; }

        public class Departure
        {
            public DateTime StartTime { get; set; }
            
            public List<float> NextStopIntervals { get; set; }
        }
    }
}

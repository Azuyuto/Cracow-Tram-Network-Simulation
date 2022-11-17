using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Text;

namespace TramNetwork.Common.Models.ZTP
{
    public class StopZTP
    {
        public string StopID { get; set; } // stop_id
        public string StopName { get; set; } // stop_name
        public Vector2 StopCoordinates { get; set; } // stop_lat, stop_lon
    }
}

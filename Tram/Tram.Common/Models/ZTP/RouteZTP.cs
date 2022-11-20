using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TramNetwork.Common.Models.ZTP;

namespace Tram.Common.Models.ZTP
{
    public class RouteZTP
    {
        public LineZTP Line { get; set; }
        public TripZTP Trip { get; set; }
        public List<StopTimesZTP> Stops { get; set; }

        public RouteZTP()
        {
            Line = new LineZTP();
            Trip = new TripZTP();
            Stops = new List<StopTimesZTP>();
        }
    }
}

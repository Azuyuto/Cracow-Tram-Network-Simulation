using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tram.Common.Models.Map
{
    public class TramStop
    {
        public string Name { get; set; }
        public Vector2 Coordinates { get; set; }

        public string StopID { get; set; }
    }
}

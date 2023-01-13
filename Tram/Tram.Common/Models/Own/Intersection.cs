using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tram.Common.Models.Own
{
    public class Intersection
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public Vector2 Coordinates { get; set; } // lat, lon
    }
}

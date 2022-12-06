using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tram.Common.Models.Map
{
    public class LineString
    {
        public List<Vector2> Coordinates { get; set; }

        public LineString()
        {
            Coordinates = new List<Vector2>();
        }
    }
}

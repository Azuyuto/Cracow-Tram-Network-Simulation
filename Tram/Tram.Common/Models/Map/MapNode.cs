using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tram.Common.Models.Map
{
    public class MapNode
    {
        public Vector2 Coordinates { get; set; }
        public bool IsTramStop { get; set; }
        public int StopID { get; set; }
    }
}

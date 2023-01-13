using Microsoft.DirectX;
using System.Collections.Generic;

namespace Tram.Common.Models
{
    public class TramIntersection : ModelBase
    {
        public List<Node> Nodes { get; set; }

        public Vehicle CurrentVehicle { get; set; }

        public Queue<Vehicle> Vehicles { get; set; }

        public Vector2 Coordinates { get; set; }
    }
}

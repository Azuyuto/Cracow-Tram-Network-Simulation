﻿using Microsoft.DirectX;
using System.Collections.Generic;
using Tram.Common.Enums;
using Tram.Common.Interfaces;

namespace Tram.Common.Models
{
    public class Node : ModelBase, IObjWithCoordinates
    {
        public string StopID { get; set; }

        public string StopName { get; set; }

        public Vector2 Coordinates { get; set; }

        public NodeType Type { get; set; }

        public TramIntersection Intersection { get; set; }

        public bool IsUnderground { get; set; }

        public Next Child { get; set; }

        public List<Next> Children { get; set; }

        public List<Vehicle> VehiclesOn { get; set; }

        public class Next
        {
            public Node Node { get; set; }

            public float Distance { get; set; }
        }
    }
}

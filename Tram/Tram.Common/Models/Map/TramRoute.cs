﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tram.Common.Models.Map
{
    public class TramRoute
    {
        public string Name { get; set; }
        public string TramNumber { get; set; }
        public List<LineString> LineStrings { get; set; }
        public List<MapNode> Nodes { get; set; }

        public TramRoute()
        {
            LineStrings = new List<LineString>();
            Nodes = new List<MapNode>();
        }
    }
}

﻿using System.Linq;
using Tram.Common.Enums;
using Tram.Common.Models;

namespace Tram.Common.Extensions
{
    public static class LineExtensions
    {
        public static Node.Next GetNextNode(this TramLine line, Node node)
        {
            if (node.Child != null)
            {
                return node.Child;
            }
            else if (node.Children != null && node.Children.Any())
            {
                return node.Children.First(ch => line.MainNodes.Any(mn => mn.Equals(ch.Node)));
                // powinno byc Single, ale daje First by dzialalo
            }

            return null;
        }

        public static float GetNextStopDistance(this TramLine line, Node node)
        {
            var next = line.GetNextNode(node);
            float distance = next.Distance;
            while (!(next.Node.Type == NodeType.TramStop && line.MainNodes.Any(n => n.Equals(next.Node))))
            {
                next = line.GetNextNode(next.Node);
                distance += next.Distance;
            }

            return distance;
        }
    }
}

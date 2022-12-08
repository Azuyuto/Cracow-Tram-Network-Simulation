using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tram.Common.Enums;
using Tram.Common.Extensions;
using Tram.Common.Helpers;
using Tram.Common.Models;
using Tram.Common.Models.Map;

namespace Tram.Controller.Repositories
{
    public static class VehicleRepository
    {
        public static List<TramIntersection> Intersections { get; set; }
        public static List<Node> Nodes { get; set; }
        public static List<TramLine> TramLines { get; set; }

        public static void Initialize()
        {
            SetNodes();
            SetTramLines();
        }

        public static void SetNodes()
        {
            Nodes = new List<Node>();
            Intersections = new List<TramIntersection>();

            foreach (var line in ZTPRepository.Lines)
            {
                var last = true;
                var latestNode = new Node();
                foreach (var mapNode in Enumerable.Reverse(line.TramRoute.Nodes).ToList())
                {
                    var node = new Node();
                    if (!Nodes.Any(a => a.Coordinates == mapNode.Coordinates))
                    {
                        node = new Node()
                        {
                            StopID = mapNode.StopID ?? "",
                            Coordinates = mapNode.Coordinates,
                            Id = "N_" + (Nodes.Count() + 1).ToString(),
                            VehiclesOn = new List<Vehicle>(),
                            Type = mapNode.IsTramStop ? NodeType.TramStop : NodeType.Normal
                        };

                        Nodes.Add(node);
                    }
                    else
                    {
                        node = Nodes.Where(a => a.Coordinates == mapNode.Coordinates).FirstOrDefault();
                    }

                    if (!last) // last node cannot have nodes after
                    {
                        if(node == latestNode)
                        {
                            continue;
                        }
                        if (node.Children != null)
                        {
                            // there are children == add new to list
                            if(!node.Children.Any(a => a.Node == latestNode))
                                node.Children.Add(new Node.Next()
                                {
                                    Node = latestNode,
                                    Distance = GeometryHelper.GetRealDistance(node.Coordinates, latestNode.Coordinates)
                                });
                        }
                        else if (node.Child != null)
                        {
                            // there is child == convert to children
                            if (node.Child.Node != latestNode)
                            {
                                node.Children = new List<Node.Next>();
                                node.Children.Add(node.Child);
                                node.Children.Add(new Node.Next()
                                {
                                    Node = latestNode,
                                    Distance = GeometryHelper.GetRealDistance(node.Coordinates, latestNode.Coordinates)
                                });
                                node.Child = null;
                            }
                        }
                        else
                        {
                            // no children == add one child
                            node.Child = new Node.Next()
                            {
                                Node = latestNode,
                                Distance = GeometryHelper.GetRealDistance(node.Coordinates, latestNode.Coordinates)
                            };
                        }
                    }

                    last = false;
                    latestNode = node;
                }
            }

            var regex = new Regex(@"stop_[0-9]*_");
            foreach(var intersection in Nodes.Where(a => !String.IsNullOrEmpty(a.StopID)).GroupBy(a => regex.Match(a.StopID).Value).Where(a => a.Count() > 2)) // intersection
            {
                var nodes = intersection.ToList();
                var distances = new List<float>();
                for(int i = 0; i < nodes.Count(); i ++)
                {
                    for(int j = i + 1; j < nodes.Count();j++)
                    {
                        distances.Add(Math.Abs(nodes[i].Coordinates.X - nodes[j].Coordinates.X) + Math.Abs(nodes[i].Coordinates.Y - nodes[j].Coordinates.Y));
                    }
                }

                var distance = distances.Average() / 2;

                if (distance > 0)
                {
                    var inter = new TramIntersection()
                    {
                        Id = "I_" + Intersections.Count(),
                        Vehicles = new Queue<Vehicle>(),
                        Nodes = new List<Node>()
                    };
                    Intersections.Add(inter);

                    foreach(var n in nodes)
                    {
                        if(n.Children != null)
                        {
                            foreach(var c in n.Children)
                            {
                                CheckAndSetIntersection(inter, n, c.Node, distance);
                            }
                        }
                        else if (n.Child != null)
                        {
                            CheckAndSetIntersection(inter, n, n.Child.Node, distance);
                        }
                    }
                }
            }
        }

        public static void CheckAndSetIntersection(TramIntersection i, Node org, Node checkNode, float distance)
        {
            if(checkNode.Type == NodeType.TramCross)
            {
                return;
            }
            var currentDistance = (Math.Abs(org.Coordinates.X - checkNode.Coordinates.X) + Math.Abs(org.Coordinates.Y - checkNode.Coordinates.Y));
            if (currentDistance > distance)
            {
                return;
            }
            else
            {
                checkNode.Type = NodeType.TramCross;
                checkNode.Intersection = i;
                i.Nodes.Add(checkNode);
                if (checkNode.Children != null)
                {
                    foreach (var c in checkNode.Children)
                    {
                        CheckAndSetIntersection(i, org, c.Node, distance);
                    }
                }
                else if (checkNode.Child != null)
                {
                    CheckAndSetIntersection(i, org, checkNode.Child.Node, distance);
                }
            }
        }

        public static void SetTramLines()
        {
            TramLines = new List<TramLine>();

            foreach (var line in ZTPRepository.Lines.Where(a => a.TramRoute != null))
            {
                var starts = line.Trips.Select(a => new DateTime() + a.FirstStart.Departure).ToList();
                var tramLine = new TramLine()
                {
                    Id = line.LineName,
                    Departures = new List<TramLine.Departure>(),
                    MainNodes = new List<Node>()
                };

                var stopsCount = line.TramRoute.Nodes.Where(a => a.IsTramStop).ToList();

                foreach (var start in starts)
                {
                    var intervals = new List<float>();
                    stopsCount.ForEach(a => intervals.Add(1));
                    tramLine.Departures.Add(new TramLine.Departure()
                    {
                        NextStopIntervals = intervals,
                        StartTime = start
                    });
                }

                foreach (var n in line.TramRoute.Nodes)
                {
                    tramLine.MainNodes.Add(Nodes.Where(a => a.Coordinates == n.Coordinates).FirstOrDefault());
                }

                TramLines.Add(tramLine);
            }
        }
    }
}

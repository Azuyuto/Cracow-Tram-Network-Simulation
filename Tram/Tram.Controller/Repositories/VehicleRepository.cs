using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tram.Common.Consts;
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
            SetIntersections();
        }

        public static void SetIntersections()
        {
            Intersections = new List<TramIntersection>();
            foreach(var i in ZTPRepository.Intersections)
            {
                Intersections.Add(new TramIntersection()
                {
                    Id = i.ID,
                    Coordinates = i.Coordinates,
                    Vehicles = new Queue<Vehicle>(),
                    Nodes = new List<Node>()
                });
            }

            foreach(var n in Nodes)
            {
                foreach(var i in Intersections)
                {
                    var distance = Math.Pow(Math.Abs(n.Coordinates.X - i.Coordinates.X), 2) + Math.Pow(Math.Abs(n.Coordinates.Y - i.Coordinates.Y), 2);
                    if(distance < 0.00000025)
                    {
                        n.Type = NodeType.TramCross;
                        n.Intersection = i;
                        i.Nodes.Add(n);
                    }
                }
            }
        }

        public static void SetNodes()
        {
            Nodes = new List<Node>();

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
                            StopName = mapNode.StopName ?? "-",
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
        }

        public static void SetTramLines()
        {
            TramLines = new List<TramLine>();

            foreach (var trip in ZTPRepository.Trips.Where(a => TimeConsts.SIMULATION_START <= a.FirstStart.Departure && a.FirstStart.Departure <= TimeConsts.SIMULATION_END))
            {
                var start = new DateTime() + trip.FirstStart.Departure;
                var tramLine = new TramLine()
                {
                    Id = trip.Line.LineName,
                    Name = trip.Destination,
                    TripID = trip.TripID,
                    Departures = new List<TramLine.Departure>(),
                    MainNodes = new List<Node>()
                };

                var stopsCount = trip.Line.TramRoute.Nodes.Where(a => a.IsTramStop).ToList();

                var intervals = new List<float>();
                stopsCount.ForEach(a => intervals.Add(0.1F));
                tramLine.Departures.Add(new TramLine.Departure()
                {
                    NextStopIntervals = intervals,
                    StartTime = start
                });

                foreach (var n in trip.Line.TramRoute.Nodes)
                {
                    tramLine.MainNodes.Add(Nodes.Where(a => a.Coordinates == n.Coordinates).FirstOrDefault());
                }

                TramLines.Add(tramLine);
            }
        }
    }
}

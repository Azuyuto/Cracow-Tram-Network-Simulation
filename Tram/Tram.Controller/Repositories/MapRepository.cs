using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Tram.Common.Models.Map;

namespace Tram.Controller.Repositories
{
    public static class MapRepository
    {
        public static List<TramRoute> TramRoutes { get; set; }
        public static List<TramStop> TramStops { get; set; } // Blue

        public static void Initialize()
        {
            ReadTramStops();
            ReadTramRoutes();
            EstabilishRoutes();
            SetBasicNodes();
            SetNearestZTPStops();
            SetTramStopsOnRoutes();
            AssignTramRouteToZTPLine();
        }

        public static void ReadTramStops()
        {
            TramStops = new List<TramStop>();

            XDocument doc = XDocument.Load(@"KML/Tram_Stop.kml");
            XElement root = doc.Root;
            XNamespace ns = root.GetDefaultNamespace();

            var placemarks = doc.Descendants(ns + "Placemark").ToList();

            foreach (XElement placemark in placemarks)
            {
                var name = placemark.Elements(ns + "name").FirstOrDefault()?.Value ?? "-";

                var point = placemark.Elements(ns + "Point").FirstOrDefault().Value;
                var coo = point.Split(',');
                var x = SafeParse(coo[0]);
                var y = SafeParse(coo[1]);

                var tramStop = new TramStop()
                {
                    Name = name,
                    Coordinates = new Vector2(x, y)
                };
                TramStops.Add(tramStop);
            }
        }

        public static void ReadTramRoutes()
        {
            TramRoutes = new List<TramRoute>();

            XDocument doc = XDocument.Load(@"KML/Tram_Route.kml");
            XElement root = doc.Root;
            XNamespace ns = root.GetDefaultNamespace();

            var placemarks = doc.Descendants(ns + "Placemark").ToList();

            foreach (XElement placemark in placemarks)
            {
                var tramRoute = new TramRoute()
                {
                    Name = placemark.Elements(ns + "name").FirstOrDefault().Value
                };

                tramRoute.TramNumber = Regex.Match(tramRoute.Name, @"\d+").Value;
                var multiGeometries = placemark.Descendants(ns + "MultiGeometry").ToList();

                foreach (XElement mg in multiGeometries)
                {
                    var lineStrings = mg.Descendants(ns + "LineString").ToList();
                    foreach (XElement ls in lineStrings)
                    {
                        var coordinates = ls.Value.Split(' ');
                        var lineString = new LineString();
                        foreach (var c in coordinates)
                        {
                            var coo = c.Split(',');
                            var x = SafeParse(coo[0]);
                            var y = SafeParse(coo[1]);
                            lineString.Coordinates.Add(new Microsoft.DirectX.Vector2(x, y));
                        }
                        tramRoute.LineStrings.Add(lineString);
                    }
                }
                TramRoutes.Add(tramRoute);
            }
        }

        public static void EstabilishRoutes()
        {
            foreach (var tr in TramRoutes)
            {
                var correctLineStrings = new List<LineString>();
                foreach (var ls in tr.LineStrings)
                {
                    var countOfCoordinates = false;
                    foreach (var c in ls.Coordinates)
                    {
                        if (tr.LineStrings.Where(a => a.Coordinates.Any(b => b == c)).Count() > 1)
                        {
                            countOfCoordinates = true;
                        }
                    }
                    if (countOfCoordinates)
                    {
                        correctLineStrings.Add(ls);
                    }
                }
                if (correctLineStrings.Count() > 0)
                    tr.LineStrings = correctLineStrings;
            }
        }

        public static void SetBasicNodes()
        {
            foreach (var tr in TramRoutes)
            {
                foreach (var ls in tr.LineStrings)
                {
                    if (tr.Nodes.Count() == 0)
                    {
                        foreach (var c in ls.Coordinates)
                        {
                            tr.Nodes.Add(new MapNode()
                            {
                                Coordinates = c
                            });
                        }
                    }
                    else
                    {
                        // On End
                        if(ls.Coordinates.FirstOrDefault() == tr.Nodes.Last().Coordinates)
                        {
                            foreach (var c in ls.Coordinates.Skip(1))
                            {
                                tr.Nodes.Add(new MapNode()
                                {
                                    Coordinates = c
                                });
                            }
                        }
                        else if(ls.Coordinates.LastOrDefault() == tr.Nodes.Last().Coordinates)
                        {
                            for(int i = ls.Coordinates.Count() - 2; 0 <= i; i--)
                            {
                                tr.Nodes.Add(new MapNode()
                                {
                                    Coordinates = ls.Coordinates[i]
                                });
                            }
                        }
                    }
                }
            }
        }

        public static void SetTramStopsOnRoutes()
        {
            foreach(var t in TramRoutes)
            {
                foreach(var n in t.Nodes)
                {
                    foreach(var s in TramStops)
                    {
                        if (s.Coordinates.Equals(n.Coordinates)) {
                            t.TramStops.Add(s);
                            n.IsTramStop = true;
                            n.StopID = s.StopID;
                            n.StopName = s.Name;
                        }
                    }
                }
            }
        }

        private static void SetNearestZTPStops()
        {
            foreach(var tramStop in TramStops)
            {
                var nearest = ZTPRepository.Stops.Select(a => new
                {
                    a.StopID,
                    Distance = Math.Abs(a.StopCoordinates.X - tramStop.Coordinates.X) + Math.Abs(a.StopCoordinates.Y - tramStop.Coordinates.Y)
                }).OrderBy(a => a.Distance).FirstOrDefault();
                tramStop.StopID = nearest.StopID;
            }
        }

        private static float SafeParse(string input)
        {
            if (String.IsNullOrEmpty(input)) { throw new ArgumentNullException("input"); }

            float res;
            if (Single.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out res))
            {
                return res;
            }

            return 0.0f; // Or perhaps throw your own exception type
        }

        public static void AssignTramRouteToZTPLine()
        {
            foreach (var tramRoute in TramRoutes)
            {
                var lineZTP = ZTPRepository.Lines.Where(a => a.LineName == tramRoute.TramNumber).FirstOrDefault();
                if(lineZTP != null)
                {
                    lineZTP.TramRoute = tramRoute;
                }
            }
        }
    }
}

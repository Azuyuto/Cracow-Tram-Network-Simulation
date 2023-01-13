using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tram.Common.Models.Own;
using Tram.Common.Models.ZTP;
using TramNetwork.Common.Models.ZTP;

namespace Tram.Controller.Repositories
{
    public static class ZTPRepository
    {
        public static int ServiceID { get; set; }

        public static List<Intersection> Intersections { get; set; }
        public static List<NodePair> MapLines { get; set; }
        public static List<LineZTP> Lines { get; set; }
        public static List<TripZTP> Trips { get; set; }
        public static List<StopZTP> Stops { get; set; } // RED
        public static List<StopTimesZTP> StopTimes { get; set; }

        public static Dictionary<string, HashSet<string>> StopDictionary { get; set; }

        public static void Initialize()
        {
            ServiceID = 2;

            ReadIntersections();
            ReadStopsInfo();
            ReadStopTimesInfo();
            ReadTripsInfo();
            ReadLinesInfo();
            SetLists();
        }

        public static void ReadIntersections()
        {
            try
            {
                Intersections = new List<Intersection>();
                using (StreamReader sr = new StreamReader("OWN/intersection.txt"))
                {
                    sr.ReadLine(); // ignore header
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Vector2 coord = new Vector2();
                        var data = line.Split(',').ToList();
                        var intersection = new Intersection();
                        intersection.ID = data[0];
                        intersection.Name = data[1].Trim('"');
                        coord.X = float.Parse(data[3], CultureInfo.InvariantCulture);
                        coord.Y = float.Parse(data[2], CultureInfo.InvariantCulture);
                        intersection.Coordinates = coord;
                        Intersections.Add(intersection);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public static void SetLists()
        {
            // Lines
            foreach(var trip in Trips)
            {
                var line = Lines.Where(a => a.RouteID == trip.RouteID).FirstOrDefault();
                if (line != null)
                {
                    line.Trips.Add(trip);
                    trip.Line = line;
                }
            }

            // Stop Dictionary
            foreach(var d in StopDictionary)
            {
                var trip = Trips.Where(a => a.TripID.Contains(d.Key)).FirstOrDefault();
                if(trip != null)
                {
                    var line = Lines.Where(a => a.RouteID == trip.RouteID).FirstOrDefault();
                    if(line != null)
                    {
                        foreach (var s in d.Value)
                            line.StopDictionary.Add(s);
                    }
                }    
            }
        }

        public static List<RouteZTP> GetRoutes()
        {
            return Trips.GroupBy(a => a.RouteID).Select(a => a.Skip(3).FirstOrDefault().TripID).Select(a => GetRouteByName(a)).ToList();
        }

        public static RouteZTP GetRouteByName(string tripID)
        {
            var model = new RouteZTP()
            {
                Trip = Trips.Where(a => a.TripID == tripID).FirstOrDefault(),
                Stops = StopTimes.Where(a => a.TripID == tripID).ToList()
            };
            model.Line = Lines.Where(a => a.RouteID == model.Trip.RouteID).FirstOrDefault();

            return model;
        }

        public static void ReadStopTimesInfo()
        {
            try
            {
                StopTimes = new List<StopTimesZTP>();
                StopDictionary = new Dictionary<string, HashSet<string>>();
                using (StreamReader sr = new StreamReader("ZTP/stop_times.txt"))
                {
                    sr.ReadLine(); // ignore header
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        List<string> stopData = new List<String>();
                        stopData = line.Split(',').ToList();
                        var stop = new StopTimesZTP();
                        stop.TripID = stopData[0];
                        stop.Arrival = stopData[1];
                        for(int i = 24; i <= 29; i ++)
                        {
                            stopData[2] = Regex.Replace(stopData[2], "^" + i.ToString(), "0" + (i - 24).ToString());
                        }
                        stop.Departure = TimeSpan.Parse(stopData[2]);
                        stop.StopID = stopData[3];
                        if(stop.TripID.Contains("service_" + ServiceID))
                        {
                            if (!StopDictionary.ContainsKey(stop.TripID))
                                StopDictionary.Add(stop.TripID, new HashSet<string>());
                            StopDictionary[stop.TripID].Add(stop.StopID);
                            StopTimes.Add(stop);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public static void ReadLinesInfo()
        {
            try
            {
                Lines = new List<LineZTP>();
                using (StreamReader sr = new StreamReader("ZTP/routes.txt"))
                {
                    sr.ReadLine(); // ignore header
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        List<string> content = new List<String>();
                        var nextLine = new LineZTP();
                        content = line.Split(',').ToList();
                        nextLine.RouteID = content[0];
                        nextLine.LineName = content[2].Trim('"');
                        Lines.Add(nextLine);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public static void ReadTripsInfo()
        {
            try
            {
                Trips = new List<TripZTP>();
                using (StreamReader sr = new StreamReader("ZTP/trips.txt"))
                {
                    sr.ReadLine(); // ignore header
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        List<string> content = new List<String>();
                        var nextTrip = new TripZTP();
                        content = line.Split(',').ToList();
                        nextTrip.TripID = content[0];
                        nextTrip.RouteID = content[1];
                        nextTrip.ServiceID = content[2];
                        nextTrip.Destination = content[3];
                        if(nextTrip.ServiceID == "service_" + ServiceID)
                        {
                            nextTrip.FirstStart = StopTimes.Where(a => a.TripID == nextTrip.TripID).OrderBy(a => a.Departure).FirstOrDefault();
                            Trips.Add(nextTrip);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read: ");
                Console.WriteLine(e.Message);
            }
        }

        public static void ReadStopsInfo()
        {
            try
            {
                Stops = new List<StopZTP>();
                using (StreamReader sr = new StreamReader("ZTP/stops.txt", Encoding.UTF8))
                {
                    sr.ReadLine(); // ignore header
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Vector2 coord = new Vector2();
                        List<string> stop_line = new List<String>();
                        var nextStop = new StopZTP();
                        stop_line = line.Split(',').ToList();
                        nextStop.StopID = stop_line[0];
                        nextStop.StopName = stop_line[2].Trim('"');
                        coord.X = float.Parse(stop_line[5], CultureInfo.InvariantCulture);
                        coord.Y = float.Parse(stop_line[4], CultureInfo.InvariantCulture);
                        nextStop.StopCoordinates = coord;
                        Stops.Add(nextStop);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read: ");
                Console.WriteLine(e.Message);
            }
        }
    }
}

﻿using Microsoft.DirectX;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Tram.Common.Enums;
using Tram.Common.Helpers;
using Tram.Common.Models;

namespace Tram.Controller.Repositories
{
    public class FileRepository : IRepository
    {
        private string mapPath, linesPath;
        private List<TramIntersection> tramIntersections;
        private List<TramLine> tramLines;
        private List<Node> nodes;

        #region Public Methods

        /// <summary>
        /// Load data into repository
        /// </summary>
        /// <param name="parameters">First string is path to file with map, the second one is connected with tram lines file</param>
        public void LoadData(params string[] parameters)
        {
            mapPath = parameters[0];
            linesPath = parameters[1];

            LoadMapAndIntersections();
            LoadLines();
        }

        public List<TramIntersection> GetTramIntersections() => tramIntersections;

        public List<TramLine> GetLines() => tramLines;

        public List<Node> GetMap() => nodes;

        #endregion Public Methods

        #region Private Methods

        private void LoadLines()
        {
            tramLines = new List<TramLine>();
            using (var file = new StreamReader(linesPath))
            {
                TramLine tramLine = null;
                bool isNewTramLine = true;
                bool isDepartureLine = false;
                //---zmienne pomocnicze do capacity
                List<string> StartingTimes = new List<string>();
                //---
                foreach (string line in GetFileLines(file))
                {
                    string[] par = line.Split(';');
                    if (isNewTramLine)
                    {
                        tramLine = new TramLine() { Id = par[0] + " (" + par[1].Replace("  ", " ").Trim() + ")", Departures = new List<TramLine.Departure>(), MainNodes = new List<Node>() };
                        isNewTramLine = false;
                    }
                    else if (string.IsNullOrEmpty(par[0]) && !isDepartureLine)
                    {
                        //koniec czytania linii
                        isDepartureLine = true;
                    }
                    else if (string.IsNullOrEmpty(par[0]) && isDepartureLine)
                    {
                        //linia w drugą stronę, nowa linia
                        isNewTramLine = true;
                        isDepartureLine = false;

                        tramLines.Add(tramLine);
                    }
                    else if (isDepartureLine)
                    {
                        StartingTimes = new List<string>();
                        for (int i = 0; i < par.Length; i++)
                        {
                            if (string.IsNullOrEmpty(par[i]))
                            {
                                break;
                            }

                            tramLine.Departures[i].StartTime = TimeHelper.GetTime(par[i]);
                            //capacity - klucze
                            StartingTimes.Add(par[i]);
                        }
                    }
                    else
                    {
                        tramLine.MainNodes.Add(nodes.Single(n => n.Id == par[0]));

                        if (!string.IsNullOrEmpty(par[1]))
                        {
                            int j = 0;
                            for (int i = 5; i < par.Length; i += 4)
                            {
                                if (string.IsNullOrEmpty(par[5]))
                                {
                                    tramLine.Departures.Add(new TramLine.Departure() { NextStopIntervals = new List<float>() });
                                    tramLine.Departures[j].NextStopIntervals.Add(0);
                                }
                                else
                                {
                                    tramLine.Departures[j].NextStopIntervals.Add(float.Parse(par[i], CultureInfo.InvariantCulture.NumberFormat));
                                }

                                j++;
                            }
                        }                
                    }
                }
            }            
        }

        private void LoadMapAndIntersections()
        {
            nodes = new List<Node>();
            tramIntersections = new List<TramIntersection>();
            List<string> childrenStr = new List<string>();

            using (var file = new StreamReader(mapPath))
            {
                file.ReadLine(); //read header

                foreach (string line in GetFileLines(file))
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] par = line.Split(';');
                        if (!nodes.Any(n => n.Id == par[0])) //TODO: usunąć ten warunek w finalnej wersji, gdy juz nie będzie duplikatów w plikach
                        {
                            Node node = new Node()
                            {
                                Coordinates = new Vector2(float.Parse(par[0], CultureInfo.InvariantCulture.NumberFormat), float.Parse(par[1], CultureInfo.InvariantCulture.NumberFormat)),
                                Id = par[2],
                                IsUnderground = par[9] == "1",
                                VehiclesOn = new List<Vehicle>(),
                                Type = par[4] == "1" ? NodeType.TramStop :
                                       !string.IsNullOrEmpty(par[3]) ? NodeType.TramCross : NodeType.Normal
                            };

                            StringBuilder childStr = new StringBuilder(";");
                            for (int i = 10; i < par.Length; i++)
                            {
                                childStr.Append(par[i]);
                                childStr.Append(";");
                            }

                            childStr.Append(";");
                            childStr.Replace("\"", "");
                            childrenStr.Add(childStr.ToString());
                            nodes.Add(node);

                            //Check for intersection 
                            string intersectionId = par[3];
                            if (!string.IsNullOrEmpty(intersectionId))
                            {
                                if (!tramIntersections.Any(i => i.Id.Equals(intersectionId)))
                                {
                                    tramIntersections.Add(new TramIntersection() { Id = intersectionId, Vehicles = new Queue<Vehicle>(), Nodes = new List<Node>() });
                                }                                

                                var intersection = tramIntersections.Single(i => i.Id.Equals(intersectionId));
                                intersection.Nodes.Add(node);
                                node.Intersection = intersection;
                            }
                        }
                    }
                }

                for (int i = 0; i < nodes.Count; i++)
                {
                    var children = nodes.Where(n => childrenStr[i].Contains(";" + n.Id + ";"));
                    if (children != null && children.Any())
                    {
                        if (children.Count() == 1)
                        {
                            nodes[i].Child = new Node.Next()
                            {
                                Node = children.First(),
                                Distance = GeometryHelper.GetRealDistance(nodes[i].Coordinates, children.First().Coordinates)
                            };
                        }
                        else
                        {
                            nodes[i].Children = new List<Node.Next>();
                            foreach (var child in children)
                            {
                                nodes[i].Children.Add(new Node.Next()
                                {
                                    Node = child,
                                    Distance = GeometryHelper.GetRealDistance(nodes[i].Coordinates, child.Coordinates)
                                });
                            }
                        }
                    }
                }
            }

            nodes = nodes.OrderBy(n => int.Parse(n.Id)).ToList();
        }

        private IEnumerable<string> GetFileLines(StreamReader file)
        {
            string textLine = null;
            while ((textLine = file.ReadLine()) != null)
            {
                yield return textLine;
            }
        }

        #endregion Private Methods
    }
}

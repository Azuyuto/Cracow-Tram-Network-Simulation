using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tram.Common.Consts;
using Tram.Common.Enums;
using Tram.Common.Helpers;
using Tram.Controller.Controllers;
using Tram.Controller;
using Tram.Controller.Repositories;
using Tram.Common.Models;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using Microsoft.DirectX;

namespace Tram.Simulation.Manager
{
    public class SimulationManager
    {
        public bool ActiveSimulation { get; set; }
        public int IntervalMiliseconds { get; set; }
        public TimeSpan Timer { get; set; }
        public DateTime LastTimeUpdate { get; set; }

        private List<CustomVertex.PositionColored[]> vertexes;
        private List<CustomVertex.PositionColored[]> edges;
        private float minX, maxX, minY, maxY;

        public void StartSimulation()
        {
            ActiveSimulation = true;
            LastTimeUpdate = DateTime.Now;
        }

        public void UpdateSimulaton()
        {
            LastTimeUpdate = DateTime.Now;
            Timer = Timer.Add(new TimeSpan(0, 0, 0, 0, IntervalMiliseconds));
        }

        public void StopSimulation()
        {
            ActiveSimulation = false;
        }

        public void InitMap()
        {
            minX = ZTPRepository.Stops.Min(n => n.StopCoordinates.X);
            maxX = ZTPRepository.Stops.Max(n => n.StopCoordinates.X);
            minY = ZTPRepository.Stops.Min(n => n.StopCoordinates.Y);
            maxY = ZTPRepository.Stops.Max(n => n.StopCoordinates.Y);

            vertexes = new List<CustomVertex.PositionColored[]>();
            edges = new List<CustomVertex.PositionColored[]>();

            foreach (var route in ZTPRepository.GetRoutes())
            {
                for (var i = 0; i < route.Stops.Count(); i++)
                {
                    var stop = ZTPRepository.Stops.Where(a => a.StopID == route.Stops[i].StopID).FirstOrDefault();
                    float stopX = CalculateXPosition(stop.StopCoordinates.X);
                    float stopY = CalculateYPosition(stop.StopCoordinates.Y);

                    vertexes.Add(DirectxHelper.CreateCircle(stopX, stopY,
                                    ViewConsts.POINT_NORMAL_COLOR.ToArgb(),
                                    ViewConsts.POINT_RADIUS,
                                    ViewConsts.POINT_PRECISION));

                    if (i + 1 != route.Stops.Count())
                    {
                        var stopNext = ZTPRepository.Stops.Where(a => a.StopID == route.Stops[i + 1].StopID).FirstOrDefault();
                        float stopNextX = CalculateXPosition(stopNext.StopCoordinates.X);
                        float stopNextY = CalculateYPosition(stopNext.StopCoordinates.Y);

                        edges.Add(DirectxHelper.CreateLine(stopX, stopY, stopNextX, stopNextY, ViewConsts.LINE_BASIC_COLOR.ToArgb(), ViewConsts.POINT_RADIUS));
                    }
                }
            }
        }

        public void Render(Device device, Vector3 cameraPosition)
        {
            //DRAW EDGES
            foreach (var edge in edges)
            {
                device.DrawUserPrimitives(PrimitiveType.TriangleStrip, 2, edge);
            }

            //DRAW POINTS
            foreach (var vertex in vertexes)
            {
                device.DrawUserPrimitives(PrimitiveType.TriangleFan, ViewConsts.POINT_PRECISION, vertex);
            }
        }

        public float CalculateXPosition(float originalX)
        {
            return (100 - (originalX - minX) * 100 / (maxX - minX)) - 50; // X axis is swapped
        }

        public float CalculateYPosition(float originalY)
        {
            return (originalY - minY) * 100 / (maxX - minX) - (50 * (minY - maxY)) / (minX - maxX);
        }
    }
}

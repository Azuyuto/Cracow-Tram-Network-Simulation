﻿using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using Tram.Common.Consts;
using Tram.Common.Enums;
using Tram.Common.Extensions;
using Tram.Common.Helpers;
using Tram.Common.Models;

namespace Tram.Controller.Controllers
{
    public class DirectxController
    {
        private bool isDeviceInit;

        private MainController mainController;
        private List<CustomVertex.PositionColored[]> vertexes;
        private List<CustomVertex.PositionColored[]> edges;

        private Microsoft.DirectX.Direct3D.Font text;
        private Line line;
        private Vector2[] lineVertexes;
        private float minX, maxX, minY, maxY;

        public DirectxController()
        {
            isDeviceInit = false;
            vertexes = new List<CustomVertex.PositionColored[]>();
            edges = new List<CustomVertex.PositionColored[]>();
        }

        #region Public Methods

        public void InitMap()
        {
            if (mainController == null)
            {
                mainController = Kernel.Get<MainController>();
            }

            minX = mainController.Map.Min(n => n.Coordinates.X);
            maxX = mainController.Map.Max(n => n.Coordinates.X);
            minY = mainController.Map.Min(n => n.Coordinates.Y);
            maxY = mainController.Map.Max(n => n.Coordinates.Y);

            foreach (var node in mainController.Map.OrderBy(n => !n.IsUnderground))
            {
                float pX = CalculateXPosition(node.Coordinates.X);
                float pY = CalculateYPosition(node.Coordinates.Y);

                vertexes.Add(
                    DirectxHelper.CreateCircle(
                        pX,
                        pY,
                        node.Type == NodeType.TramStop ? Color.Green.ToArgb() :
                                        node.Type == NodeType.TramCross ? Color.Blue.ToArgb() :
                                                                Color.Black.ToArgb(),
                            ViewConsts.POINT_RADIUS,
                            ViewConsts.POINT_PRECISION));

                if (node.Child != null)
                {
                    float pX2 = CalculateXPosition(node.Child.Node.Coordinates.X);
                    float pY2 = CalculateYPosition(node.Child.Node.Coordinates.Y);
                    edges.Add(DirectxHelper.CreateLine(pX, pY, pX2, pY2, ViewConsts.LINE_BASIC_COLOR.ToArgb(), ViewConsts.POINT_RADIUS));
                }
                else if (node.Children != null)
                {
                    foreach (var child in node.Children)
                    {
                        float pX2 = CalculateXPosition(child.Node.Coordinates.X);
                        float pY2 = CalculateYPosition(child.Node.Coordinates.Y);
                        edges.Add(DirectxHelper.CreateLine(pX, pY, pX2, pY2, ViewConsts.LINE_BASIC_COLOR.ToArgb(), ViewConsts.POINT_RADIUS));
                    }
                }
            }

            // INTERSECTIONS
            //foreach (var i in mainController.TramIntersections)
            //{
            //    float pX = CalculateXPosition(i.Coordinates.X);
            //    float pY = CalculateYPosition(i.Coordinates.Y);
            //    vertexes.Add(DirectxHelper.CreateCircle(pX, pY, Color.DeepPink.ToArgb(),
            //                ViewConsts.POINT_RADIUS * 40,
            //                ViewConsts.POINT_PRECISION));
            //}
        }

        public void Render(Device device, Vector3 cameraPosition, Vehicle selectedVehicle, string time)
        {
            if (!isDeviceInit)
            {
                InitDevice(device);
            }

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

            DrawVehicles(device, cameraPosition, selectedVehicle);

            //DRAW TIME
            text.DrawText(null, time, new Point(12, 11), Color.Black);
            line.Draw(lineVertexes, Color.Black);
        }

        public float CalculateXPosition(float originalX)
        {
            return (100 - (originalX - minX) * 100 / (maxX - minX)) - 50; // X axis is swapped
        }

        public float CalculateYPosition(float originalY)
        {
            return (originalY - minY) * 100 / (maxX - minX) - (50 * (minY - maxY)) / (minX - maxX);
        }

        public float OrgXPosition(float X)
        {
            return (((X + maxY) - 100) * (maxX - minX / 100)) - minX;
        }

        #endregion Public Methods

        #region Private Methods

        private void InitDevice(Device device)
        {
            System.Drawing.Font systemfont = new System.Drawing.Font("Arial", 12f, System.Drawing.FontStyle.Regular);
            text = new Microsoft.DirectX.Direct3D.Font(device, systemfont);
            line = new Line(device);
            lineVertexes = new Vector2[] { new Vector2(8, 8), new Vector2(77, 8), new Vector2(77, 31), new Vector2(8, 31), new Vector2(8, 8) };
            isDeviceInit = true;
        }

        private void DrawVehicles(Device device, Vector3 cameraPosition, Vehicle selectedVehicle)
        {
            foreach (var vehicle in mainController.Vehicles)
            {
                Color tramColor = Color.Green;
                float pX = CalculateXPosition(vehicle.Position.Coordinates.X);
                float pY = CalculateYPosition(vehicle.Position.Coordinates.Y);
                float thickness = GetPointRadius(cameraPosition.Z);
                float selectedThickness = thickness * 1.7f;

                if (vehicle.Equals(selectedVehicle))
                {
                    device.DrawUserPrimitives(PrimitiveType.TriangleFan, ViewConsts.POINT_PRECISION, DirectxHelper.CreateCircle(pX, pY, ViewConsts.SELECTED_COLOR.ToArgb(), selectedThickness, ViewConsts.POINT_PRECISION));
                }

                device.DrawUserPrimitives(PrimitiveType.TriangleFan, ViewConsts.POINT_PRECISION, DirectxHelper.CreateCircle(pX, pY, tramColor.ToArgb(), thickness, ViewConsts.POINT_PRECISION));

                float length = VehicleConsts.LENGTH;
                int actualNodeIndex = vehicle.VisitedNodes.Count - 2;
                Vector2 prevCoordinates = vehicle.Position.Coordinates;
                float pX2, pY2;
                while (length > 0)
                {
                    if (actualNodeIndex >= 0)
                    {
                        Node actualNode = vehicle.VisitedNodes[actualNodeIndex--];
                        float distance = prevCoordinates.RealDistanceTo(actualNode);
                        pX = CalculateXPosition(prevCoordinates.X);
                        pY = CalculateYPosition(prevCoordinates.Y);
                        if (distance >= length)
                        {
                            float displacement = (distance - length) * 100 / distance;
                            var pos = GeometryHelper.GetLocactionBetween(displacement, actualNode.Coordinates, prevCoordinates);
                            pX2 = CalculateXPosition(pos.X);
                            pY2 = CalculateYPosition(pos.Y);
                        }
                        else
                        {
                            pX2 = CalculateXPosition(actualNode.Coordinates.X);
                            pY2 = CalculateYPosition(actualNode.Coordinates.Y);
                            prevCoordinates = actualNode.Coordinates;
                        }

                        if (vehicle.Equals(selectedVehicle))
                        {
                            var selectedTramTail = DirectxHelper.CreateLine(pX, pY, pX2, pY2, ViewConsts.SELECTED_COLOR.ToArgb(), selectedThickness);
                            device.DrawUserPrimitives(PrimitiveType.TriangleStrip, 2, selectedTramTail);
                        }

                        var tramTail = DirectxHelper.CreateLine(pX, pY, pX2, pY2, tramColor.ToArgb(), thickness);
                        device.DrawUserPrimitives(PrimitiveType.TriangleStrip, 2, tramTail);

                        length -= distance;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private float GetPointRadius(float cameraHeight)
        {
            return (cameraHeight * (19f / 99) + (80f / 99)) * ViewConsts.POINT_RADIUS;
        }
        #endregion Private Methods
    }
}

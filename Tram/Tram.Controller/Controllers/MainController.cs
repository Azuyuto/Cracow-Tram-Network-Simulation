﻿using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using System;
using System.Collections.Generic;
using System.Linq;
using Tram.Common.Consts;
using Tram.Common.Enums;
using Tram.Common.Helpers;
using Tram.Common.Models;
using Tram.Controller.Repositories;

namespace Tram.Controller.Controllers
{
    public class MainController
    {
        private IRepository repository;
        private DirectxController directxController;
        private VehiclesController vehiclesController;
        
        private DateTime lastUpdateTime;

        #region Public Properties

        public float SimulationSpeed { get; set; } = 100;

        public List<Node> Map { get; set; }

        public List<Vehicle> Vehicles { get; set; }

        public List<TramLine> Lines { get; set; }

        public DateTime ActualRealTime { get; set; }

        #endregion Public Properties

        public MainController(DirectxController directxController, VehiclesController vehiclesController, IRepository repository)
        {
            this.directxController = directxController;
            this.vehiclesController = vehiclesController;
            this.repository = repository;
            repository.LoadData("Points.csv", "Lines.csv");
        }

        #region Public Methods

        public void StartSimulation(DateTime startTime)
        {
            lastUpdateTime = DateTime.Now;
            ActualRealTime = startTime;
            GetAndPrepareModels();
        }

        public void Render(Device device, Vector3 cameraPosition, Vehicle selectedVehicle)
        {
            directxController.Render(device, cameraPosition, selectedVehicle, TimeHelper.GetExtTimeStr(ActualRealTime));
        }

        public void Update()
        {
            // Get time interval since last update (in seconds)
            float elapsedTime = (float)Math.Min((DateTime.Now - lastUpdateTime).TotalSeconds, CalculationConsts.MAX_TIME_INTERVAL);
            float deltaTime = elapsedTime * 10;
            lastUpdateTime = DateTime.Now;

            // Change time 
            ActualRealTime += new TimeSpan(0, 0, 0, 0, (int)(deltaTime * 10000));

            //Remove finished courses
            Vehicles.RemoveAll(vehiclesController.FinishCoursePredicate);
            
            float sampleDeltaTime = deltaTime / CalculationConsts.SAMPLES_COUNT;
            for (int i = 0; i < CalculationConsts.SAMPLES_COUNT; i++)
            {
                // Update trams
                vehiclesController.Update(sampleDeltaTime);
            }

            StartNewCourses();                        
        }

        #endregion Public Methods

        #region Private Methods

        private void GetAndPrepareModels()
        {
            Lines = VehicleRepository.TramLines;
            Map = VehicleRepository.Nodes;

            //Lines = repository.GetLines();
            //Map = repository.GetMap();

            Vehicles = new List<Vehicle>();
            directxController.InitMap();
        }

        private void StartNewCourses()
        {
            List<Node> startPoints = new List<Node>();
            foreach (var line in Lines)
            {
                for (int i = line.Departures.Count - 1; i >= 0; i--)
                {
                    if (TimeHelper.GetTimeStr(line.Departures[i].StartTime) == TimeHelper.GetTimeStr(ActualRealTime))
                    {
                        if (line.Departures[i] != line.LastDeparture &&
                            !startPoints.Any(sp => sp.Equals(line.MainNodes.First())) &&
                            vehiclesController.IsFreeSpace(line.MainNodes.First(), VehicleConsts.SAFE_SPACE))
                        {
                            startPoints.Add(line.MainNodes.First());
                            line.LastDeparture = line.Departures[i];
                            Vehicle newVehicle = new Vehicle()
                            {
                                Id = TimeHelper.GetTimeStr(line.LastDeparture.StartTime) + " - " + line.Id,
                                Line = line,
                                StartTime = line.LastDeparture.StartTime,
                                LastDepartureTime = line.LastDeparture.StartTime,
                                Departure = line.LastDeparture,
                                Passengers = 0,
                                PassengersHistory = new List<int>() { 0 },
                                DelaysHistory = new List<double>() { 0 },
                                Speed = 0f,
                                IsOnStop = line.MainNodes.First().Type == NodeType.TramStop,
                                LastVisitedStops = new List<Node>(),
                                VisitedNodes = new List<Node>()
                                {
                                    line.MainNodes.First(),
                                    line.MainNodes.First().Child.Node
                                },
                                Position = new Vehicle.Location()
                                {
                                    Node1 = line.MainNodes.First(),
                                    Node2 = line.MainNodes.First().Child.Node,
                                    Displacement = 0,
                                    Coordinates = line.MainNodes.First().Coordinates
                                }
                            };
                            line.MainNodes.First().VehiclesOn.Add(newVehicle);
                            Vehicles.Add(newVehicle);
                        }

                        break;
                    }
                }
            }
        }

        #endregion Private Methods
    }
}

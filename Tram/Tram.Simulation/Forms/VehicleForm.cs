﻿using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Tram.Common.Helpers;
using Tram.Common.Models;

namespace Tram.Simulation.Forms
{
    public partial class VehicleForm : Form
    {
        public Vehicle Vehicle { get; set; }

        public VehicleForm(Vehicle vehicle)
        {
            InitializeComponent();
            Vehicle = vehicle;
        }

        public void Init()
        {
            Text = Vehicle.Id;
            InitSummary();
        }

        private void InitSummary()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Linia: ");
            sb.Append(Vehicle.Line.Id);
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("Godzina startu: ");
            sb.Append(TimeHelper.GetTimeStr(Vehicle.StartTime));
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("Zapełnienie: ");
            sb.Append(Vehicle.Passengers);
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("Szybkość: ");
            sb.Append(Vehicle.Speed.ToString("N2"));
            sb.Append("km/h");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("Współrzędne: ");
            sb.Append("N: ");
            sb.Append(Vehicle.Position.Coordinates.Y.ToString("N4"));
            sb.Append("   E: ");
            sb.Append(Vehicle.Position.Coordinates.X.ToString("N4"));
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("Ilość odwiedzonych przystanków: ");
            sb.Append(Vehicle.LastVisitedStops.Count);
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            propertiesLabel.Text = sb.ToString();
        }
    }
}

using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Tram.Common.Helpers;
using Tram.Common.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

            sb.Append("Trip ID: ");
            sb.Append(Vehicle.Line.TripID);
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("Linia: ");
            sb.Append(Vehicle.Line.Id);
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("Kierunek: ");
            sb.Append(Vehicle.Line.Name);
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("Godzina startu: ");
            sb.Append(TimeHelper.GetTimeStr(Vehicle.StartTime));
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

            sb.Append("Historia przystanków: ");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            foreach (var i in Vehicle.StopHistories)
            {
                var s = Encoding.UTF8.GetString(Encoding.Default.GetBytes(i.Item1));
                sb.Append(i.Item2.ToString("HH:mm") + " - " + s);
                sb.Append(Environment.NewLine);
            }

            propertiesLabel.ReadOnly = true;
            propertiesLabel.BorderStyle = 0;
            propertiesLabel.BackColor = this.BackColor;
            propertiesLabel.TabStop = false;
            propertiesLabel.Text = sb.ToString();
        }
    }
}

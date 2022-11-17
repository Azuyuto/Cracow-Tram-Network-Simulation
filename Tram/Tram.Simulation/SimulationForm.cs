using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tram.Controller.Controllers;
using Tram.Simulation.Manager;

namespace Tram.Simulation
{
    public partial class SimulationForm : Form
    {
        private SimulationManager simulationManager;
        private Device device;

        public SimulationForm(SimulationManager simulationManager)
        {
            InitializeComponent();
            this.simulationManager = simulationManager;
        }

        public void Init()
        {
            InitializeGraphics();
        }

        public void RenderMap()
        {
            // TODO:
        }

        public void SetTimer(TimeSpan timeSpan)
        {
            timerLabel.Text = timeSpan.ToString(@"hh\:mm\:ss");
        }

        #region Private Methods

        private bool InitializeGraphics()
        {
            try
            {
                PresentParameters presentParams = new PresentParameters();
                presentParams.Windowed = true;
                presentParams.SwapEffect = SwapEffect.Discard;
                device = new Device(0, DeviceType.Hardware, renderPanel, CreateFlags.MixedVertexProcessing, presentParams);
                return true;
            }
            catch (DirectXException)
            {
                return false;
            }
        }

        #endregion Private Methods

        private void startButton_Click(object sender, EventArgs e)
        {
            simulationManager.StartSimulation();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            simulationManager.StopSimulation();
        }
    }
}

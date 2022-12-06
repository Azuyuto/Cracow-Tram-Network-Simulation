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
using Tram.Common.Models;
using Tram.Common.Consts;

namespace Tram.Simulation
{
    public partial class SimulationForm : Form
    {
        private SimulationManager simulationManager;
        private Device device;
        private Vector3 cameraPosition, cameraTarget;
        private Point lastClickedMouseLocation;

        public SimulationForm(SimulationManager simulationManager)
        {
            InitializeComponent();
            this.simulationManager = simulationManager;

            // Set handlers
            renderPanel.MouseWheel += RenderPanel_MouseWheel;

            // Set variables
            cameraPosition = new Vector3(0, 0, ViewConsts.START_CAMERA_Z);
            cameraTarget = new Vector3(0, 0, 0);
        }

        public void Init()
        {
            InitializeGraphics();
        }

        public void Render(Action<Device, Vector3> renderAction)
        {
            device.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4, renderPanel.Width / renderPanel.Height, 1.0f, 1000f);
            device.Transform.View = Matrix.LookAtLH(cameraPosition, cameraTarget, new Vector3(0, 1, 0));
            device.RenderState.Lighting = false;
            device.RenderState.CullMode = Cull.None;

            device.Clear(ClearFlags.Target, Color.WhiteSmoke, 1.0f, 0);

            device.BeginScene();

            device.VertexFormat = CustomVertex.PositionColored.Format;

            //Invoke render action
            renderAction(device, cameraPosition);

            device.EndScene();
            device.Present();
            Invalidate();
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

        private void ZoomInButton_Click(object sender, EventArgs e)
        {
            if (cameraPosition.Z > ViewConsts.ZOOM_OFFSET)
            {
                cameraPosition.Z -= ViewConsts.ZOOM_OFFSET;
            }
            else if (cameraPosition.Z > 1 + (ViewConsts.ZOOM_OFFSET / 20))
            {
                cameraPosition.Z -= ViewConsts.ZOOM_OFFSET / 20;
            }
        }

        private void ZoomOutButton_Click(object sender, EventArgs e)
        {
            if (cameraPosition.Z < ViewConsts.ZOOM_OFFSET)
            {
                cameraPosition.Z += ViewConsts.ZOOM_OFFSET / 20;
            }
            else if (cameraPosition.Z < ViewConsts.START_CAMERA_Z * 1.5)
            {
                cameraPosition.Z += ViewConsts.ZOOM_OFFSET;
            }

            if (cameraPosition.Z > ViewConsts.START_CAMERA_Z * 1.5)
            {
                cameraPosition.Z = ViewConsts.START_CAMERA_Z * 1.5f;
            }
        }

        private void RenderPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                ZoomInButton_Click(this, new EventArgs());
            }
            else if (e.Delta < 0)
            {
                ZoomOutButton_Click(this, new EventArgs());
            }
        }

        private void renderPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button.HasFlag(MouseButtons.Left))
            {
                if (!lastClickedMouseLocation.IsEmpty && lastClickedMouseLocation != e.Location)
                {
                    float xDiff = Math.Abs(lastClickedMouseLocation.X - e.Location.X);
                    if (lastClickedMouseLocation.X > e.Location.X)
                    {
                        cameraPosition.X -= ViewConsts.SWIPE_OFFSET * cameraPosition.Z * xDiff;
                    }
                    else if (lastClickedMouseLocation.X < e.Location.X)
                    {
                        cameraPosition.X += ViewConsts.SWIPE_OFFSET * cameraPosition.Z * xDiff;
                    }

                    float yDiff = Math.Abs(lastClickedMouseLocation.Y - e.Location.Y);
                    if (lastClickedMouseLocation.Y > e.Location.Y)
                    {
                        cameraPosition.Y -= ViewConsts.SWIPE_OFFSET * cameraPosition.Z * yDiff;
                    }
                    else if (lastClickedMouseLocation.Y < e.Location.Y)
                    {
                        cameraPosition.Y += ViewConsts.SWIPE_OFFSET * cameraPosition.Z * yDiff;
                    }

                    cameraTarget.X = cameraPosition.X;
                    cameraTarget.Y = cameraPosition.Y;
                }

                lastClickedMouseLocation = e.Location;
            }
            else
            {
                lastClickedMouseLocation = Point.Empty;
            }
        }
    }
}

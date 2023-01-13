using System;
using System.Windows.Forms;
using Tram.Common.Consts;
using Tram.Common.Helpers;
using Tram.Controller;
using Tram.Controller.Controllers;
using Tram.Controller.Repositories;

namespace Tram.Simulation
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var lastTimeUpdate = DateTime.Now;

            ZTPRepository.Initialize();
            MapRepository.Initialize();
            VehicleRepository.Initialize();
            MainController controller = Kernel.Get<MainController>();
            DirectxController directxController = Kernel.Get<DirectxController>();
            controller.StartSimulation(new DateTime() + TimeConsts.SIMULATION_START);

            using (MainForm form = new MainForm())
            {
                int screenHeight = Screen.PrimaryScreen.Bounds.Height - 60;
                form.Size = new System.Drawing.Size(screenHeight * form.Width / form.Height, screenHeight);
                form.Init(controller, directxController);
                form.Show();

                while (form.Created)
                {
                    if ((DateTime.Now - lastTimeUpdate).TotalMilliseconds > TimeConsts.REFRESH)
                    {
                        lastTimeUpdate = DateTime.Now;
                        controller.Update(); // UPDATE SIMULATION
                        form.UpdateForm(); // UPDATE WINDOW
                    }

                    form.Render(controller.Render); //RENDER SIMULATION
                    Application.DoEvents();
                }
            }
        }
    }
}

using System;
using System.Windows.Forms;
using Tram.Common.Helpers;
using Tram.Controller;
using Tram.Controller.Controllers;
using Tram.Controller.Repositories;
using Tram.Simulation.Manager;

namespace Tram.Simulation
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //MainController controller = Kernel.Get<MainController>();
            //DirectxController directxController = Kernel.Get<DirectxController>();
            //controller.StartSimulation(TimeHelper.GetTime("05:30"));

            //using (MainForm form = new MainForm())
            //{
            //    int screenHeight = Screen.PrimaryScreen.Bounds.Height - 60;
            //    form.Size = new System.Drawing.Size(screenHeight * form.Width / form.Height, screenHeight);
            //    form.Init(controller, directxController);
            //    form.Show();

            //    while (form.Created)
            //    {
            //        controller.Update(); // UPDATE SIMULATION
            //        form.Render(controller.Render); //RENDER SIMULATION
            //        form.UpdateForm(); // UPDATE WINDOW
            //        Application.DoEvents();

            //    }
            //}



            var simulationManager = new SimulationManager();

            using (SimulationForm form = new SimulationForm(simulationManager))
            {
                ZTPRepository.Initialize();
                MapRepository.Initialize();
                form.Init();
                form.Show();
                simulationManager.LastTimeUpdate = DateTime.Now;
                simulationManager.IntervalMiliseconds = 1000;
                simulationManager.Timer = new TimeSpan(7, 0, 0);
                simulationManager.InitMap3();

                while (form.Created)
                {
                    if (simulationManager.ActiveSimulation)
                        if ((DateTime.Now - simulationManager.LastTimeUpdate).TotalMilliseconds > 1000)
                        {
                            simulationManager.UpdateSimulaton();
                            form.SetTimer(simulationManager.Timer);
                        }

                    form.Render(simulationManager.Render); //RENDER SIMULATION
                    Application.DoEvents();
                }
            }
        }
    }
}

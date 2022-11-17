using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tram.Simulation.Manager
{
    public class SimulationManager
    {
        public bool ActiveSimulation { get; set; }
        public int IntervalMiliseconds {get; set;}
        public TimeSpan Timer { get; set; }
        public DateTime LastTimeUpdate { get; set; }

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
    }
}

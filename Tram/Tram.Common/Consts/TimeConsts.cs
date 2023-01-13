using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tram.Common.Consts
{
    public static class TimeConsts
    {
        public static readonly TimeSpan SIMULATION_START = new TimeSpan(5, 0, 0);
        public static readonly TimeSpan SIMULATION_END = new TimeSpan(7, 0, 0);
        public static readonly int REFRESH = 10; // ms
        public static readonly int SIMULATION_UNIT = 1000; // ms
    }
}

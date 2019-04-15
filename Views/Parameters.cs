using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCarSim.Views
{
    class Parameters
    {
        private int _threads;
        private int _spawns;

        public int threads { get { return this._threads;} set { this._threads = value;} }
        public int spawns { get { return this._spawns; } set { this._spawns = value; } }
        public Parameters(int threads, int spawns)
        {
            this.threads = threads;
            this.spawns = spawns;
        }
    }
}

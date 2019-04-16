using AutoCarSim.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoCarSim.Models
{
    public abstract class Sensor : BaseModel
    {
        private List<Tile> _tiles;
        private int slowDown = 250;

        public List<Tile> tiles
        {
            get { return _tiles; }

            set { this.SetProperty(ref this._tiles, value); }
        }

        public int getSlowDown() { return slowDown; }

        public abstract bool check(int x, int y);
    }
}

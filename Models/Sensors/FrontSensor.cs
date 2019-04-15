using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoCarSim.Models.Sensors
{
    public class FrontSensor : Sensor
    {
        public FrontSensor(List<Tile> tiles)
        {
            this.tiles = tiles;
        }

        public override bool check(int x, int y)
        {
            bool turn = true;
            foreach (Tile tile in tiles)
            {
                if (tile.x == x && tile.y == y + 2) if (tile.isTaken) turn = false;
                if (tile.x == x && tile.y == y + 3) if (tile.isTaken) turn = false;
                if (tile.x == x && tile.y == y + 4) if (tile.isTaken) turn = false;
            }
            Thread.Sleep(this.getSlowDown());
            return turn;
        }
    }
}

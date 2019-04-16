using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoCarSim.Models.Sensors
{
    public class RightSensor : Sensor
    {
        public RightSensor(List<Tile> tiles)
        {
            this.tiles = tiles;
        }

        //check the right side of the autonomous car for possible movement
        public override bool check(int x, int y)
        {
            bool turn = true;
            foreach(Tile tile in tiles)
            {
                if (tile.x == x + 1 && tile.y == y) if (tile.isTaken) turn = false;
                if (tile.x == x + 2 && tile.y == y) if (tile.isTaken) turn = false;
                if (tile.x == x + 1 && tile.y == y + 1) if (tile.isTaken) turn = false;
                if (tile.x == x + 2 && tile.y == y + 1) if (tile.isTaken) turn = false;
            }
            Thread.Sleep(this.getSlowDown());
            return turn;
        }
    }
}

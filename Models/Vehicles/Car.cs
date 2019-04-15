using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCarSim.Models
{
    public class Car : Vehicle
    {
        public Car()
        {
            this.color = "Blue";
            this.speed = 1;
            this.length = 2;
        }

        public override void move(List<Tile> tiles)
        {
            if (tiles.Count() == 2)
            {
                this.saveTiles(tiles);
            }
            else
            {
                int position = 0;
                foreach(Tile tile in this.tiles)
                {
                    if (tile.y > position) position = tile.y;
                }
                List<Tile> endTiles = new List<Tile>();
                foreach (Tile tile in tiles)
                {
                    if (tile.x == this.tiles.First().x && tile.y == position - speed)
                    {
                        endTiles.Add(tile);
                    }
                    if (tile.x == this.tiles.First().x && tile.y == position - speed - length + 1)
                    {
                        endTiles.Add(tile);
                    }
                }
                this.saveTiles(endTiles);
            }
        }
    }
}

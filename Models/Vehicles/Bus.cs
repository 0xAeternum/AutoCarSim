using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCarSim.Models.Vehicles
{
    /**
     * The Bus will spawn on the opposing side of the autonomous car
     * It has a size of 3 tiles
     * The green tiles contain a bus
     */
    public class Bus : Vehicle
    {
        public Bus()
        {
            this.color = "Green";
            this.speed = 2;
            this.length = 3;
        }

        public override Task moveAsync(List<Tile> tiles)
        {
            throw new NotImplementedException();
        }

        public override void move(List<Tile> tiles)
        {
            int position = 0;

            //move vehicle
            foreach (Tile tile in this.tiles)
            {
                if (tile.y > position) position = tile.y;
            }

            //add the new tiles that contain the vehicle after movement done
            //and discard the previous tiles and turn them back into highway
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
                if (tile.x == this.tiles.First().x && tile.y == position - speed - length + 2)
                {
                    endTiles.Add(tile);
                }
            }
            this.saveTiles(endTiles);
        }
    }
}

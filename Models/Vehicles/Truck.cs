using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCarSim.Models.Vehicles
{
    /**
     * The Truck will spawn on the opposing direction of the autonomous car
     * The truck has a size of 4 tiles
     * Tiles of white contain a truck
     */
    public class Truck : Vehicle
    {
        public Truck()
        {
            this.color = "White";
            this.speed = 3;
            this.length = 4;
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
                if (tile.x == this.tiles.First().x && tile.y == position - speed - length + 3)
                {
                    endTiles.Add(tile);
                }
            }
            //save new tiles of the vehicle
            this.saveTiles(endTiles);
        }
    }
}

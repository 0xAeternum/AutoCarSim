using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCarSim.Models
{
    /**
     * The Car will spawn on the opposing direction of the autonomous car
     * The car has a size of 3 tiles
     * Tiles of blue contain a car
     */
    public class Car : Vehicle
    {
        public Car()
        {
            this.color = "Blue";
            this.speed = 1;
            this.length = 2;
        }

        public override Task moveAsync(List<Tile> tiles)
        {
            throw new NotImplementedException();
        }

        public override void move(List<Tile> tiles)
        {
            if (tiles.Count() == 2)
            {
                //perform checks on the tiles x and y position to make sure autonomous car does not "fall out" of the map.
                if(tiles[0].y >= 0 && tiles[0].x >= 0 && tiles[0].x < 7 && tiles[1].y < 20) this.saveTiles(tiles);
            }
            else
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
                }
                this.saveTiles(endTiles);
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCarSim.Models.Vehicles
{
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
            foreach (Tile tile in this.tiles)
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
                if (tile.x == this.tiles.First().x && tile.y == position - speed - length + 2)
                {
                    endTiles.Add(tile);
                }
                if (tile.x == this.tiles.First().x && tile.y == position - speed - length + 3)
                {
                    endTiles.Add(tile);
                }
            }
            this.saveTiles(endTiles);
        }
    }
}

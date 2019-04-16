using AutoCarSim.Models.Sensors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoCarSim.Models
{
    public class AutonomousCar : Car
    {
        private List<Sensor> _sensors = new List<Sensor>();

        public AutonomousCar(int amountOfThreads, List<Tile> tiles)
        {
            this.color = "Red";
            this.speed = 1;
            this.length = 2;
            makeSensors(amountOfThreads, tiles);
        }

        public List<Sensor> sensors
        {
            get { return _sensors; }

            set { this.SetProperty(ref this._sensors, value); }
        }

        public override void move(List<Tile> tiles)
        {
            bool accelerate = false;
            bool turnLeft = false;
            bool turnRight = false;
            bool slowDown = false;
            bool cruize = false;

            new Thread(new ThreadStart(() =>
            {
                accelerate = sensors[0].check(this.tiles[0].x, this.tiles[0].y);
            }));

            if (accelerate)
            {
                List<Tile> endTiles = new List<Tile>();
                foreach (Tile tile in tiles)
                {
                    if (tile.x == this.tiles[0].x && tile.y == this.tiles[0].y + 1)
                    {
                        endTiles.Add(tile);
                    }
                    if (tile.x == this.tiles[1].x && tile.y == this.tiles[1].y + 1)
                    {
                        endTiles.Add(tile);
                    }
                }
                Debug.WriteLine("ACCELERATE");
                base.move(endTiles);
            }
            else if (sensors[1].check(this.tiles[0].x, this.tiles[0].y) && sensors[2].check(this.tiles[0].x, this.tiles[0].y))
            {
                List<Tile> endTiles = new List<Tile>();
                foreach (Tile tile in tiles)
                {
                    if (tile.x == this.tiles[0].x - 1 && tile.y == this.tiles[0].y - 1)
                    {
                        endTiles.Add(tile);
                    }
                    if (tile.x == this.tiles[1].x - 1 && tile.y == this.tiles[1].y - 1)
                    {
                        endTiles.Add(tile);
                    }
                }
                Debug.WriteLine("TURN LEFT");
                base.move(endTiles);
            }
            else if (sensors[3].check(this.tiles[0].x, this.tiles[0].y) && sensors[4].check(this.tiles[0].x, this.tiles[0].y))
            {
                List<Tile> endTiles = new List<Tile>();
                foreach (Tile tile in tiles)
                {
                    if (tile.x == this.tiles[0].x + 1 && tile.y == this.tiles[0].y - 1)
                    {
                        endTiles.Add(tile);
                    }
                    if (tile.x == this.tiles[1].x + 1 && tile.y == this.tiles[1].y - 1)
                    {
                        endTiles.Add(tile);
                    }
                }
                Debug.WriteLine("TURN RIGHT");
                base.move(endTiles);
            }
            else if (sensors[5].check(this.tiles[0].x, this.tiles[0].y))
            {
                List<Tile> endTiles = new List<Tile>();
                foreach (Tile tile in tiles)
                {
                    if (tile.x == this.tiles[0].x && tile.y == this.tiles[0].y - 2)
                    {
                        endTiles.Add(tile);
                    }
                    if (tile.x == this.tiles[1].x && tile.y == this.tiles[1].y - 2)
                    {
                        endTiles.Add(tile);
                    }
                }
                Debug.WriteLine("SLOW DOWN");
                base.move(endTiles);
            }
            else
            {
                List<Tile> endTiles = new List<Tile>();
                foreach (Tile tile in tiles)
                {
                    if (tile.x == this.tiles[0].x)
                    {
                        endTiles.Add(tile);
                    }
                }
                Debug.WriteLine("CRUIZE");
                base.move(endTiles);
            }
        }

        private void makeSensors(int amountOfThreads, List<Tile> tiles)
        {
       
            switch (amountOfThreads)
            {
                case 1:
                    sensors.Add(new FrontSensor(tiles));
                    sensors.Add(new FrontLeftSensor(tiles));
                    sensors.Add(new LeftSensor(tiles));
                    sensors.Add(new FrontRightSensor(tiles));
                    sensors.Add(new RightSensor(tiles));
                    sensors.Add(new BackSensor(tiles));
                    break;
                case 2:
                    //TODO: Make threads
                    break;
                case 6:
                    //TODO: Make threads
                    break;
            }
        }
    }
}

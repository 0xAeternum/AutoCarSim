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
    /**
     * Autonomous car moves on its own depending on result from sensors
     * it has a length of 2 tiles
     * the red colored tiles contain the autonomous car
     */
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

        public override async Task moveAsync(List<Tile> tiles)
        {
            bool accelerate = false;
            bool turnLeft = false;
            bool turnRight = false;
            bool slowDown = false;

            Task sensor1 = new Task(() => { });
            Task sensor2 = new Task(() => { });
            Task sensor3 = new Task(() => { });
            Task sensor4 = new Task(() => { });
            if ((int)Windows.Storage.ApplicationData.Current.LocalSettings.Values["threads"] > 1) {
                for (int i = 0; i < (int)Windows.Storage.ApplicationData.Current.LocalSettings.Values["threads"]; i++)
                {
                    switch (i)
                    {
                        case 0:
                            sensor1 = new Task(() =>
                            {
                                //check if vehicle should accelerate
                                accelerate = sensors[0].check(this.tiles[0].x, this.tiles[0].y);
                            });
                            sensor1.Start();
                            break;
                        case 1:
                            sensor2 = new Task(() =>
                            {
                                //check if turning left is possible
                                turnLeft = sensors[1].check(this.tiles[0].x, this.tiles[0].y) && sensors[2].check(this.tiles[0].x, this.tiles[0].y);
                            });
                            sensor2.Start();
                            break;
                        case 2:
                            sensor3 = new Task(() =>
                            {
                                //check if turning right is possible
                                turnRight = sensors[3].check(this.tiles[0].x, this.tiles[0].y) && sensors[4].check(this.tiles[0].x, this.tiles[0].y);
                            });
                            sensor3.Start();
                            break;
                        case 3:
                            sensor4 = new Task(() =>
                            {
                                //check if vehicle should slow down
                                slowDown = sensors[5].check(this.tiles[0].x, this.tiles[0].y);
                            });
                            sensor4.Start();
                            break;
                        default:
                            break;
                    }
                }
                await Task.WhenAll(sensor1, sensor2, sensor3, sensor4);
            }

            if (accelerate || sensors[0].check(this.tiles[0].x, this.tiles[0].y))
            {
                //adjust tiles and tile colours depending on next movement
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
            else if (turnLeft || sensors[1].check(this.tiles[0].x, this.tiles[0].y) && sensors[2].check(this.tiles[0].x, this.tiles[0].y))
            {
                //adjust tiles and tile colours depending on next movement
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
            else if (turnRight || sensors[3].check(this.tiles[0].x, this.tiles[0].y) && sensors[4].check(this.tiles[0].x, this.tiles[0].y))
            {
                //adjust tiles and tile colours depending on next movement
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
            else if (slowDown || sensors[5].check(this.tiles[0].x, this.tiles[0].y))
            {
                //adjust tiles and tile colours depending on next movement
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
                //autonomous vehicle did not move, adjust tiles depending on enemy vehicle movement
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
            sensors.Add(new FrontSensor(tiles));
            sensors.Add(new FrontLeftSensor(tiles));
            sensors.Add(new LeftSensor(tiles));
            sensors.Add(new FrontRightSensor(tiles));
            sensors.Add(new RightSensor(tiles));
            sensors.Add(new BackSensor(tiles));
        }
    }
}

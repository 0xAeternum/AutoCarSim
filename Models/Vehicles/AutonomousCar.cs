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
                                accelerate = sensors[0].check(this.tiles[0].x, this.tiles[0].y);
                            });
                            sensor1.Start();
                            //Task.WaitAll(sensor1);
                            break;
                        case 1:
                            sensor2 = new Task(() =>
                            {
                                turnLeft = sensors[1].check(this.tiles[0].x, this.tiles[0].y) && sensors[2].check(this.tiles[0].x, this.tiles[0].y);
                            });
                            sensor2.Start();
                            //Task.WaitAll(sensor2);
                            break;
                        case 2:
                            sensor3 = new Task(() =>
                            {
                                turnRight = sensors[3].check(this.tiles[0].x, this.tiles[0].y) && sensors[4].check(this.tiles[0].x, this.tiles[0].y);
                            });
                            sensor3.Start();
                            //Task.WaitAll(sensor3);
                            break;
                        case 3:
                            sensor4 = new Task(() =>
                            {
                                slowDown = sensors[5].check(this.tiles[0].x, this.tiles[0].y);
                            });
                            sensor4.Start();
                            //Task.WaitAll(sensor4);
                            break;
                        default:
                            break;
                    }
                }
                await Task.WhenAll(sensor1, sensor2, sensor3, sensor4);
            }

            Debug.WriteLine(accelerate);


            if (accelerate || sensors[0].check(this.tiles[0].x, this.tiles[0].y))
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
            else if (turnLeft || sensors[1].check(this.tiles[0].x, this.tiles[0].y) && sensors[2].check(this.tiles[0].x, this.tiles[0].y))
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
            else if (turnRight || sensors[3].check(this.tiles[0].x, this.tiles[0].y) && sensors[4].check(this.tiles[0].x, this.tiles[0].y))
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
            else if (slowDown || sensors[5].check(this.tiles[0].x, this.tiles[0].y))
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
            sensors.Add(new FrontSensor(tiles));
            sensors.Add(new FrontLeftSensor(tiles));
            sensors.Add(new LeftSensor(tiles));
            sensors.Add(new FrontRightSensor(tiles));
            sensors.Add(new RightSensor(tiles));
            sensors.Add(new BackSensor(tiles));

           
        }
    }
}

using AutoCarSim.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCarSim.Models
{
    public class Map : BaseModel
    {
        private List<Tile> _tiles = new List<Tile>();
        private List<Vehicle> _vehicles = new List<Vehicle>();
        private List<Vehicle> _avehicles = new List<Vehicle>();

        public List<Tile> tiles
        {
            get { return _tiles; }

            set { this.SetProperty(ref this._tiles, value); }
        }

        public List<Vehicle> vehicles
        {
            get { return _vehicles; }

            set { this.SetProperty(ref this._vehicles, value); }
        }

        public List<Vehicle> avehicles
        {
            get { return _avehicles; }

            set { this.SetProperty(ref this._avehicles, value); }
        }

        public void populateMap(int height, int length, int amountOfCars, int amountOfThreads)
        {
            //create tiles
            for (int x = 0; x < height; x++)
            {
                for (int y = 0; y < length; y++)
                {
                    this.tiles.Add(new Tile(x, y));
                }
            }

            //create autonomous vehicles
            int z = 0;
            while (z < amountOfCars)
            {
                this.avehicles.Add(new AutonomousCar(amountOfThreads, this.tiles));
                z++;
            }
            this.addVehicles(height);
        }

        public void makeStep()
        {
            //move avehicles
            foreach(Vehicle avehicle in avehicles)
            {
                foreach (Tile vehicleTile in avehicle.tiles)
                {
                    foreach (Tile tile in tiles)
                    {
                        if (vehicleTile.x == tile.x && vehicleTile.y == tile.y)
                        {
                            tile.color = "Black";
                            tile.isTaken = false;
                        }
                    }
                }
                avehicle.move(tiles);
                foreach (Tile vehicleTile in avehicle.tiles)
                {
                    foreach (Tile tile in tiles)
                    {
                        if (vehicleTile.x == tile.x && vehicleTile.y == tile.y)
                        {
                            tile.color = avehicle.color;
                            tile.isTaken = true;
                        }
                    }
                }
            }
            //move vehicles
            foreach (Vehicle vehicle in vehicles)
            {
                List<Tile> endTiles = new List<Tile>();
                foreach (Tile tile in tiles)
                {
                    if (tile.x == vehicle.tiles[0].x)
                    {
                        Tile newTile = tile;
                        endTiles.Add(newTile);
                    }
                }
                foreach (Tile vehicleTile in vehicle.tiles)
                {
                    foreach (Tile tile in tiles)
                    {
                        if (vehicleTile.x == tile.x && vehicleTile.y == tile.y)
                        {
                            tile.color = "Black";
                            tile.isTaken = false;
                        }
                    }
                }
                vehicle.move(endTiles);
                foreach (Tile vehicleTile in vehicle.tiles)
                {
                    foreach(Tile tile in tiles)
                    {
                        if(vehicleTile.x == tile.x && vehicleTile.y == tile.y)
                        {
                            tile.color = vehicle.color;
                            tile.isTaken = true;
                        }
                    }
                }
            }
            //TODO: Remove - for testing
            for (int i = 0; i < tiles.Count(); i += 40)
            {
                String text = "";
                for (int j = 0; j < 40; j++)
                {
                    if (tiles[i + j].color == "Black")
                    {
                        text += "-";
                    }
                    else if (tiles[i + j].color == "Red")
                    {
                        text += "x";
                    }
                    else
                    {
                        text += "o";
                    }
                }
                Debug.WriteLine(text);
            }
        }

        public bool checkCrash()
        {
            bool crash = false;
            //check for crash
            if(crash == true)
            {
                return true;
            }
            return false;
        }

        public void addVehicles(int height)
        {
            //add autonomous vehicles to map - on start only
            if (avehicles.Count() == 1)
            {
                List<Tile> tiles = new List<Tile>();
                foreach (Tile tile in this.tiles)
                {
                    if(tile.x == height / 2 && tile.y == 0)
                    {
                        tiles.Add(tile);
                    }
                    if (tile.x == height / 2 && tile.y == 1)
                    {
                        tiles.Add(tile);
                    }
                }
                avehicles.First().saveTiles(tiles);
                foreach (Tile tile in tiles)
                {
                    tile.color = avehicles.First().color;
                    tile.isTaken = true;

                }
            }
            else
            {
                //TODO: Add for multiple autonomous vehicles
            }
        }

        public void addNewVehicle(int lane, int length)
        {
            Vehicle newVehicle = vehicles.Last();
            List<Tile> vahicleTiles = new List<Tile>();
            for (int i = newVehicle.length; i >= 0; i--)
            {
                foreach (Tile tile in tiles)
                {
                    if (tile.x == lane && tile.y == length - i)
                    {
                        vahicleTiles.Add(tile);
                    }
                }
            }
            newVehicle.saveTiles(vahicleTiles);
            foreach (Tile tile in tiles)
            {
                tile.color = newVehicle.color;
                tile.isTaken = true;
            }
        }
    }
}

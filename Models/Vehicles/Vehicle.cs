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
    public abstract class Vehicle : BaseModel
    {
        private String _color;
        private int _speed;
        private int _length;
        private List<Tile> _tiles;

        public String color
        {
            get { return _color; }

            set { this.SetProperty(ref this._color, value); }
        }

        public int speed
        {
            get { return _speed; }

            set { this.SetProperty(ref this._speed, value); }
        }

        public int length
        {
            get { return _length; }

            set { this.SetProperty(ref this._length, value); }
        }

        public List<Tile> tiles
        {
            get { return _tiles; }

            set { this.SetProperty(ref this._tiles, value); }
        }

        public void saveTiles(List<Tile> newTiles)
        {
            tiles = new List<Tile>();
            foreach(Tile tile in newTiles)
            {
                tiles.Add(tile);
            }
        }

        public abstract void move(List<Tile> tiles);
        public abstract Task moveAsync(List<Tile> tiles);
    }
}

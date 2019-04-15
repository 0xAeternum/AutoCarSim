using AutoCarSim.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCarSim.Models
{
    public class Tile : BaseModel
    {
        private int _x;
        private int _y;
        private String _color = "Black";
        private bool _isTaken;

        public Tile(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.isTaken = false;
        }

        public int x
        {
            get { return _x; }

            set { this.SetProperty(ref this._x, value); }
        }

        public int y
        {
            get { return _y; }

            set { this.SetProperty(ref this._y, value); }
        }

        public String color
        {
            get { return _color; }

            set { this.SetProperty(ref this._color, value); }
        }

        public bool isTaken
        {
            get { return _isTaken; }

            set { this.SetProperty(ref this._isTaken, value); }
        }
    }
}

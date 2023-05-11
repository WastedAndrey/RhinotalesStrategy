using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PathFindLib.PathMap
{
    public class PathMapPoint
    {
        public int X;
        public int Y;

        public bool Passable;
        public int GraphNodeOwner;

        public PathMapPoint(int x, int y, bool passable)
        { 
            X = x;
            Y = y;
            Passable = passable;
            GraphNodeOwner = -1;
        }

    }
}

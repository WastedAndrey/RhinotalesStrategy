using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathFindLib.GraphPathFind;

namespace PathFindLib.PathMap
{
    public class MapGraphNode : GraphPathFindNode
    {
        public int X, Y;
        public int Width, Height;
        public int CenterX, CenterY;

        public MapGraphNode(List<GraphPathFindDirection> directions, int ID)
            : base(directions, ID)
        { init(0, 0, 0, 0); }


        public MapGraphNode(List<GraphPathFindDirection> directions, int ID, int x, int y, int width, int height) 
            : base(directions, ID)
        { init(x, y, width, height); }

        public void init(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            CenterX = X + width / 2;
            CenterY = Y + height / 2;
        }
    }
}

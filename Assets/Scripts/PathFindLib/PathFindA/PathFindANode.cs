using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PathFindLib.PathFindA
{
    public class PathFindANode
    {
        public bool Passable;
        public int StepsCost = 99999;
        public int HeuristicCost;
        public int TotalCost;
        public int x, y;
        public PathFindANode parent;

        public PathFindANode(int x, int y, bool passable)
        {
            Passable = passable;
            this.x = x;
            this.y = y;
        }
    }
}

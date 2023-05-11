using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PathFindLib.GraphPathFind
{
    public struct GraphPathFindDirection
    {
        public int LinkWith; // номер ячейки графа, к которому ведёт это направление
        public int Cost;     // стоимость прохождения по направлению
        
        public GraphPathFindDirection(int linkWith, int cost)
        {
            Cost = cost;
            LinkWith = linkWith;
        }
    }

    public class GraphPathFindNode
    {
        public int ID;
        public List<GraphPathFindDirection> Directions;
        public int TotalCost = 99999;
        public int ParentLink;

        public GraphPathFindNode(List<GraphPathFindDirection> directions, int ID)
        {
            this.ID = ID;
            Directions = directions;
        }

        public void Default()
        {
            TotalCost = 99999;
            ParentLink = 0;
        }
    }
}

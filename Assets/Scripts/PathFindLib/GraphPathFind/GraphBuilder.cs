using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathFindLib.PathFindA;

namespace PathFindLib.GraphPathFind
{
    public class GraphBuilder
    {
        PointA[] movements = new PointA[] {new PointA(-1, 0),
                                            new PointA(1, 0),
                                            new PointA(0, -1),
                                            new PointA(0, 1),
                                            };




        public List<GraphPathFindNode> BuildGraph(bool[,] field, int maxCellSize)
        {
            int width;
            int height;


            width = field.GetLength(0);
            height = field.GetLength(1);


            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < width; j++)
                { 
                    
                }
            }

            return null;
        }


        public List<GraphPathFindNode> BuildNodeList(bool[,] field, int cellSize, int cellX, int cellY)
        {
            int fieldX = cellX * cellSize;
            int fieldY = cellY * cellSize;

            for (int i = cellX; i < fieldX + cellSize; i++)
            {

            }


            return null;
        }
    }
}

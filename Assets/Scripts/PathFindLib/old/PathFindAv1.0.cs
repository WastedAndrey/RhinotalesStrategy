using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
/*
namespace PathFindLib.PathFindA
{
    public class PathFindA
    {
        PointA[] movements = new PointA[] {new PointA(-1, 0),
                                            new PointA(1, 0),
                                            new PointA(0, -1),
                                            new PointA(0, 1),
                                            new PointA(-1, -1),
                                            new PointA(-1, 1),
                                            new PointA(1, -1),
                                            new PointA(1, 1), 
                                            };

        float ticks1 = 0;
        float ticks2 = 0;




        public List<PointA> PathFind(PointA position, PointA target, bool[,] field)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();

            ticks2 = 0;

            int width = field.GetLength(0);
            int height = field.GetLength(1);

            bool madeProgress = false;

            PathFinderNode[,] NodeMap = new PathFinderNode[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    NodeMap[i, j] = new PathFinderNode(i, j, field[i, j]);
                }
            }
            NodeMap[position.X, position.Y].StepsCost = 0;


            List<PathFinderNode> openNodes = new List<PathFinderNode>();
            openNodes.Add(NodeMap[position.X, position.Y]);

            while (madeProgress == false)
            {
                if (openNodes.Count > 0)
                {
                    PathFinderNode currentNode = Pop(openNodes);

                    int currentStepsCost = NodeMap[currentNode.X, currentNode.Y].StepsCost;
                    int newStepsCost = currentStepsCost + 10;

                    for (int i = 0; i < 8; i++)
                    {
                        if (i > 3) { newStepsCost = currentStepsCost + 14; }

                        int X = currentNode.X + movements[i].X;
                        int Y = currentNode.Y + movements[i].Y;

                        if (X >= 0 && Y >= 0 && X < width && Y < height)
                        {
                            var nextNode = NodeMap[X, Y];

                            if (field[nextNode.X, nextNode.Y] == true)
                            {
                                if (target.X == nextNode.X && target.Y == nextNode.Y)
                                {
                                    madeProgress = true;
                                    nextNode.parent = currentNode;
                                    break;
                                }

                                if (nextNode.StepsCost > newStepsCost)
                                {
                                    nextNode.StepsCost = newStepsCost;
                                    nextNode.parent = currentNode;
                                    nextNode.HeuristicCost = 20 * (Math.Max(Math.Abs(nextNode.X - target.X), Math.Abs(nextNode.Y - target.Y)));
                                    nextNode.TotalCost = nextNode.StepsCost + nextNode.HeuristicCost;

                                    Push(nextNode, openNodes);
                                }
                            }
                        }
                    }
                }
                else
                {
                    return null;
                }

            }
            PathFinderNode addingNode;
            addingNode = NodeMap[target.X, target.Y];
            List<PointA> result = new List<PointA>();

            while (addingNode.StepsCost > 0)
            { 
                result.Add(new PointA(addingNode.parent.X, addingNode.parent.Y));
                addingNode = addingNode.parent;
            }


            stopWatch.Stop();
            ticks1 = (stopWatch.ElapsedTicks * 1000) / (float)Stopwatch.Frequency;
            ticks2 = 0;

            return result;
        }




        public void Push(PathFinderNode node, List<PathFinderNode> openNodes)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();

            bool placed = false;
            int currentPlace = openNodes.Count / 2;
            int shift = openNodes.Count / 2;

            if (openNodes.Count == 0) { placed = true; }

            while (!placed)
            {
                if (currentPlace < 0) {currentPlace=0;}
                if (currentPlace >= openNodes.Count) { currentPlace = openNodes.Count-1; }

                if (currentPlace == 0)
                {
                    if (openNodes[currentPlace].TotalCost > node.TotalCost)
                    { placed = true; }
                }

                if (!placed && openNodes[currentPlace].TotalCost <= node.TotalCost)
                {
                    if (currentPlace + 1 < openNodes.Count)
                    {
                        if (openNodes[currentPlace + 1].TotalCost > node.TotalCost)
                        {
                            placed = true;
                            currentPlace++;
                        }
                    }
                    else
                    { placed = true; }
                }

                if (!placed)
                {
                    if (shift == 0) { shift = 1; }

                    if (shift % 2 == 0)
                    {
                        shift = shift >> 1;
                    }
                    else
                    {
                        shift = (shift+1) >> 1;
                    }

                    if (openNodes[currentPlace].TotalCost > node.TotalCost)
                    { currentPlace = currentPlace - shift; }
                    else
                    { currentPlace = currentPlace + shift; }
                }
            }

            if (placed)
            {
                if (currentPlace < openNodes.Count)
                { openNodes.Insert(currentPlace, node); }
                else
                { openNodes.Add(node);  }
            }

            stopWatch.Stop();
            ticks2 += (stopWatch.ElapsedTicks * 1000) / (float)Stopwatch.Frequency;
        }

        public PathFinderNode Pop(List<PathFinderNode> openNodes)
        {
            if (openNodes.Count > 0)
            {
                var result = openNodes[0];
                openNodes.RemoveAt(0);
                return result; 
            }
            else
            { return null; }
        }


    }
}
*/
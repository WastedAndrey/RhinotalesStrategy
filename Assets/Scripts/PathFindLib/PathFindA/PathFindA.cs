using System;
using System.Collections.Generic;
using UnityEngine;

namespace PathFindLib.PathFindA
{
    public class PathFindA
    {
        static Vector2Int[] movements = new Vector2Int[] {new Vector2Int(-1, 0),
                                            new Vector2Int(1, 0),
                                            new Vector2Int(0, -1),
                                            new Vector2Int(0, 1),
                                           // new Vector2Int(-1, -1),
                                           // new Vector2Int(-1, 1),
                                           // new Vector2Int(1, -1),
                                           // new Vector2Int(1, 1),
                                            };



        public static List<Vector2Int> PathFind(Vector2Int position, Vector2Int target, bool[,] field)
        {
            int width = field.GetLength(0);
            int height = field.GetLength(1);

            bool madeProgress = false;

            PathFindANode[,] NodeMap = new PathFindANode[width, height];

            /* for (int i = 0; i < width; i++)
             {
                 for (int j = 0; j < width; j++)
                 {
                     NodeMap[i, j] = new PathFindANode(i, j, field[i, j]);
                 }
             }*/
            NodeMap[position.x, position.y] = new PathFindANode(position.x, position.y, field[position.x, position.y]);

            NodeMap[position.x, position.y].StepsCost = 0;


            List<PathFindANode> openNodes = new List<PathFindANode>();
            openNodes.Add(NodeMap[position.x, position.y]);

            while (madeProgress == false)
            {
                if (openNodes.Count > 0)
                {
                    PathFindANode currentNode = Pop(openNodes);

                    int currentStepsCost = NodeMap[currentNode.x, currentNode.y].StepsCost;
                    int newStepsCost = currentStepsCost + 10;

                    for (int i = 0; i < movements.GetLength(0); i++)
                    {
                        if (i > 3) { newStepsCost = currentStepsCost + 14; }

                        int X = currentNode.x + movements[i].x;
                        int Y = currentNode.y + movements[i].y;

                        if (X >= 0 && Y >= 0 && X < width && Y < height)
                        {
                            if (NodeMap[X, Y] == null)
                            { NodeMap[X, Y] = new PathFindANode(X, Y, field[X, Y]); }

                            var nextNode = NodeMap[X, Y];

                            if (field[nextNode.x, nextNode.y] == true)
                            {
                                if (target.x == nextNode.x && target.y == nextNode.y)
                                {
                                    madeProgress = true;
                                    nextNode.parent = currentNode;
                                    break;
                                }

                                if (nextNode.StepsCost > newStepsCost)
                                {
                                    nextNode.StepsCost = newStepsCost;
                                    nextNode.parent = currentNode;
                                    nextNode.HeuristicCost = 20 * (Math.Max(Math.Abs(nextNode.x - target.x), Math.Abs(nextNode.y - target.y)));
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
            PathFindANode addingNode;
            addingNode = NodeMap[target.x, target.y];
            List<Vector2Int> result = new List<Vector2Int>();

            while (addingNode.StepsCost > 0)
            {
                result.Add(new Vector2Int(addingNode.parent.x, addingNode.parent.y));
                addingNode = addingNode.parent;
            }




            return result;
        }




        private static void Push(PathFindANode node, List<PathFindANode> openNodes)
        {
            bool placed = false;
            int currentPlace = openNodes.Count / 2;
            int shift = openNodes.Count / 2;

            if (openNodes.Count == 0) { placed = true; }

            while (!placed)
            {
                if (currentPlace < 0) { currentPlace = 0; }
                if (currentPlace >= openNodes.Count) { currentPlace = openNodes.Count - 1; }

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
                        shift = (shift + 1) >> 1;
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
                { openNodes.Add(node); }
            }
        }

        private static PathFindANode Pop(List<PathFindANode> openNodes)
        {
            var result = openNodes[0];
            openNodes.RemoveAt(0);
            return result;
        }


    }
}

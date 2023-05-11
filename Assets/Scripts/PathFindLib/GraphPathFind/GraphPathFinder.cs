using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PathFindLib.GraphPathFind
{
    public class GraphPathFinder
    {
        public List<int> Pathfind(int position, int target, List<GraphPathFindNode> graph)
        {
            bool madeProgress = false;

            var openNodes = new List<GraphPathFindNode>();

            graph[position].TotalCost = 0;
            openNodes.Add(graph[position]);

            while (madeProgress == false)
            {
                if (openNodes.Count > 0)
                {
                    var currentNode = Pop(openNodes);

                    for (int i = 0; i < currentNode.Directions.Count; i++)
                    {
                        var nextNode = graph[currentNode.Directions[i].LinkWith];

                        int newTotalCost = currentNode.TotalCost + currentNode.Directions[i].Cost;

                        if (target == nextNode.ID)
                        {
                            madeProgress = true;
                            nextNode.ParentLink = currentNode.ID;
                            break;
                        }

                        if (newTotalCost < nextNode.TotalCost)
                        {
                            nextNode.TotalCost = newTotalCost;
                            nextNode.ParentLink = currentNode.ID;

                            Push(nextNode, openNodes);
                        } 
                    }
                }
                else
                {
                    return null;
                }
            }

            var addingNode = graph[target];
            var result = new List<int>();

            while (addingNode.TotalCost > 0)
            {
                result.Add(addingNode.ID);
                addingNode = graph[addingNode.ParentLink];
            }

            return result;
        }

        public GraphPathFindNode Pop(List<GraphPathFindNode> openNodes)
        {
            var result = openNodes[0];
            openNodes.RemoveAt(0);
            return result;
        }

        public void Push(GraphPathFindNode node, List<GraphPathFindNode> openNodes)
        {   
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
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathFindLib.GraphPathFind;
using PathFindLib.PathFindA;

namespace PathFindLib.PathMap
{
    public class MapRegion
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public bool Passable;
        public List<int> RegionLinks;

        public MapRegion() 
        {
            init(0,0,0,0,false);
        }

        public MapRegion(int x, int y, int width, int height, bool passable)
        {
            init(x,y,width,height,passable);
        }

        private void init(int x, int y, int width, int height, bool passable)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Passable = passable;
            RegionLinks = new List<int>();
        }
    }

    public class PathMap
    {
        public List<GraphPathFindNode> Graph;
        public List<MapRegion> Regions;

        private PathMapPoint[,] Points;
        private GraphPathFinder GraphPathFinder = new GraphPathFinder();

        private int maxCellSize;

        public PathMap(bool[,] field, int maxCellSize) { init(field, maxCellSize); }

        private void init(bool[,] field, int maxCellSize)
        {
            this.maxCellSize = maxCellSize;

            int width;
            int height;
            width = field.GetLength(0);
            height = field.GetLength(1);

            Points = new PathMapPoint[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Points[i, j] = new PathMapPoint(i, j, field[i, j]);
                }
            }

            BuildMap();

        }

        public List<int> FindPath(PointA position, PointA target)
        {
            int positionNodeNumber = Points[position.X, position.Y].GraphNodeOwner;
            int targetNodeNumber = Points[target.X, target.Y].GraphNodeOwner;

            for (int i = 0; i < Graph.Count; i++)
            { Graph[i].Default(); }

            List<int> result = GraphPathFinder.Pathfind(positionNodeNumber, targetNodeNumber, Graph);

            return result;
        }

        private void BuildMap()
        {
            
            Regions = new List<MapRegion>();

            int width;
            int height;
            width = Points.GetLength(0);
            height = Points.GetLength(1);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (Points[i, j].GraphNodeOwner == -1)
                    {
                        if (Points[i, j].Passable == true)
                        {
                            AddRegion(i, j, true);
                        }
                        else
                        {
                            AddRegion(i, j, false);
                        }
                    }
                }
            }

            for (int i = 0; i < Regions.Count; i++)
            {
                CheckRegionBorders(i);
            }

            BuildGraph();

        }

        private void AddRegion(int i, int j, bool passable)
        {
            int progressStep = 0;
            int rightProgress = 0;
            int bottomProgress = 0;
            bool rightProgressContinue = true;
            bool bottomProgressContinue = true;

            while (true)
            {
                if (rightProgressContinue == true)
                {
                    if (i + rightProgress >= Points.GetLength(0))
                    {
                        rightProgressContinue = false;
                    }
                }

                if (bottomProgressContinue == true)
                {
                    if (j + bottomProgress >= Points.GetLength(1))
                    {
                        bottomProgressContinue = false;
                    }
                }


                if (bottomProgressContinue == true)
                {
                    for (int k = 0; k < rightProgress; k++)
                    {
                        if (Points[i + k, j + bottomProgress].Passable == !passable)
                        {
                            bottomProgressContinue = false;
                        }
                        if (Points[i + k, j + bottomProgress].GraphNodeOwner != -1)
                        {
                            bottomProgressContinue = false;
                        }
                    }
                }

                if (rightProgressContinue == true)
                {
                    for (int k = 0; k < bottomProgress; k++)
                    {
                        if (Points[i + rightProgress, j + k].Passable == !passable)
                        {
                            rightProgressContinue = false;
                        }
                        if (Points[i + rightProgress, j + k].GraphNodeOwner != -1)
                        {
                            rightProgressContinue = false;
                        }
                    }
                }

                if (rightProgressContinue == true && bottomProgressContinue == true) // проверка угловой клетки
                {
                    if (Points[i + rightProgress, j + bottomProgress].Passable == !passable
                        || Points[i + rightProgress, j + bottomProgress].GraphNodeOwner != -1)
                    {
                        rightProgressContinue = false;
                    }
                }

                


                if (rightProgressContinue == false && bottomProgressContinue == false)
                {
                    break;
                }

                if (progressStep + 1 <= maxCellSize)
                {
                    progressStep++;
                }
                else
                {
                    break;
                }

                if (rightProgressContinue == true)
                { rightProgress++; }

                if (bottomProgressContinue == true)
                { bottomProgress++; }

            }

            Regions.Add(new MapRegion(i, j, rightProgress, bottomProgress, passable));

            for (int k = 0; k < rightProgress; k++)
            {
                for (int l = 0; l < bottomProgress; l++)
                {
                    Points[i + k, j + l].GraphNodeOwner = Regions.Count-1;
                }
            }
 
        }

        private void CheckRegionBorders(int regionNumber)
        {
            if (Regions[regionNumber].Passable == false)
            { return;  }

            if (Regions[regionNumber].Y - 1 >= 0)
            {
                for (int i = Regions[regionNumber].X; i < Regions[regionNumber].X + Regions[regionNumber].Width; i++)
                {
                    if (Points[i, Regions[regionNumber].Y-1].Passable==true && Points[i, Regions[regionNumber].Y-1].GraphNodeOwner != -1)
                    {
                        AddLink(regionNumber, Points[i, Regions[regionNumber].Y - 1].GraphNodeOwner);
                    }   
                }
            }

            if (Regions[regionNumber].Y + Regions[regionNumber].Height < Points.GetLength(1))
            {
                for (int i = Regions[regionNumber].X; i < Regions[regionNumber].X + Regions[regionNumber].Width; i++)
                {
                    if (Points[i, Regions[regionNumber].Y + Regions[regionNumber].Height].Passable == true
                        && Points[i, Regions[regionNumber].Y + Regions[regionNumber].Height].GraphNodeOwner != -1)
                    {
                        AddLink(regionNumber, Points[i, Regions[regionNumber].Y + Regions[regionNumber].Height].GraphNodeOwner);
                    }
                }
            }

            if (Regions[regionNumber].X - 1 >= 0)
            {
                for (int i = Regions[regionNumber].Y; i < Regions[regionNumber].Y + Regions[regionNumber].Height; i++)
                {
                    if (Points[Regions[regionNumber].X - 1, i].Passable == true && Points[Regions[regionNumber].X - 1, i].GraphNodeOwner != -1)
                    {
                        AddLink(regionNumber, Points[Regions[regionNumber].X - 1, i].GraphNodeOwner);
                    }
                }
            }

            if (Regions[regionNumber].X + Regions[regionNumber].Width < Points.GetLength(0))
            {
                for (int i = Regions[regionNumber].Y; i < Regions[regionNumber].Y + Regions[regionNumber].Height; i++)
                {
                    if (Points[Regions[regionNumber].X + Regions[regionNumber].Width, i].Passable == true
                        && Points[Regions[regionNumber].X + Regions[regionNumber].Width, i].GraphNodeOwner != -1)
                    {
                        AddLink(regionNumber, Points[Regions[regionNumber].X + Regions[regionNumber].Width, i].GraphNodeOwner);
                    }
                }
            }

        }

        private void AddLink(int region1, int region2)
        {
            if (region1 == region2)
            {
                return;
            }

            bool addLink = true;
            for (int i = 0; i < Regions[region1].RegionLinks.Count; i++)
            {
                if (Regions[region1].RegionLinks[i] == region2)
                { addLink = false; }
            }
            if (addLink == true)
            {
                Regions[region1].RegionLinks.Add(region2);
            }

            addLink = true;
            for (int i = 0; i < Regions[region2].RegionLinks.Count; i++)
            {
                if (Regions[region2].RegionLinks[i] == region1)
                { addLink = false; }
            }
            if (addLink == true)
            {
                Regions[region2].RegionLinks.Add(region1);
            }
        }

        private void BuildGraph()
        {
            Graph = new List<GraphPathFindNode>();

            for (int i = 0; i < Regions.Count; i++)
            {
                Graph.Add(new MapGraphNode(new List<GraphPathFindDirection>(), i, Regions[i].X, Regions[i].Y, Regions[i].Width, Regions[i].Height));

                for (int j = 0; j < Regions[i].RegionLinks.Count; j++)
                {
                    Graph[i].Directions.Add(new GraphPathFindDirection(Regions[i].RegionLinks[j], 1));
                }
            }    
        }

        //*************
    }
}

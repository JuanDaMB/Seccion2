using System;
using System.Collections.Generic;

namespace Seccion2
{
    public static class SearchPath
    {
        private static Node _startingPoint;
        private static Node _endingPoint;

        private static Dictionary<Vector, Node> _block = new Dictionary<Vector, Node>();  // For storing all the nodes with Node.cs
        private static Vector[] _directions = { Vector.Up, Vector.Right, Vector.Down, Vector.Left }; 
        private static Queue<Node> _queue = new Queue<Node>(); // Queue for enqueueing nodes and traversing through them
        private static Node _searchingPoint; // Current node we are searching
        private static bool _isExploring = true; // If we are end then it is set to false

        private static List<Node> _path = new List<Node>(); // For storing the path traversed
        
        public static void CreateMaze(int width, int height)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Vector pos = new Vector(i,j);
                    _block.Add(pos, new Node(pos));
                }
            }

            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                _block[new Vector(r.Next(width), r.Next(height))].IsObstacle = true;
            }

            while (_startingPoint == null)
            {
                Vector pos = new Vector(r.Next(width), r.Next(height));
                if (_block[pos].IsObstacle)
                {
                    continue;
                }

                _startingPoint = _block[pos];
            }
            while (_endingPoint == null)
            {
                Vector pos = new Vector(r.Next(width), r.Next(height));
                if (_block[pos].IsObstacle)
                {
                    continue;
                }

                if (pos == _startingPoint.GetPos())
                {
                    continue;
                }

                _endingPoint = _block[pos];
            }

            
            Console.WriteLine("Showing map");
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Vector pos = new Vector(i,j);
                    if (_block[pos].IsObstacle)
                    {
                        Console.Write(" X ");
                    }
                    else if (_block[pos].GetPos() == _startingPoint.GetPos())
                    {
                        Console.Write(" O ");
                    }
                    else if (_block[pos].GetPos() == _endingPoint.GetPos())
                    {
                        Console.Write(" ! ");
                    }
                    else
                    {
                        Console.Write(" ■ ");
                    }
                }
                Console.Write("\n");
            }
            Console.WriteLine("Ended showing Map");
            
            Console.WriteLine("\nStarting point 'O' at: " + _startingPoint.GetPos().y +","+ _startingPoint.GetPos().x);
            Console.WriteLine("\nEnding point '!' at: " + _endingPoint.GetPos().y +","+ _endingPoint.GetPos().x);
        }

        public static void BFS()
        {
            _queue.Enqueue(_startingPoint);
            while (_queue.Count > 0 && _isExploring)
            {
                _searchingPoint = _queue.Dequeue();
                OnReachingEnd();
                ExploreNeighbourNodes();
            }
        }

        private static void OnReachingEnd()
        {
            if (_searchingPoint == _endingPoint)
            {
                _isExploring = false;
            }
            else
            {
                _isExploring = true;
            }
        }


        // Searching the neighbouring nodes
        private static void ExploreNeighbourNodes()
        {
            if (!_isExploring)
            {
                return;
            }

            foreach (Vector direction in _directions)
            {
                Vector neighbourPos = _searchingPoint.GetPos() + direction;

                if (_block.ContainsKey(neighbourPos)
                ) // If the explore neighbour is present in the dictionary _block, which contians all the blocks with Node.cs attached
                {
                    Node node = _block[neighbourPos];

                    if (node.IsObstacle)
                    {
                        continue;
                    }
                    
                    if (!node.IsExplored)
                    {
                        _queue.Enqueue(node); // Enqueueing the node at this position
                        node.IsExplored = true;
                        node.IsExploredFrom =
                            _searchingPoint; // Set how we reached the neighbouring node i.e the previous node; for getting the path
                    }
                }
            }
        }

        // Creating path using the isExploredFrom var of each node to get the previous node from where we got to this node
        public static void CreatePath()
        {
            SetPath(_endingPoint);
            Node previousNode = _endingPoint.IsExploredFrom;

            while (previousNode != _startingPoint)
            {
                SetPath(previousNode);
                previousNode = previousNode.IsExploredFrom;
            }

            SetPath(_startingPoint);
            _path.Reverse();
        }

        // For adding nodes to the path
        private static void SetPath(Node node)
        {
            _path.Add(node);
        }

        public static void PrintPath()
        {
            Console.WriteLine("\nShowing path");
            foreach (Node node in _path)
            {
                Console.WriteLine(node.GetPos().y + "," + node.GetPos().x);
            }
            Console.WriteLine("End of showing path");
        }

    }
}
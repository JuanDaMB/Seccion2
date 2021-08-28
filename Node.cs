namespace Seccion2
{
    public class Node
    {
        public bool IsExplored;
        public bool IsObstacle;
        public Node IsExploredFrom;
        private Vector _pos;

        public Node(Vector vector)
        {
            _pos = vector;
            IsExplored = false;
            IsObstacle = false;
        }
        
        public Vector GetPos()
        {
            return new Vector(_pos.x,_pos.y);
        }
    }
}
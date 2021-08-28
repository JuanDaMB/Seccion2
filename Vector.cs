namespace Seccion2
{
    public struct Vector
    {
        public int x;
        public int y;
        
        public static readonly Vector Up = new Vector(0,1);
        public static readonly Vector Down= new Vector(0,-1);
        public static readonly Vector Left= new Vector(-1,0);
        public static readonly Vector Right= new Vector(1,0);
        public static Vector operator +(Vector a, Vector b) => new Vector(a.x + b.x, a.y + b.y);
        public static bool operator ==(Vector lhs, Vector rhs) => lhs.x == rhs.x && lhs.y == rhs.y;
        public static bool operator !=(Vector lhs, Vector rhs) => !(lhs == rhs);
        public Vector(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
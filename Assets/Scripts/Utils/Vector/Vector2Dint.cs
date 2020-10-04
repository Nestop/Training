namespace Utils
{
    public struct Vector2Dint
    {
       public int x;
       public int y;

       public Vector2Dint(int x, int y)
       {
           this.x = x;
           this.y = y;
       }

        public override bool Equals(object obj)
        {
            var vector = (Vector2Dint)obj;
            return x == vector.x && y == vector.y;
        }

        public override int GetHashCode()
        {
            int hashcode = x.GetHashCode();
            hashcode = 1023*hashcode + y.GetHashCode();
            return hashcode;
        }
    }
}

namespace Utils
{
    public class Vector3Dint
    {
       public int x;
       public int y;
       public int z;

       public Vector3Dint(int x, int y, int z)
       {
           this.x = x;
           this.y = y;
           this.z = z;
       }

        public override bool Equals(object obj)
        {
            var vector = obj as Vector3Dint;
            return x == vector.x && y == vector.y && z == vector.z;
        }

        public override int GetHashCode()
        {
            int hashcode = x.GetHashCode();
            hashcode = 31*hashcode + y.GetHashCode();
            hashcode = 31*hashcode + z.GetHashCode();
            return hashcode;
        }
    }
}

namespace Utils
{
    public class Vector3D
    {
       public float x;
       public float y;
       public float z;

       public Vector3D(float x, float y, float z)
       {
           this.x = x;
           this.y = y;
           this.z = z;
       }

        public override bool Equals(object obj)
        {
            var vector = obj as Vector3D;
            return x == vector.x && y == vector.y && z == vector.z;
        }

        public override int GetHashCode()
        {
            int hashcode = x.GetHashCode();
            hashcode = 32*hashcode + y.GetHashCode();
            hashcode = 32*hashcode + z.GetHashCode();
            return hashcode;
        }
    }
}

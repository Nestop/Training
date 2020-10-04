namespace Utils
{
    public struct Vector3D
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
            var vector = (Vector3D)obj;
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

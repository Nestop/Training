using System;

namespace Utils.Vector
{
    public struct Vector3Dint : IEquatable<Vector3Dint>
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
            return obj is Vector3Dint vector && Equals(vector);
        }

        public bool Equals(Vector3Dint vector)
        {
            return x.Equals(vector.x) && y.Equals(vector.y) && z.Equals(vector.z);
        }

        public override int GetHashCode()
        {
            var hashcode = x.GetHashCode();
            hashcode = 1023 * hashcode + y.GetHashCode();
            hashcode = 1023 * hashcode + z.GetHashCode();
            return hashcode;
        }
    }
}
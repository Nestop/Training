using System;

namespace Utils.Vector
{
    public struct Vector3D : IEquatable<Vector3D>
    {
        private const float AccuracyForHash = 1E6f + 1;

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
            return obj is Vector3D vector && Equals(vector);
        }

        public bool Equals(Vector3D vector)
        {
            return x.Equals(vector.x) && y.Equals(vector.y) && z.Equals(vector.z);
        }

        public override int GetHashCode()
        {
            var hashcode = ((int) (x * AccuracyForHash)).GetHashCode();
            hashcode = hashcode * 1023 + ((int) (y * AccuracyForHash)).GetHashCode();
            hashcode = hashcode * 1023 + ((int) (z * AccuracyForHash)).GetHashCode();
            return hashcode;
        }
    }
}
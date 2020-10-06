using System;

namespace Utils.Vector
{
    public struct Vector2Dint : IEquatable<Vector2Dint>
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
            return obj is Vector2Dint vector && Equals(vector);
        }

        public bool Equals(Vector2Dint vector)
        {
            return x.Equals(vector.x) && y.Equals(vector.y);
        }

        public override int GetHashCode()
        {
            var hashcode = x.GetHashCode();
            hashcode = 1023 * hashcode + y.GetHashCode();
            return hashcode;
        }
        
        public static implicit operator Vector2Dint(Vector3Dint vec3)
        {
            return new Vector2Dint(vec3.x, vec3.y);
        }

        public static implicit operator Vector3Dint(Vector2Dint vec2)
        {
            return new Vector3Dint(vec2.x, vec2.y, 0);
        }
    }
}
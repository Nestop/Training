using System;

namespace Utils.Vector
{
    public struct Vector2D : IEquatable<Vector2D>
    {
        private const float AccuracyForHash = 1E6f + 1;

        public float x;
        public float y;

        public Vector2D(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(object obj)
        {
            return obj is Vector2D vector && Equals(vector);
        }

        public bool Equals(Vector2D vector)
        {
            return x.Equals(vector.x) && y.Equals(vector.y);
        }

        public override int GetHashCode()
        {
            var hashcode = ((int) (x * AccuracyForHash)).GetHashCode();
            hashcode = hashcode * 1023 + ((int) (y * AccuracyForHash)).GetHashCode();
            return hashcode;
        }
        
        public static implicit operator Vector2D(Vector3D vec3)
        {
            return new Vector2D(vec3.x, vec3.y);
        }

        public static implicit operator Vector3D(Vector2D vec2)
        {
            return new Vector3D(vec2.x, vec2.y, 0f);
        }
    }
}
namespace Utils
{
    public class Vector2D
    {
        private const float ACCURACY_FOR_HASH = 1E6f+1;

        public float x;
        public float y;

        public Vector2D(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

            public override bool Equals(object obj)
            {
                var vector = obj as Vector2D;
                return x == vector.x && y == vector.y;
            }

            public override int GetHashCode()
            {
                int hashcode = ((int)(x*ACCURACY_FOR_HASH)).GetHashCode();
                hashcode =hashcode*1023 + ((int)(y*ACCURACY_FOR_HASH)).GetHashCode();
                return hashcode;
            }
    }
}

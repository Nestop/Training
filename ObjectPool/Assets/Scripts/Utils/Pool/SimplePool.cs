using UnityEngine;

namespace Utils.Pool
{
    public class SimplePool : ObjectPool<GameObject>
    {
        public SimplePool(GameObject createObject, int poolSize = 0, bool fillAtStart = false, bool expandable = false) : base(createObject, poolSize, fillAtStart, expandable)
        {
        }

        protected override GameObject GetActiveObjectStrategy()
        {
            return _objects[Random.Range(0, ObjectCount)];
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Tests
{
    public class TestHashCode : MonoBehaviour
    {
        private void Start()
        {
            var unityVector2Collisions = Test.TestClassOnCollision((x,y)=>{ return new Vector2(x,y);});
            Debug.Log(unityVector2Collisions);

            var unityVector2IntCollisions = Test.TestClassOnCollision((x,y)=>{ return new Vector2Int(x,y);});
            Debug.Log(unityVector2IntCollisions);

            var Vector2Collisions = Test.TestClassOnCollision((x,y)=>{ return new Vector2D(x,y);});
            Debug.Log(Vector2Collisions);

            var Vector2IntCollisions = Test.TestClassOnCollision((x,y)=>{ return new Vector2Dint(x,y);});
            Debug.Log(Vector2IntCollisions);
        }
    }

    static public class Test
    {
        private const int SQRT_DICTIONARY_SIZE = 1024;
        private const int SQRT_TEST_COUNT = 1000;
            
        public delegate object ObjectCreator(int x, int y);

        static public int TestClassOnCollision(ObjectCreator CreateObject)
        {
            var collisons = 0;
            var size = SQRT_DICTIONARY_SIZE;
            var tests = SQRT_TEST_COUNT;
            var hashCodes = new Dictionary<int, object>(size*size);
            
            for(int i = 0; i < tests; i++)
            {
                for(int j = 0; j < tests; j++)
                {
                    var obj = CreateObject(i,j);
                    var hashCode = obj.GetHashCode();
                    if(hashCodes.ContainsKey(hashCode))
                    {
                        collisons++;
                    }
                    else
                    {
                        hashCodes[hashCode] = null;
                    }
                }
            }
            return collisons;
        }
    }
}

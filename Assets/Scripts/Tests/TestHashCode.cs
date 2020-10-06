using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Utils.Vector;

namespace Tests
{
    public class TestHashCode : MonoBehaviour
    {
        private void Start()
        {
            TestClassOnCollision((x, y) => new Vector2(Mathf.Sqrt(x), Mathf.Sqrt(y)), 0, 1000);
            TestClassOnCollision((x, y) => new Vector2Int(x, y), 0, 1000);
            TestClassOnCollision((x, y) => new Vector2D(Mathf.Sqrt(x), Mathf.Sqrt(y)), 0, 1000);
            TestClassOnCollision((x, y) => new Vector2Dint(x, y), 0, 1000);
        }

        private delegate object ObjectCreator(int x, int y);

        private void TestClassOnCollision(ObjectCreator createObject, int from, int to)
        {
            var collisions = 0;
            var checksCountInLoop = to - from;
            var allChecks = checksCountInLoop * checksCountInLoop;
            var dictionarySize = (int) Mathf.Pow(2, Mathf.Ceil(Mathf.Log(allChecks, 2f)));
            var hashCodes = new Dictionary<int, object>(dictionarySize);

            for (var i = from; i < to; i++)
            {
                for (var j = from; j < to; j++)
                {
                    var obj = createObject(i, j);
                    var hashCode = obj.GetHashCode();
                    if (hashCodes.ContainsKey(hashCode))
                    {
                        collisions++;
                    }
                    else
                    {
                        hashCodes[hashCode] = null;
                    }
                }
            }

            var collisionsPercent = collisions * 100f / allChecks;
            Debug.Log(
                $"{collisionsPercent.ToString(CultureInfo.CurrentCulture)}% collisions in " +
                $"{createObject(0,0).GetType().FullName} ({collisions} matches out of {allChecks} checks)");
        }
    }
}
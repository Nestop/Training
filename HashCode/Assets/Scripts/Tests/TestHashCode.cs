using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using UnityEngine;
using Utils.Vector;
using Debug = UnityEngine.Debug;

namespace Tests
{
    public class TestHashCode : MonoBehaviour
    {
        private void Start()
        {
            TestClassOnTime((x, y) => new Vector2(Mathf.Sqrt(x), Mathf.Sqrt(y)), 0, 1000);
            TestClassOnTime((x, y) => new Vector2Int(x, y), 0, 1000);
            TestClassOnTime((x, y) => new Vector2D(Mathf.Sqrt(x), Mathf.Sqrt(y)), 0, 1000);
            TestClassOnTime((x, y) => new Vector2Dint(x, y), 0, 1000);
        }

        private delegate object ObjectCreator(int x, int y);

        private void TestClassOnCollision(ObjectCreator createObject, int from, int to)
        {
            var collisions = 0;
            var checksCountInLoop = to - from;
            var allChecks = checksCountInLoop * checksCountInLoop;
            var hashCodes = CreateDictionary<int, object>(allChecks);

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
        
        private void TestClassOnTime(ObjectCreator createObject, int from, int to)
        {
            var checksCountInLoop = to - from;
            var allChecks = checksCountInLoop * checksCountInLoop;
            var dictionary = CreateDictionary<object, object>(allChecks);

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            
            for (var i = from; i < to; i++)
            {
                for (var j = from; j < to; j++)
                {
                    var obj = createObject(i, j);
                    dictionary[obj] = obj;
                }
            }
            
            for (var i = from; i < to; i++)
            {
                for (var j = from; j < to; j++)
                {
                    var obj = createObject(i, j);
                    object receiver;
                    dictionary.TryGetValue(obj,out receiver);
                }
            }

            stopWatch.Stop();
            Debug.Log(
                $"{stopWatch.Elapsed.TotalSeconds.ToString(CultureInfo.CurrentCulture)} seconds for " +
                $"{createObject(0,0).GetType().FullName} ({allChecks} elements)");
        }

        private Dictionary<T1,T2> CreateDictionary<T1,T2>(int checksCount)
        {
            var dictionarySize = (int) Mathf.Pow(2, Mathf.Ceil(Mathf.Log(checksCount, 2f))); 
            return new Dictionary<T1,T2>(dictionarySize);
        }
    }
}
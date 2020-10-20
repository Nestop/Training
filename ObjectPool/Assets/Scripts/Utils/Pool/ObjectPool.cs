using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utils.Pool
{
    public class ObjectPool<T> where T : Object
    {
        protected List<T> _objects;
        private Stack<T> _inactiveObjects;
        private bool _expandable;
        private T _object;

        protected int ObjectCount => _objects.Count;
        
        public ObjectPool(T obj, int poolSize = 0, bool fillAtStart = false, bool expandable = false)
        {
            _object = obj;
            _objects = new List<T>(poolSize);
            _inactiveObjects = new Stack<T>(poolSize);
            
            if (fillAtStart && poolSize > 0)
            {
                for (int i = 0; i < poolSize; i++)
                {
                    _objects.Add(Object.Instantiate(_object));
                    _inactiveObjects.Push(_objects[i]);
                }
            }

            _expandable = expandable;
        }
        
        public T GetObject()
        {
            if (_inactiveObjects.Count > 0)
            {
                return _inactiveObjects.Pop();
            }
            else
            {
                if (_expandable)
                {
                    var obj = Object.Instantiate(_object);
                    _objects.Add(obj);
                    return obj;  
                }
                else
                {
                    return GetActiveObjectStrategy();  
                }
            }
        }

        protected virtual T GetActiveObjectStrategy()
        {
            return _objects[Random.Range(0, ObjectCount)];
        }

        public void DeactivateObject(T obj)
        {
            _inactiveObjects.Push(obj);
        }
    }
}
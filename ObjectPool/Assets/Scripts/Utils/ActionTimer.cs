using System;
using System.Collections;
using UnityEngine;

namespace Utils
{
    public class ActionTimer : MonoBehaviour
    {
        private static ActionTimer _instance;
        public static ActionTimer Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject instance = new GameObject("ActionTimer");
                    _instance = instance.AddComponent<ActionTimer>();
                    return _instance;
                }
                else
                {
                    return _instance;
                }
            }
        }

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else
                if (_instance != this) Destroy(gameObject);
        }

        public void SingleCall(Action action, float seconds)
        {
            StartCoroutine(SingleCallCoroutine(action, seconds));
        }
        
        private static IEnumerator SingleCallCoroutine(Action action, float seconds)
        {
            yield return new WaitForSeconds(seconds);   
            action?.Invoke();
        }
        
        public void InfinityCall(Action action, float secondsLoopTime)
        {
            StartCoroutine(InfinityCallCoroutine(action, secondsLoopTime));
        }
        
        private static IEnumerator InfinityCallCoroutine(Action action, float secondsLoopTime)
        {
            while (true)
            {
                action?.Invoke();
                yield return new WaitForSeconds(secondsLoopTime);
            }
        }
    }
}
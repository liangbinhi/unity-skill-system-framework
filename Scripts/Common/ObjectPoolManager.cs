using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public static ObjectPoolManager Instance { get; private set; }

        private readonly Dictionary<string, Queue<GameObject>> poolDict = new();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public GameObject Get(string key, GameObject prefab)
        {
            if (poolDict.ContainsKey(key) && poolDict[key].Count > 0)
            {
                GameObject obj = poolDict[key].Dequeue();
                obj.SetActive(true);
                return obj;
            }

            return Instantiate(prefab);
        }

        public void Release(string key, GameObject obj)
        {
            if (!poolDict.ContainsKey(key))
            {
                poolDict[key] = new Queue<GameObject>();
            }

            obj.SetActive(false);
            poolDict[key].Enqueue(obj);
        }
    }
}
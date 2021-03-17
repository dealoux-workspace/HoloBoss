using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeaLoux.CoreSystems.Patterns
{
    public abstract class GenericObjectPool<T> : MonoBehaviour where T : Component
    {
        [SerializeField]
        private T prefab;

        public static GenericObjectPool<T> Instance { get; private set; }
        private Queue<T> objects = new Queue<T>();

        private void Awake()
        {
            Instance = this;
        }

        public T Get()
        {
            if (objects.Count == 0)
                AddObject(1);

            var obj = objects.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }

        public void ReturnToPool(T objToReturn)
        {
            objToReturn.gameObject.SetActive(false);
            objects.Enqueue(objToReturn);
        }

        private void AddObject(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var newObj = Instantiate(prefab);
                newObj.transform.SetParent(transform);
                newObj.gameObject.SetActive(false);
                objects.Enqueue(newObj);
            }
        }
    }
}
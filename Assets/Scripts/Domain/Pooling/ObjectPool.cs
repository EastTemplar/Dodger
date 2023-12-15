using System.Collections.Generic;
using UnityEngine;

namespace Dodger.Domain.Pooling
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        public T Prefab { get; }
        public Transform Parent { get; }
        private Stack<T> _stack;
        private int _initialCount;

        public ObjectPool(T prefab, Transform parent, int initalCount = 0)
        {
            Prefab = prefab;
            Parent = parent;
            _initialCount = initalCount;

            _stack = new Stack<T>();

            for (int i = 0; i < _initialCount; i++)
            {
                CreateObject();
            }
        }

        private void CreateObject()
        {
            var newItem = Object.Instantiate(Prefab, Parent);
            Pool(newItem);
        }

        public void Pool(T item)
        {
            _stack.Push(item);
            item.gameObject.SetActive(false);
        }

        private T UnpoolStacked()
        {
            var unpooledItem = _stack.Pop();
            unpooledItem.gameObject.SetActive(true);
            return unpooledItem;
        }

        public T Unpool()
        {
            if (_stack.Count > 0)
            {
                return UnpoolStacked();
            }

            CreateObject();
            return UnpoolStacked();
        }
    }
}

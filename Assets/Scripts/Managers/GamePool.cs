using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public abstract class AbstractGamePool
    {
        public abstract void ReleasePool();
    }

    public class GamePool<T> : AbstractGamePool where T : class
    {
        private readonly HashSet<T> _objectPool = new HashSet<T>();
        private readonly GameObject _poolContainer = new GameObject($"{typeof(T).Name.Replace("Base", "")}Pool");

        public GameObject PoolContainer => _poolContainer;

        public HashSet<T> Get()
        {
            return _objectPool;
        }

        public bool Remove(T poolObject)
        {
            if (_objectPool.Contains(poolObject))
            {
                _objectPool.Remove(poolObject);
                return true;
            }

            return false;
        }

        public bool Add(T poolElement)
        {
            return _objectPool.Add(poolElement);
        }

        public override void ReleasePool()
        {
            _objectPool.Clear();
            Object.Destroy(_poolContainer);
        }
    }
}
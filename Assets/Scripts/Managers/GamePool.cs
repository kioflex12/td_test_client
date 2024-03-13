using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public abstract class AbstractGamePool {
        public abstract void ReleasePool();
    }
    
    public class GamePool<T> : AbstractGamePool where T : class {
        private readonly HashSet<T> m_objectPool = new HashSet<T>();
        public readonly GameObject m_poolContainer = new GameObject($"{typeof(T).Name.Replace("Base", "")}Pool");

        public HashSet<T> Get() {
            return m_objectPool;
        }

        public bool Remove(T poolObject) {
            if (m_objectPool.Contains(poolObject)) {
                m_objectPool.Remove(poolObject);
                return true;
            }
            return false;
        }

        public bool Add(T poolElement) {
            return m_objectPool.Add(poolElement);
        }

        public override void ReleasePool() {
            m_objectPool.Clear();
            Object.Destroy(m_poolContainer);
        }
    }
}
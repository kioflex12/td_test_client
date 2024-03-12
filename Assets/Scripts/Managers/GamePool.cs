using System.Collections.Generic;

namespace Managers
{
    public abstract class AbstractGamePool {
        public abstract void ReleasePool();
    }
    
    public class GamePool<T> : AbstractGamePool where T : class {
        private readonly HashSet<T> m_objectPool = new HashSet<T>();

        public HashSet<T> Get() {
            return m_objectPool;
        }

        public bool TryRemove(T poolObject) {
            if (m_objectPool.Contains(poolObject)) {
                m_objectPool.Remove(poolObject);
                return true;
            }
            return false;
        }

        public bool TryAdd(T poolElement) {
            return m_objectPool.Add(poolElement);
        }

        public override void ReleasePool() {
            m_objectPool.Clear();
        }
    }
}
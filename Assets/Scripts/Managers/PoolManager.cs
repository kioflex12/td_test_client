using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class PoolManager { 

        private static readonly List<AbstractGamePool> m_gamePools = new List<AbstractGamePool>();

        public static HashSet<T> GetOrCreatePool<T>() where T : class {
            GamePool<T> pool = null;
            
            foreach (var abstractGamePool in m_gamePools) {
                if (abstractGamePool is GamePool<T> gamePool) {
                    pool = gamePool;
                    break;
                }
            }

            if (pool == null) {
                pool = new GamePool<T>();
                m_gamePools.Add(pool);
            }
            
            return pool.Get();
        }

        public static void ReleasePools()
        {
            m_gamePools.ForEach(pool => pool.ReleasePool());
        }
    }
}
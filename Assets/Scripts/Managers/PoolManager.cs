using System.Collections.Generic;

namespace Managers
{
    public class PoolManager
    {
        private static readonly List<AbstractGamePool> _gamePools = new List<AbstractGamePool>();

        public static GamePool<T> GetOrCreatePool<T>() where T : class
        {
            GamePool<T> pool = null;

            foreach (var abstractGamePool in _gamePools)
            {
                if (abstractGamePool is GamePool<T> gamePool)
                {
                    pool = gamePool;
                    break;
                }
            }

            if (pool == null)
            {
                pool = new GamePool<T>();
                _gamePools.Add(pool);
            }

            return pool;
        }

        public static void ReleasePools()
        {
            _gamePools.ForEach(pool => pool.ReleasePool());
        }
    }
}
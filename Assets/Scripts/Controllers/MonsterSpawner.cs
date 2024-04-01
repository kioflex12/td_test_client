using System;
using System.Collections;
using System.Linq;
using Managers;
using Models;
using Monsters;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Controllers
{
    public class MonsterSpawner : IDisposable
    {
        private readonly GameSettings _gameSettings;
        private readonly Transform _spawnPosition;
        private readonly GamePool<Monster> _pool;
        private readonly Coroutine _spawnRoutine;

        public MonsterSpawner(Transform spawnPosition)
        {
            _gameSettings = ModelsProvider.GameSettings;
            _spawnPosition = spawnPosition;
            _spawnRoutine = CoroutineRunner.StartCoroutine(SpawnCoroutine());
            _pool = PoolManager.GetOrCreatePool<Monster>();
        }

        private IEnumerator SpawnCoroutine()
        {
            var wfs = new WaitForSeconds(_gameSettings.SpawnSettings.interval);
            while (true)
            {
                yield return wfs;

                if (_pool == null)
                {
                    throw new Exception("Can`t get pool monster");
                }

                var monster = _pool.Get().FirstOrDefault(poolElement => poolElement.IsAlive == false);

                if (monster == null)
                {
                    monster = CreateMonster();
                    _pool.Add(monster);
                }

                monster.Init(_gameSettings.MonsterBalanceSettings);
                monster.transform.position = _spawnPosition.position;
            }
        }

        private Monster CreateMonster()
        {
            var monsterObject = Resources.Load<Monster>("Monsters/Monster");
            var monster = Object.Instantiate(monsterObject, _pool.PoolContainer.transform);
            return monster;
        }

        public void Dispose()
        {
            if (_spawnRoutine != null)
            {
                CoroutineRunner.Stop(_spawnRoutine);
            }
        }
    }
}
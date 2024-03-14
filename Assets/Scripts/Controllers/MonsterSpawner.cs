using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using Models;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Controllers {
	public class MonsterSpawner : IDisposable {
		private readonly GameSettings m_gameSettings;
		private readonly Transform m_spawnPosition;
		private readonly GamePool<Monster> m_pool;
		private readonly Coroutine m_spawnCoroutine;

		public MonsterSpawner(Transform spawnPosition) {
			m_gameSettings = ModelsProvider.GameSettings;
			m_spawnPosition = spawnPosition;
			m_spawnCoroutine = CoroutineRunner.StartCoroutine(SpawnCoroutine());
			m_pool = PoolManager.GetOrCreatePool<Monster>();
		}

		private IEnumerator SpawnCoroutine () {
			var wfs = new WaitForSeconds(m_gameSettings.m_spawnSettings.m_interval);
			while (true)
			{
				yield return wfs;
				
				if (m_pool == null) {
					throw new Exception("Can`t get pool monster");
				}

				var monster = m_pool.Get().FirstOrDefault(poolElement => poolElement.IsAlive == false);

				if (monster == null) {
					monster = CreateMonster();
					m_pool.Add(monster);
				}
				
				monster.Init(m_gameSettings.m_monsterSettings);
				monster.transform.position = m_spawnPosition.position;
			}
		}

		private Monster CreateMonster() {
			var monsterObject = Resources.Load<Monster>("Monsters/Monster");
			var monster = Object.Instantiate(monsterObject, m_pool.m_poolContainer.transform);
			return monster;
		}

		public void Dispose() {
			if (m_spawnCoroutine != null) {
				CoroutineRunner.Stop(m_spawnCoroutine);
			}	
		}
	}
}

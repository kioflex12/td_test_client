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
		private readonly HashSet<Monster> m_pool;
		private readonly GameObject m_parentGameObject;
		private readonly Coroutine m_spawnCoroutine;

		public MonsterSpawner(GameSettings gameSettings, Transform spawnPosition) {
			m_gameSettings = gameSettings;
			m_spawnPosition = spawnPosition;
			m_spawnCoroutine = CoroutineRunner.StartCoroutine(SpawnCoroutine());
			m_pool = PoolManager.GetOrCreatePool<Monster>();
			m_parentGameObject = new GameObject("MonsterPool");
		}

		private IEnumerator SpawnCoroutine () {
			while (true)
			{
				yield return new WaitForSeconds(m_gameSettings.m_spawnSettings.m_interval);
				
				if (m_pool == null) {
					throw new Exception("Can`t get pool monster");
				}

				var monster = m_pool.FirstOrDefault(poolElement => poolElement.IsAlive == false);

				if (monster == null) {
					monster = CreateMonster();
				}
				
				monster.Init(m_gameSettings.m_monsterSettings);
				monster.transform.position = m_spawnPosition.position;
			}
		}

		private Monster CreateMonster() {
			var monsterObject = Resources.Load<Monster>("Monster/Monster");
			var monster = Object.Instantiate(monsterObject, m_parentGameObject.transform);
			return monster;
		}

		public void Dispose() {
			if (m_spawnCoroutine != null) {
				CoroutineRunner.Stop(m_spawnCoroutine);
			}	
			Object.Destroy(m_parentGameObject);
		}
	}
}

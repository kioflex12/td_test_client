using System;
using System.Collections;
using Managers;
using Models;
using UnityEngine;

namespace Controllers {
	public class Spawner : IDisposable {
		private readonly GameSettings m_gameSettings;
		private readonly Transform m_spawnPosition;

		private Coroutine m_spawnCoroutine;
		
		public Spawner(GameSettings gameSettings, Transform spawnPosition) {
			m_gameSettings = gameSettings;
			m_spawnPosition = spawnPosition;
			m_spawnCoroutine = CoroutineRunner.StartCoroutine(SpawnCoroutine());
		}

		private IEnumerator SpawnCoroutine () {
			while (true)
			{
				yield return new WaitForSeconds(m_gameSettings.m_spawnSettings.m_interval);
				
				var monster = MonsterPoolManager.Instance.GetOrCreateFreeMonster();
				if (monster == null) {
					throw new Exception("Can`t get free monster from pool");
				}
				monster.Init(m_gameSettings.m_monsterSettings);
				monster.transform.position = m_spawnPosition.position;
			}
		}

		public void Dispose() {
			if (m_spawnCoroutine != null) {
				CoroutineRunner.Stop(m_spawnCoroutine);
			}		
		}
	}
}

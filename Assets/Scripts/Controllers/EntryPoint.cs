using Managers;
using Models;
using UnityEngine;

namespace Controllers {
    public sealed class EntryPoint : MonoBehaviour {
        public MonsterPathManager m_pathManager;
        
        private MonsterSpawner m_monsterSpawner;
        private MonsterMover m_monsterMover;
        
        private void Awake() {
            InitializeSystems();
        }

        private void InitializeSystems() {
            m_monsterSpawner = new MonsterSpawner(m_pathManager.m_spawnPoint);
            m_monsterMover = new MonsterMover(m_pathManager.m_moveTargetPoint);
        }

        private void OnDestroy() {
            m_monsterSpawner.Dispose();
            m_monsterMover.Dispose();
            PoolManager.ReleasePools();
        }
    }
}
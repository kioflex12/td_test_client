using Managers;
using Models;
using UnityEngine;

namespace Controllers {
    public sealed class EntryPoint : MonoBehaviour {
        public GameSettings m_gameSettings;
        public MonsterPathManager m_pathManager;
        
        private MonsterSpawner _mMonsterSpawner;
        private MonsterMover m_monsterMover;
        
        private void Awake() {
            InitializeSystems();
        }

        private void InitializeSystems() {
            _mMonsterSpawner = new MonsterSpawner(m_gameSettings, m_pathManager.m_spawnPoint);
            m_monsterMover = new MonsterMover(m_pathManager.m_moveTargetPoint);
        }

        private void OnDestroy() {
            _mMonsterSpawner.Dispose();
            m_monsterMover.Dispose();
            PoolManager.ReleasePools();
        }
    }
}
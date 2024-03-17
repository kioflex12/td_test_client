using Managers;
using UnityEngine;

namespace Controllers {
    public sealed class EntryPoint : MonoBehaviour {
        public MonsterPathManager m_pathManager;
        
        private MonsterSpawner m_monsterSpawner;
        private MonsterMover _mObjectMover;
        
        private void Awake() {
            InitializeSystems();
        }

        private void InitializeSystems() {
            m_monsterSpawner = new MonsterSpawner(m_pathManager.m_spawnPoint);
            _mObjectMover = new MonsterMover(m_pathManager.m_moveTargetPoint);
        }

        private void OnDestroy() {
            m_monsterSpawner.Dispose();
            _mObjectMover.Dispose();
            PoolManager.ReleasePools();
        }
    }
}
using Managers;
using Models;
using UnityEngine;

namespace Controllers {
    public sealed class EntryPoint : MonoBehaviour {
        public GameSettings m_gameSettings;
        public MonsterPathManager m_pathManager;
        
        private Spawner m_spawner;
        private MonsterMover m_monsterMover;
        
        private void Awake() {
            InitializeSystems();
        }

        private void InitializeSystems() {
            m_spawner = new Spawner(m_gameSettings, m_pathManager.m_spawnPoint);
            m_monsterMover = new MonsterMover(m_gameSettings, m_pathManager.m_moveTargetPoint);
        }

        private void OnDestroy() {
            m_spawner.Dispose();
            m_monsterMover.Dispose();
        }
    }
}
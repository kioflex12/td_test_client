using Managers;
using UnityEngine;

namespace Controllers
{
    public sealed class EntryPoint : MonoBehaviour
    {
        [SerializeField] private MonsterPathManager pathManager;

        private MonsterSpawner _monsterSpawner;
        private MonsterMover _objectMover;

        private void Awake()
        {
            InitializeSystems();
        }

        private void InitializeSystems()
        {
            _monsterSpawner = new MonsterSpawner(pathManager.SpawnPoint);
            _objectMover = new MonsterMover(pathManager.MoveTargetPoint);
        }

        private void OnDestroy()
        {
            _monsterSpawner.Dispose();
            _objectMover.Dispose();
            PoolManager.ReleasePools();
        }
    }
}
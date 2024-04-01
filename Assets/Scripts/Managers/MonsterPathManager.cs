using UnityEngine;

namespace Managers
{
    public class MonsterPathManager : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform moveTargetPoint;
        
        public Transform SpawnPoint => spawnPoint;
        public Transform MoveTargetPoint => moveTargetPoint;
    }
}
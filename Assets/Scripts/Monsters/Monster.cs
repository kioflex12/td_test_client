using Managers;
using UnityEngine;
using static Models.GameSettings;

namespace Monsters
{
    public class Monster : MonoBehaviour, IDamageable, IMovable
    {
        public bool IsAlive { get; private set; }
        public Transform Transform => transform;
        public Transform MoveTarget { get; private set; }
        public float MoveSpeed { get; private set; }

        private MonsterSettings _monsterSettings;
        private GamePool<Monster> _monsterPool;

        private float _currentHp;

        private void Awake()
        {
            _monsterPool = PoolManager.GetOrCreatePool<Monster>();
        }

        private void OnDestroy()
        {
            _monsterPool.Remove(this);
        }

        public void Init(MonsterSettings monsterSettings)
        {
            _monsterSettings = monsterSettings;
            IsAlive = true;
            gameObject.SetActive(true);
            InitializeParameters();
        }

        private void InitializeParameters()
        {
            _currentHp = _monsterSettings.maxHP;
            MoveSpeed = _monsterSettings.speed;
        }

        public void ApplyDamage(float damage)
        {
            _currentHp -= damage;
            if (_currentHp <= 0)
            {
                Kill();
            }
        }

        public void Kill()
        {
            IsAlive = false;
            gameObject.SetActive(false);
        }

        public void Move(Transform target)
        {
            MoveTarget = target;
            transform.position = Vector3.MoveTowards(transform.position, target.position, MoveSpeed);
        }

        public Vector3 GetPredictPosition(float predictTime)
        {
            var fixedUpdateCalls = predictTime / Time.fixedDeltaTime;
            var totalMovement = MoveSpeed * fixedUpdateCalls;
            var direction = (MoveTarget.position - Transform.position).normalized;
            var predictPosition = Transform.position + direction * totalMovement;
            return predictPosition;
        }
    }
}
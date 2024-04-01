using Managers;
using Models;
using Monsters;
using UnityEngine;
using static Models.GameSettings;

namespace Projectiles
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public abstract class BaseProjectile : MonoBehaviour
    {
        public Rigidbody m_rigidbody;
        public Rigidbody Rigidbody => m_rigidbody;

        public bool IsActive { get; protected set; }

        protected IDamageable damageableTarget;
        protected IMovable movableTarget;

        public IDamageable DamageableTarget => damageableTarget;
        public IMovable MovableTarget => movableTarget;

        protected ProjectileSettings projectileSettings;
        protected GamePool<BaseProjectile> projectilePool;
        protected Vector3 projectileTargetPosition;

        public abstract ProjectileType ProjectileType { get; }

        public virtual void Initialize(IDamageable target, Transform shootPoint)
        {
            gameObject.transform.position = shootPoint.transform.position;
            damageableTarget = target;
            movableTarget = target as IMovable;
        }

        private void Awake()
        {
            projectileSettings = ModelsProvider.GameSettings.GetProjectileSettings(ProjectileType);
            projectilePool = PoolManager.GetOrCreatePool<BaseProjectile>();
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            IsActive = true;
        }

        private void OnDisable()
        {
            IsActive = false;
        }

        private void OnDestroy()
        {
            projectilePool.Remove(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.ApplyDamage(projectileSettings.damage);
            }
            KillObject();
        }

        private void KillObject()
        {
            gameObject.SetActive(false);
        }
    }
}
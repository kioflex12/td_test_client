using System;
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

        protected IDamageable m_damageableTarget;
        protected IMovable m_movableTarget;

        public IDamageable DamageableTarget => m_damageableTarget;

        public IMovable MovableTarget => m_movableTarget;

        protected ProjectileSettings m_projectileSettings;
        protected GamePool<BaseProjectile> m_projectilePool;
        protected Vector3 m_projectileTargetPosition;

        public abstract ProjectileType ProjectileType { get; }

        public virtual void Init(IDamageable target, Transform shootPoint) {
            gameObject.transform.position = shootPoint.transform.position;
            m_damageableTarget = target;
            m_movableTarget = target as IMovable;
            gameObject.SetActive(true);
        }

        private void Awake() {
            m_projectileSettings = ModelsProvider.GameSettings.GetProjectileSettings(ProjectileType);
            m_projectilePool = PoolManager.GetOrCreatePool<BaseProjectile>();
        }

        private void OnEnable() {
            IsActive = true;
        }

        private void OnDisable() {
            IsActive = false;
        }

        private void OnDestroy() {
            m_projectilePool.Remove(this);
        }

        void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.ApplyDamage(m_projectileSettings.m_damage);
            }
            KillObject();
        }

        private void KillObject() {
            gameObject.SetActive(false);
        }
    }
}
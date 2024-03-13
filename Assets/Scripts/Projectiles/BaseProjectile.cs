using System;
using Models;
using Monsters;
using UnityEngine;

namespace Projectiles
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public abstract class BaseProjectile : MonoBehaviour
    {
        public bool IsActive { get; protected set; }

        public abstract ProjectileType ProjectileType { get; }
        public abstract void Init(IDamageable target, Transform shootPoint);

        protected GameSettings.ProjectileSettings mProjectileSettings;

        private void Awake() {
            mProjectileSettings = ModelsProvider.GameSettings.GetProjectileSettings(ProjectileType);
        }

        private void OnEnable()
        {
            IsActive = true;
        }

        private void OnDisable()
        {
            IsActive = false;
        }

        void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.ApplyDamage(mProjectileSettings.m_damage);
            }
            DestroyObject();
        }

        private void DestroyObject()
        {
            gameObject.SetActive(false);
        }
    }
}
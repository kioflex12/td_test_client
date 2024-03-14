using System.Collections.Generic;
using System.Linq;
using Managers;
using Models;
using Monsters;
using Projectiles;
using Towers;
using UnityEngine;

namespace Weapons
{
    public abstract class BaseTowerWeapon: MonoBehaviour, IWeapon
    {
        public BaseProjectile m_weaponProjectile;
        public Transform m_shootPoint;
        
        protected GameSettings.BaseTowerWeaponSettings m_weaponSettings; 
        protected GamePool<BaseProjectile> m_projectilePool;
        
        public IDamageable m_shootTarget;
        public IMovable m_movableTarget;
        
        protected float m_lastShotTime = -0.5f;
        public float SqrShootDistance => m_weaponSettings.m_range * m_weaponSettings.m_range;

        protected abstract WeaponType WeaponType { get; }
        public abstract void Shoot();

        public virtual void Init() {
            m_projectilePool = PoolManager.GetOrCreatePool<BaseProjectile>();
            m_weaponSettings = ModelsProvider.GameSettings.GetWeaponTowerSettings(WeaponType);
        }

        public bool ShootAvailable() {
            return !(m_lastShotTime + m_weaponSettings.m_shootInterval > Time.time);
        }

        private BaseProjectile CreateProjectile() {
            var projectile = Instantiate(m_weaponProjectile, m_projectilePool.m_poolContainer.transform);
            projectile.transform.position = m_shootPoint.position;
            projectile.gameObject.SetActive(false);
            m_projectilePool.Add(projectile);
            return projectile;
        }

        public void SetTarget(IDamageable target) {
            m_shootTarget = target;
            m_movableTarget = target as IMovable;
        }

        protected BaseProjectile GetOrCreateProjectile() {
            var projectile = m_projectilePool.Get().FirstOrDefault(p => p.IsActive == false && p.ProjectileType == m_weaponProjectile.ProjectileType);
            if (projectile == null) {
                projectile = CreateProjectile();
            }

            return projectile;
        }
    }
}
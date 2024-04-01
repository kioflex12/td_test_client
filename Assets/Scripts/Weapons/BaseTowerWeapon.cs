using System.Linq;
using Managers;
using Models;
using Monsters;
using Projectiles;
using Towers;
using UnityEngine;
using static Models.GameSettings;

namespace Weapons
{
    public abstract class BaseTowerWeapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private BaseProjectile weaponProjectile;
        [SerializeField] private Transform shootPoint;
        protected Transform ShootPoint => shootPoint;

        private BaseTowerWeaponSettings _weaponSettings;
        public BaseTowerWeaponSettings WeaponSettings => _weaponSettings;

        private GamePool<BaseProjectile> _projectilePool;
        public GamePool<BaseProjectile> ProjectilePool => _projectilePool;

        private IDamageable _shootTarget;
        public IDamageable ShootTarget => _shootTarget;

        private IMovable _movableTarget;
        public IMovable MovableTarget => _movableTarget;

        protected float lastShotTime;
        public float SqrShootDistance => _weaponSettings.range * _weaponSettings.range;

        protected abstract WeaponType WeaponType { get; }

        public abstract void Shoot();

        public virtual void Init()
        {
            _projectilePool = PoolManager.GetOrCreatePool<BaseProjectile>();
            _weaponSettings = ModelsProvider.GameSettings.GetWeaponTowerSettings(WeaponType);
            lastShotTime = -_weaponSettings.shootInterval;
        }

        public bool ShootAvailable()
        {
            return lastShotTime + _weaponSettings.shootInterval > Time.time == false || ShootTarget == null;
        }

        private BaseProjectile CreateProjectile()
        {
            var projectile = Instantiate(weaponProjectile, _projectilePool.PoolContainer.transform);
            projectile.transform.position = shootPoint.position;
            projectile.gameObject.SetActive(false);
            _projectilePool.Add(projectile);
            return projectile;
        }

        public void SetTarget(IDamageable target)
        {
            _shootTarget = target;
            _movableTarget = target as IMovable;
        }

        protected BaseProjectile GetOrCreateProjectile()
        {
            var projectile = _projectilePool.Get().FirstOrDefault(p => p.IsActive == false && p.ProjectileType == weaponProjectile.ProjectileType);
            if (projectile == null)
            {
                projectile = CreateProjectile();
            }
            return projectile;
        }
    }
}
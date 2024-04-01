using Models;
using UnityEngine;

namespace Weapons
{
    public class SimpleTowerTowerWeapon : BaseTowerWeapon
    {
        protected override WeaponType WeaponType => WeaponType.SimpleWeapon;

        public override void Shoot()
        {
            if (shootTarget.IsAlive == false) return;
            
            var projectile = GetOrCreateProjectile();
            projectile.Initialize(shootTarget, ShootPoint);
            lastShotTime = Time.time;
        }
    }
}
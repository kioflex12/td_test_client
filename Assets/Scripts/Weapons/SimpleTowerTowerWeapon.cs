using Models;
using UnityEngine;

namespace Weapons {
    public class SimpleTowerTowerWeapon : BaseTowerWeapon {
        protected override WeaponType WeaponType => WeaponType.SimpleWeapon;
        
        public override void Shoot() {
            if (m_shootTarget == null) {
                return;
            }
            var projectile = GetOrCreateProjectile();
            projectile.Init(m_shootTarget, m_shootPoint);
            m_lastShotTime = Time.time;
        }
    }
}
using Models;
using UnityEngine;
using static Models.GameSettings;

namespace Weapons {
    public class SimpleTowerWeapon : BaseWeapon {
        protected override WeaponType WeaponType => WeaponType.SimpleWeapon;

        private SimpleTowerWeaponSettings m_simpleTowerWeaponSettings;
        
        public override void Shoot() {
            if (m_shootTarget == null) {
                return;
            }
            var projectile = GetShootProjectile();
            projectile.Init(m_shootTarget, m_shootPoint);
            m_lastShotTime = Time.time;
        }
    }
}
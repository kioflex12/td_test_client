using Models;
using Monsters;
using UnityEngine;

namespace Projectiles
{
    public abstract class BaseProjectile : MonoBehaviour
    {
        protected abstract ProjectileType ProjectileType { get; }
        public abstract void Init(IDamageable target);

        protected GameSettings.ProjectileSetting m_projectileSetting;

        private void Awake() {
            m_projectileSetting = ModelsProvider.GameSettings.GetProjectileSettings(ProjectileType);
        }
    }
}
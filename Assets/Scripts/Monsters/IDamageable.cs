using UnityEngine;

namespace Monsters
{
    public interface IDamageable {
        Transform Transform { get; }
        bool IsAlive { get; }
        void ApplyDamage(float damage);
        void Kill();
    }
}
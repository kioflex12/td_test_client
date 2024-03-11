using Monsters;
using UnityEngine;

namespace Projectiles
{
    public abstract class BaseProjectile : MonoBehaviour
    {
        public abstract void Init(IMovable target);
    }
}
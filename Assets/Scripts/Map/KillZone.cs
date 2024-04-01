using Monsters;
using UnityEngine;

namespace Map
{
    [RequireComponent(typeof(Collider))]
    public class KillZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider targetObject)
        {
            if (targetObject.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.Kill();
            }
        }
    }
}
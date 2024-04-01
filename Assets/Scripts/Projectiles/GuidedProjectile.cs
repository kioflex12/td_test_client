using System.Collections;
using Models;
using Monsters;
using UnityEngine;

namespace Projectiles
{
    public class GuidedProjectile : BaseProjectile
    {
        public override ProjectileType ProjectileType => ProjectileType.GuidedProjectile;

        private Coroutine _moveCoroutine;

        private IEnumerator MoveToTarget()
        {
            while (true)
            {
                yield return new WaitForFixedUpdate();
                
                if (IsActive == false) break;
                
                if (damageableTarget != null)
                {
                    projectileTargetPosition = damageableTarget.Transform.position;

                    if (damageableTarget?.IsAlive == false)
                    {
                        projectileTargetPosition = damageableTarget.Transform.position;
                        damageableTarget = null;
                    }
                }

                var translation = projectileTargetPosition - transform.position;
                if (translation.magnitude > projectileSettings.speed)
                {
                    translation = translation.normalized * projectileSettings.speed;
                }
                transform.Translate(translation);
            }
        }

        public override void Initialize(IDamageable target, Transform shootPoint)
        {
            base.Initialize(target, shootPoint);
            Activate();
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
            }
            _moveCoroutine = StartCoroutine(MoveToTarget());
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}
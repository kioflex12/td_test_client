using System.Collections;
using Models;
using Monsters;
using UnityEngine;

namespace Projectiles {
	public class GuidedProjectile : BaseProjectile {
		public override ProjectileType ProjectileType => ProjectileType.GuidedProjectile;

		private Coroutine m_moveCoroutine;
		
		private IEnumerator MoveToTarget () {
			while (true) {
				yield return new WaitForFixedUpdate();
				if (IsActive == false) {
					break;
				}
				if (m_damageableTarget != null) {
					m_projectileTargetPosition = m_damageableTarget.Transform.position;

					if (m_damageableTarget?.IsAlive == false) {
						m_projectileTargetPosition = m_damageableTarget.Transform.position;
						m_damageableTarget = null;
					}
				}

				var translation = m_projectileTargetPosition - transform.position;
				if (translation.magnitude > m_projectileSettings.m_speed) {
					translation = translation.normalized * m_projectileSettings.m_speed;
				}
				transform.Translate (translation);
			}
		}

		public override void Initialize(IDamageable target, Transform shootPoint) {
			base.Initialize(target,shootPoint);
			Activate();
			if (m_moveCoroutine != null) {
				StopCoroutine(m_moveCoroutine);
			}
			m_moveCoroutine = StartCoroutine(MoveToTarget());
		}
		
		private void OnDestroy() {
			StopAllCoroutines();
		}
	}
}

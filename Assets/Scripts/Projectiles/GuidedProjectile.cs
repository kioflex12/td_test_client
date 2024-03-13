using System.Collections;
using Models;
using Monsters;
using UnityEngine;

namespace Projectiles {
	public class GuidedProjectile : BaseProjectile {
		public override ProjectileType ProjectileType => ProjectileType.GuidedProjectile;

		private IDamageable m_target;
		private Vector3 m_lastLiveTargetPosition;

		IEnumerator MoveToTarget () {

			while (true) {
				yield return new WaitForFixedUpdate();
				if (IsActive == false)
				{
					break;
				}
				if (m_target != null)
				{
					m_lastLiveTargetPosition = m_target.Transform.position;

					if (m_target?.IsAlive == false)
					{
						m_lastLiveTargetPosition = m_target.Transform.position;
						m_target = null;
					}
				}

				var translation = m_lastLiveTargetPosition - transform.position;
				if (translation.magnitude > mProjectileSettings.m_speed) {
					translation = translation.normalized * mProjectileSettings.m_speed;
				}
				transform.Translate (translation);
			}
		}

		public override void Init(IDamageable target, Transform shootPoint) {
			
			gameObject.transform.position = shootPoint.transform.position;
			m_target = target;
			gameObject.SetActive(true);
			StartCoroutine(MoveToTarget());
		}

		private void OnDestroy()
		{
			StopAllCoroutines();
		}
	}
}

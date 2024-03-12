using System.Collections;
using Models;
using Monsters;
using UnityEngine;

namespace Projectiles {
	public class GuidedProjectile : BaseProjectile {
		protected override ProjectileType ProjectileType => ProjectileType.GuidedProjectile;

		private Transform m_target;

		IEnumerator MoveToTarget () {

			while (true) {
				yield return new WaitForFixedUpdate();
				
				var translation = m_target.position - transform.position;
				if (translation.magnitude > m_projectileSetting.m_speed) {
					translation = translation.normalized * m_projectileSetting.m_speed;
				}
				transform.Translate (translation);
			}
		}

		void OnTriggerEnter(Collider other) {
			if (other.TryGetComponent<IDamageable>(out var damageable))
			{
				damageable.ApplyDamage(m_projectileSetting.m_damage);
			}
			Destroy(gameObject);
		}


		public override void Init(IDamageable target){
			if (target is MonoBehaviour monoBehaviour)
			{
				m_target = monoBehaviour.transform;
				StartCoroutine(MoveToTarget());
			}
		}

		private void OnDestroy()
		{
			StopAllCoroutines();
		}
	}
}

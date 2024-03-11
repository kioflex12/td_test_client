using Monsters;
using UnityEngine;

namespace Projectiles {
	public class GuidedProjectile : BaseProjectile {
		public IMovable m_target;
		public float m_speed = 0.2f;
		public int m_damage = 10;

		void Update () {
			if (m_target == null) {
				Destroy (gameObject);
				return;
			}

			var translation = m_target.Transform.position - transform.position;
			if (translation.magnitude > m_speed) {
				translation = translation.normalized * m_speed;
			}
			transform.Translate (translation);
		}

		void OnTriggerEnter(Collider other) {
			if (other.TryGetComponent<IDamageable>(out var damageable))
			{
				damageable.ApplyDamage(m_damage);
			}

			Destroy(gameObject);
		}

		public override void Init(IMovable target){
			m_target = target;
		}
	}
}

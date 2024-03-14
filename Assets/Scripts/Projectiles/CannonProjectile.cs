using Models;
using Monsters;
using UnityEngine;

namespace Projectiles {
	public class CannonProjectile : BaseProjectile
	{
		public override ProjectileType ProjectileType => ProjectileType.CannonProjectile;

		private Vector3 CalculatePredictedTargetPosition(IDamageable damageable)
		{
			if (damageable is Monster monster)
			{
				
			}

			return Vector3.zero;
		}
		// void Update () {
		// 	var translation = transform.forward * m_projectileSettings.m_speed;
		// 	transform.Translate (translation);
		// }

		// void OnTriggerEnter(Collider other) {
		// 	var monster = other.gameObject.GetComponent<Monster> ();
		// 	if (monster == null)
		// 		return;
		//
		// 	monster.m_hp -= m_damage;
		// 	if (monster.m_hp <= 0) {
		// 		Destroy (monster.gameObject);
		// 	}
		// 	Destroy (gameObject);
		// }


		public override void Initialize(IDamageable damageable, Transform shootPoint)
		{
			base.Initialize(damageable, shootPoint);
			m_projectileTargetPosition = CalculatePredictedTargetPosition(damageable);
		}
	}
}

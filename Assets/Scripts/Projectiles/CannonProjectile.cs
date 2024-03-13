using Models;
using Monsters;
using UnityEngine;

namespace Projectiles {
	public class CannonProjectile : BaseProjectile
	{
		public override ProjectileType ProjectileType => ProjectileType.CannonProjectile;

		void Update () {
			var translation = transform.forward * mProjectileSettings.m_speed;
			transform.Translate (translation);
		}

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


		public override void Init(IDamageable damageable, Transform shootPoint)
		{
			//TODO
		}
	}
}

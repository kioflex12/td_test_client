using System;
using Managers;
using Monsters;
using UnityEngine;
using Weapons;

namespace Towers {
	public class Tower : MonoBehaviour {
		public BaseTowerWeapon m_towerWeapon;

		private void Awake() {
			if (m_towerWeapon == null) {
				throw new Exception("Tower has no weapon");
			}
			m_towerWeapon.Init();
		}

		//TODO здесь можно сделать стек из монстров при инициализации башни брать пулл текущих мобов
		//TODO выписывать при взятии в таргет и добавлять в конец стека при спавне
		private IDamageable FindTarget() {
			var monsters = PoolManager.GetOrCreatePool<Monster>().Get();

			if (monsters.Count == 0) {
				return null;
			}

			Monster target = null;
			float selectedTargetSqrMagnitude = 0;
			foreach (var monster in monsters) {
				if (monster.IsAlive == false) {
					continue;
				}
				
				if (target == null) {
					target = monster;
					selectedTargetSqrMagnitude = (target.transform.position - transform.position).sqrMagnitude;
					continue;
				}
				
				var sqrMagnitude = (monster.transform.position - transform.position).sqrMagnitude;
				if (sqrMagnitude < selectedTargetSqrMagnitude) {
					target = monster;
					selectedTargetSqrMagnitude = sqrMagnitude;
				}
			}

			return selectedTargetSqrMagnitude < m_towerWeapon.SqrShootDistance ? target : null;
		}

		void FixedUpdate ()
		{
			if (m_towerWeapon.ShootAvailable() == false) {
				return;
			}

			var shootTarget = FindTarget();
			if (m_towerWeapon.m_shootTarget != shootTarget) {
				m_towerWeapon.SetTarget(shootTarget);
			}

			if (m_towerWeapon.m_shootTarget != null) {
				m_towerWeapon.Shoot();
			}
		}
	}
}

using Managers;
using Monsters;
using Projectiles;
using UnityEngine;

namespace Towers {
	public class SimpleTower : MonoBehaviour, IShootable {
		public float m_shootInterval = 0.5f;
		public float m_range = 4f;
		public BaseProjectile m_projectilePrefab;
		

		private float m_lastShotTime = -0.5f;
		private float m_sqrShootDistance;

		private void Awake()
		{
			m_sqrShootDistance = m_range * m_range;
		}

		void Update ()
		{
			if (m_projectilePrefab == null)
				return;

			if (m_lastShotTime + m_shootInterval > Time.time)
				return;
		
			var target = FindTarget();

			if (target != null)
			{
				TryShoot(target);
			}
			m_lastShotTime = Time.time;
		}

		//TODO здесь можно сделать стек из монстров при инициализации башни брать пулл текущих мобов
		//TODO выписывать при взятии в таргет и добавлять в конец стека при спавне
		private IDamageable FindTarget()
		{
			var allAliveMonsters = PoolManager.GetOrCreatePool<Monster>();

			if (allAliveMonsters.Count == 0)
			{
				return null;
			}

			Monster target = null;
			float selectedTargetSqrMagnitude = 0;
			foreach (var monster in allAliveMonsters)
			{
				if (target == null)
				{
					target = monster;
					selectedTargetSqrMagnitude = (target.transform.position - transform.position).sqrMagnitude;
					continue;
				}
				
				var sqrMagnitude = (monster.transform.position - transform.position).sqrMagnitude;
				if (sqrMagnitude < selectedTargetSqrMagnitude)
				{
					target = monster;
					selectedTargetSqrMagnitude = sqrMagnitude;
				}
			}

			return selectedTargetSqrMagnitude < m_sqrShootDistance ? target : null;
		}


		public void TryShoot(IDamageable target)
		{
			var projectile = Instantiate(m_projectilePrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
			projectile.Init(target);
		}
	}
}

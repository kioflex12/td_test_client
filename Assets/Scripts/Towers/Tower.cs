using System;
using Managers;
using Monsters;
using UnityEngine;
using Weapons;

namespace Towers
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private BaseTowerWeapon towerWeapon;

        private void Awake()
        {
            if (towerWeapon == null) throw new Exception("Tower has no weapon");
            towerWeapon.Init();
        }

        //TODO здесь можно сделать стек из монстров при инициализации башни брать пулл текущих мобов
        //TODO выписывать при взятии в таргет и добавлять в конец стека при спавне
        private IDamageable FindTarget()
        {
            var monsters = PoolManager.GetOrCreatePool<Monster>().Get();

            if (monsters.Count == 0) return null;

            Monster target = null;
            float selectedTargetSqrMagnitude = 0;
            foreach (var monster in monsters)
            {
                if (monster.IsAlive == false) continue;

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

            return selectedTargetSqrMagnitude < towerWeapon.SqrShootDistance ? target : null;
        }

        private void FixedUpdate()
        {
            if (towerWeapon.ShootAvailable() == false) return;

            var shootTarget = FindTarget();
            
            if (towerWeapon.ShootTarget != shootTarget)
            {
                towerWeapon.SetTarget(shootTarget);
            }

            if (towerWeapon.ShootTarget != null)
            {
                towerWeapon.Shoot();
            }
        }
    }
}
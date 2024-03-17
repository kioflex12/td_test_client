using System;
using System.Collections;
using Managers;
using Monsters;
using UnityEngine;

namespace Controllers {
    public class MonsterMover : IDisposable {
        private readonly Transform m_movePoint;
        private readonly GamePool<Monster> m_pool;

        private Coroutine m_moveCoroutine;

        public MonsterMover(Transform moveTargetPoint) {
            m_movePoint = moveTargetPoint;
            m_pool = PoolManager.GetOrCreatePool<Monster>();
            m_moveCoroutine = CoroutineRunner.StartCoroutine(MoveCoroutine());
        }

        private IEnumerator MoveCoroutine() {
            var wfu = new WaitForFixedUpdate();
            while (true) {
                yield return wfu;

                foreach (var monster in m_pool.Get()) {
                    if (monster.IsAlive && monster is IMovable movableMonster)
                    {
                        movableMonster.Move(m_movePoint);
                    }
                }
            }
        }

        public void Dispose() {
            if (m_moveCoroutine != null) {
                CoroutineRunner.Stop(m_moveCoroutine);
            }        
        }
    }
}
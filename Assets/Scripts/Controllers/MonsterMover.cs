using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using Monsters;
using UnityEngine;

namespace Controllers {
    public class MonsterMover : IDisposable {
        private readonly Transform m_movePoint;
        private readonly HashSet<Monster> m_pool;

        private Coroutine m_moveCoroutine;

        public MonsterMover(Transform moveTargetPoint) {
            m_movePoint = moveTargetPoint;
            m_moveCoroutine = CoroutineRunner.StartCoroutine(MoveCoroutine());
            m_pool = PoolManager.GetOrCreatePool<Monster>();
        }

        private IEnumerator MoveCoroutine() {
            while (true) {
                yield return new WaitForFixedUpdate();

                foreach (var monster in m_pool) {
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
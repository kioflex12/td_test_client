using System;
using System.Collections;
using Managers;
using Models;
using Monsters;
using UnityEngine;

namespace Controllers {
    public class MonsterMover : IDisposable {
        private readonly GameSettings m_gameSettings;
        private readonly Transform m_movePoint;
        
        private Coroutine m_moveCoroutine;

        public MonsterMover(GameSettings gameSettings, Transform moveTargetPoint) {
            m_gameSettings = gameSettings;
            m_movePoint = moveTargetPoint;
            m_moveCoroutine = CoroutineRunner.StartCoroutine(MoveCoroutine());
        }

        private IEnumerator MoveCoroutine() {
            while (true) {
                yield return new WaitForFixedUpdate();

                foreach (var monster in MonsterPoolManager.Instance.MonsterPool) {
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
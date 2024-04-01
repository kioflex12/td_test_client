using System;
using System.Collections;
using Managers;
using Monsters;
using UnityEngine;

namespace Controllers
{
    public class MonsterMover : IDisposable
    {
        private readonly Transform _movePoint;
        private readonly GamePool<Monster> _pool;
        private readonly Coroutine _moveRoutine;

        public MonsterMover(Transform moveTargetPoint)
        {
            _movePoint = moveTargetPoint;
            _pool = PoolManager.GetOrCreatePool<Monster>();
            _moveRoutine = CoroutineRunner.StartCoroutine(MoveCoroutine());
        }

        private IEnumerator MoveCoroutine()
        {
            var wfu = new WaitForFixedUpdate();
            while (true)
            {
                yield return wfu;

                foreach (var monster in _pool.Get())
                {
                    if (monster.IsAlive && monster is IMovable movableMonster)
                    {
                        movableMonster.Move(_movePoint);
                    }
                }
            }
        }

        public void Dispose()
        {
            if (_moveRoutine != null)
            {
                CoroutineRunner.Stop(_moveRoutine);
            }
        }
    }
}
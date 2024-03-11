using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class MonsterPoolManager : MonoBehaviour { 
        private static MonsterPoolManager m_instance;
        public static MonsterPoolManager Instance => m_instance;

        private readonly List<Monster> m_monsterPool = new List<Monster>();
        public List<Monster> MonsterPool => m_monsterPool;

        private void Awake()
        {
            if (m_instance == null)
            {
                m_instance = this;
            }
        }

        public void AddMonster(Monster monster)
        {
            if (m_monsterPool.Contains(monster) == false) {
                m_monsterPool.Add(monster);
            }
        }

        public void RemoveMonster(Monster monster)
        {
            if (m_monsterPool.Contains(monster)) {
                m_monsterPool.Remove(monster);
            }
        }

        public List<Monster> GetAllAliveMonsters()
        {
            return m_monsterPool.FindAll(x => x.IsAlive);
        }

        public Monster GetFreeMonster()
        {
            var monster = m_monsterPool.Find(x => x.IsAlive == false);
            return monster;
        }

        public Monster CreateMonster()
        {
            var monsterObject = Resources.Load<Monster>("Monster/Monster");
            var monster = Instantiate(monsterObject, transform);
            return monster;
        }

        public Monster GetOrCreateFreeMonster()
        {
            var monster = GetFreeMonster();
            if (monster == null)
            {
                monster = CreateMonster();
            }
            return monster;
        }
    }
}
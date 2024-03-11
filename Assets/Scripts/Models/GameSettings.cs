using System;
using UnityEngine;

namespace Models
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "SO/Create GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public MonsterSpawnSettings m_spawnSettings;
        public MonsterSettings m_monsterSettings;
        
        [Serializable]
        public class MonsterSpawnSettings
        {
            public float m_interval = 3;
        }
        
        [Serializable]
        public class MonsterSettings
        {
            public float m_speed = 0.1f;
            public int m_maxHP = 30;
        }
    }
}
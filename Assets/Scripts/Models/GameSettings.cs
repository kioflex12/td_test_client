using System;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "SO/Create GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public MonsterSpawnSettings m_spawnSettings;
        public MonsterSettings m_monsterSettings;
        public List<ProjectileSetting> projectileSettings;

        public ProjectileSetting GetProjectileSettings(ProjectileType projectileType)
        {
            foreach (var projectileSetting in projectileSettings)
            {
                if (projectileSetting.projectileType == projectileType)
                {
                    return projectileSetting;
                }
            }

            throw new Exception($"Can`t find projectile settings with Type {projectileType}");
        }
        
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
        
        [Serializable]
        public class ProjectileSetting
        {
            public ProjectileType projectileType; 
            public float m_speed = 0.2f;
            public int m_damage = 10;
        }
    }
}
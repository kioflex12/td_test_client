using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Models {
    [CreateAssetMenu(fileName = "GameSettings", menuName = "SO/Create GameSettings")]
    public class GameSettings : ScriptableObject {
        public MonsterSpawnSettings m_spawnSettings;
        public MonsterSettings m_monsterSettings;
        public List<BaseTowerWeaponSettings> m_towerWeaponSettings;
        public List<ProjectileSettings> m_projectileSettings;
        public CannonTowerWeaponSettings m_cannonTowerWeaponSettings;

        public ProjectileSettings GetProjectileSettings(ProjectileType projectileType) {
            foreach (var projectileSetting in m_projectileSettings)
            {
                if (projectileSetting.m_projectileType == projectileType)
                {
                    return projectileSetting;
                }
            }

            throw new Exception($"Can`t find projectile settings with Type {projectileType}");
        }
        
        public BaseTowerWeaponSettings GetWeaponTowerSettings(WeaponType weaponType) {
            foreach (var weaponSetting in m_towerWeaponSettings)
            {
                if (weaponSetting.m_weaponType == weaponType)
                {
                    return weaponSetting;
                }
            }

            throw new Exception($"Can`t find projectile settings with Type {weaponType}");
        }

        
        [Serializable]
        public class MonsterSpawnSettings {
            public float m_interval = 3;
        }
        
        [Serializable]
        public class MonsterSettings {
            public float m_speed = 0.1f;
            public int m_maxHP = 30;
        }
        
        [Serializable]
        public class ProjectileSettings {
            public ProjectileType m_projectileType; 
            public float m_speed = 0.2f;
            public int m_damage = 10;
        }

        
        [Serializable]
        public class BaseTowerWeaponSettings {
            public WeaponType m_weaponType; 
            public float m_shootInterval = 0.5f;
            public float m_range = 4f;
        } 
        
        [Serializable]
        public class CannonTowerWeaponSettings  {
            public float m_initialSpeed = 18f;
            public float m_angle = 45f;
        }
    }
}
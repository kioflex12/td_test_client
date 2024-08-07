using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Models
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "SO/Create GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private MonsterSpawnSettings spawnSettings;
        [SerializeField] private MonsterSettings monsterBalanceSettings;
        [SerializeField] private List<BaseTowerWeaponSettings> towerWeaponSettings;
        [SerializeField] private List<ProjectileSettings> projectileBalanceSettings;
        [SerializeField] private CannonTowerWeaponSettings cannonTowerSettigns;
        
        public MonsterSpawnSettings SpawnSettings => spawnSettings;
        public MonsterSettings MonsterBalanceSettings => monsterBalanceSettings;
        public List<BaseTowerWeaponSettings> TowerWeaponSettings => towerWeaponSettings;
        public List<ProjectileSettings> ProjectileBalanceSettings => projectileBalanceSettings;
        public CannonTowerWeaponSettings CannonTowerSettigns => cannonTowerSettigns;

        public ProjectileSettings GetProjectileSettings(ProjectileType projectileType)
        {
            foreach (var projectileSetting in projectileBalanceSettings)
            {
                if (projectileSetting.projectileType == projectileType)
                {
                    return projectileSetting;
                }
            }
            throw new Exception($"Can`t find projectile settings with Type {projectileType}");
        }

        public BaseTowerWeaponSettings GetWeaponTowerSettings(WeaponType weaponType)
        {
            foreach (var weaponSetting in towerWeaponSettings)
            {
                if (weaponSetting.weaponType == weaponType)
                {
                    return weaponSetting;
                }
            }
            throw new Exception($"Can`t find projectile settings with Type {weaponType}");
        }


        [Serializable]
        public class MonsterSpawnSettings
        {
            public float interval = 3;
        }

        [Serializable]
        public class MonsterSettings
        {
            public float speed = 0.1f;
            public int maxHP = 30;
        }

        [Serializable]
        public class ProjectileSettings
        {
            public ProjectileType projectileType;
            public float speed = 0.2f;
            public int damage = 10;
        }


        [Serializable]
        public class BaseTowerWeaponSettings
        {
            public WeaponType weaponType;
            public float shootInterval = 0.5f;
            public float range = 4f;
        }

        [Serializable]
        public class CannonTowerWeaponSettings
        {
            public float initialSpeed = 18f;
            public float angle = 45f;
            public float rotationSpeed = 5f;
        }
    }
}

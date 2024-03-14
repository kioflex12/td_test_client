using Models;
using UnityEngine;
using static Models.GameSettings;

namespace Weapons
{
    public class CannonTowerTowerWeapon : BaseTowerWeapon
    {
        private CannonTowerWeaponSettings m_cannonTowerWeaponSetting;
        
        protected override WeaponType WeaponType => WeaponType.CannonWeapon;
        
        public override void Shoot() {
            if (m_shootTarget == null) {
                return;
            }
            var projectile = GetOrCreateProjectile();
            projectile.Init(m_shootTarget, m_shootPoint);
            m_lastShotTime = Time.time;
            
            projectile.Rigidbody.velocity = Vector3.zero;
            projectile.Rigidbody.angularVelocity = Vector3.zero;

            Vector3 predictedTargetPosition;
            if (projectile.MovableTarget != null && projectile.MovableTarget.MoveTargetTransform != null) {
                var movableTarget = projectile.MovableTarget;
                var time = CalculateProjectileMoveTime(m_shootPoint.position.y, movableTarget.Transform.position.y, m_cannonTowerWeaponSetting.m_initialSpeed, m_cannonTowerWeaponSetting.m_angle);
                predictedTargetPosition = GetPredictPosition(movableTarget.Transform.position, movableTarget.MoveTargetTransform.position, movableTarget.MoveSpeed, time + 0.1f);
            } else {
                predictedTargetPosition = projectile.DamageableTarget.Transform.position;
            }
            LaunchProjectile(projectile.Rigidbody, m_shootPoint.position, predictedTargetPosition, m_cannonTowerWeaponSetting.m_initialSpeed, m_cannonTowerWeaponSetting.m_angle);
        }
        
        public void LaunchProjectile(Rigidbody projectileRigidbody, Vector3 launchPosition, Vector3 targetPosition, float initialSpeed, float launchAngleDegrees) {
            projectileRigidbody.position = launchPosition;
            var initialVelocity = CalculateLaunchVelocity(launchPosition, targetPosition, initialSpeed, launchAngleDegrees);
            projectileRigidbody.velocity = initialVelocity; 
        }
        
        private Vector3 CalculateLaunchVelocity(Vector3 launchPosition, Vector3 targetPosition, float initialSpeed, float angle) {
            var toTarget = targetPosition - launchPosition;
            var distance = Vector3.Distance(new Vector3(launchPosition.x, 0, launchPosition.z), new Vector3(targetPosition.x, 0, targetPosition.z));
            var angleRadians = angle * Mathf.Deg2Rad;
            var v0Y = initialSpeed * Mathf.Sin(angleRadians);
            var time = CalculateProjectileMoveTime(launchPosition.y, targetPosition.y, initialSpeed, angle);
            var v0X = distance / time;
            var velocity = new Vector3(toTarget.normalized.x * v0X, v0Y, toTarget.normalized.z * v0X);

            return velocity;
        }

        private static float CalculateProjectileMoveTime(float launchHeight, float targetHeight, float initialSpeed, float angle)
        {
            var angleRadians = angle * Mathf.Deg2Rad;
            var v0Y = initialSpeed * Mathf.Sin(angleRadians);
            var extraHeight = v0Y * v0Y / (2 * Physics.gravity.magnitude);
            var heightDifference = targetHeight - launchHeight;
            var time = (v0Y + Mathf.Sqrt(v0Y * v0Y + 2 * Physics.gravity.magnitude * (heightDifference + extraHeight))) / Physics.gravity.magnitude;
            return time;
        }

        private Vector3 GetPredictPosition(Vector3 currentPosition, Vector3 targetPosition, float speed, float predictTime) {
            var fixedUpdateCalls = predictTime / Time.fixedDeltaTime; 
            var totalMovement = speed * fixedUpdateCalls; 
            var direction = (targetPosition - currentPosition).normalized;
            var predictPosition = currentPosition + direction * totalMovement;
            
            return predictPosition;
        }

        public override void Init() {
            base.Init();
            m_cannonTowerWeaponSetting = ModelsProvider.GameSettings.m_cannonTowerWeaponSettings;
        }


        private void Update()
        {
            // if (m_shootTarget == null)
            // {
            //     m_cannonTranform.position = Vector3.zero;
            //     return;
            // }
        }
    }
}
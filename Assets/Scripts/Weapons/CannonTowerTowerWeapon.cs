using Models;
using UnityEngine;
using static Models.GameSettings;

namespace Weapons {
    public class CannonTowerTowerWeapon : BaseTowerWeapon {
        private CannonTowerWeaponSettings m_cannonTowerWeaponSetting;
        
        protected override WeaponType WeaponType => WeaponType.CannonWeapon;
        
        public override void Init() {
            base.Init();
            m_cannonTowerWeaponSetting = ModelsProvider.GameSettings.m_cannonTowerWeaponSettings;
        }
        
        public override void Shoot() {
            if (m_shootTarget == null) {
                return;
            }
            var projectile = GetOrCreateProjectile();
            projectile.Initialize(m_shootTarget, m_shootPoint);

            Vector3 predictedTargetPosition;
            if (projectile.MovableTarget != null && projectile.MovableTarget.MoveTargetTransform != null) {
                var movableTarget = projectile.MovableTarget;
                var time = CalculateProjectileMoveTime(m_shootPoint.position.y, movableTarget.Transform.position.y, m_cannonTowerWeaponSetting.m_initialSpeed, m_cannonTowerWeaponSetting.m_angle);
                predictedTargetPosition = GetPredictPosition(movableTarget.Transform.position, movableTarget.MoveTargetTransform.position, movableTarget.MoveSpeed, time + 0.1f);
            } else {
                return;
            }

            projectile.Rigidbody.velocity = Vector3.zero;
            projectile.Rigidbody.angularVelocity = Vector3.zero;
            projectile.Activate();
            LaunchProjectile(projectile.Rigidbody, m_shootPoint.position, predictedTargetPosition, m_cannonTowerWeaponSetting.m_initialSpeed, m_cannonTowerWeaponSetting.m_angle);
            m_lastShotTime = Time.time;
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

        //TODO чёто очень неоптимизированное не придумал лучше
        private void FixedUpdate() {
            if (m_shootTarget != null) {
                var time = CalculateProjectileMoveTime(m_shootPoint.position.y, m_movableTarget.Transform.position.y, m_cannonTowerWeaponSetting.m_initialSpeed, m_cannonTowerWeaponSetting.m_angle);
                var predictedTargetPosition = GetPredictPosition(m_shootPoint.position, m_shootTarget.Transform.position, m_movableTarget.MoveSpeed, time + 0.1f);
                
                var directionToTarget = predictedTargetPosition - transform.position;
                var targetRotation = Quaternion.LookRotation(directionToTarget);
                var newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, m_cannonTowerWeaponSetting.m_rotationSpeed * Time.fixedDeltaTime);

                transform.rotation = Quaternion.Euler(0, newRotation.eulerAngles.y, 0);
            }
        }
    }
}
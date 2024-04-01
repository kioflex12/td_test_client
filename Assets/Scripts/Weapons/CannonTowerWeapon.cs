using Models;
using UnityEngine;
using static Models.GameSettings;

namespace Weapons
{
    public class CannonTowerWeapon : BaseTowerWeapon
    {
        private CannonTowerWeaponSettings _cannonTowerWeaponSetting;
        private Vector3 _deltaDiffPredictPosition;

        protected override WeaponType WeaponType => WeaponType.CannonWeapon;

        public override void Init()
        {
            base.Init();
            _cannonTowerWeaponSetting = ModelsProvider.GameSettings.CannonTowerSettigns;
        }

        public override void Shoot()
        {
            if (shootTarget.IsAlive == false) return;

            var projectile = GetOrCreateProjectile();
            projectile.Initialize(shootTarget, ShootPoint);

            Vector3 predictedTargetPosition;
            if (projectile.MovableTarget != null && projectile.MovableTarget.MoveTarget != null)
            {
                var movableTarget = projectile.MovableTarget;
                var time = CalculateProjectileMoveTime(
                    ShootPoint.position.y,
                    movableTarget.Transform.position.y,
                    _cannonTowerWeaponSetting.initialSpeed, 
                    _cannonTowerWeaponSetting.angle);
                predictedTargetPosition = movableTarget.GetPredictPosition(time);
            }
            else
            {
                return;
            }

            _deltaDiffPredictPosition = predictedTargetPosition - shootTarget.Transform.position;
            projectile.Rigidbody.velocity = Vector3.zero;
            projectile.Rigidbody.angularVelocity = Vector3.zero;
            projectile.Activate();
            LaunchProjectile(
                projectile.Rigidbody,
                ShootPoint.position,
                predictedTargetPosition,
                _cannonTowerWeaponSetting.initialSpeed,
                _cannonTowerWeaponSetting.angle);
            
            lastShotTime = Time.time;
        }

        public void LaunchProjectile(Rigidbody projectileRigidbody, Vector3 launchPosition, Vector3 targetPosition, float initialSpeed, float launchAngleDegrees)
        {
            projectileRigidbody.position = launchPosition;
            var initialVelocity = CalculateLaunchVelocity(launchPosition, targetPosition, initialSpeed, launchAngleDegrees);
            projectileRigidbody.velocity = initialVelocity;
        }

        private Vector3 CalculateLaunchVelocity(Vector3 launchPosition, Vector3 targetPosition, float initialSpeed, float angle)
        {
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
            var heightDifference = Mathf.Abs(targetHeight - launchHeight);
            var time = (v0Y + Mathf.Sqrt(v0Y * v0Y + 2 * Physics.gravity.magnitude * (heightDifference + extraHeight))) / Physics.gravity.magnitude;
            var correctFactor = v0Y * -0.04f;
            return time + correctFactor;
        }

        private void FixedUpdate()
        {
            if (shootTarget != null && shootTarget.IsAlive)
            {
                var predictPosition = shootTarget.Transform.position + _deltaDiffPredictPosition;
                var directionToTarget = predictPosition - transform.position;
                var targetRotation = Quaternion.LookRotation(directionToTarget);
                var newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _cannonTowerWeaponSetting.rotationSpeed * Time.fixedDeltaTime);
                transform.rotation = Quaternion.Euler(0, newRotation.eulerAngles.y, 0);
            }
        }
    }
}
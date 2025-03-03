using System.Collections;
using System.Collections.Generic;
using _Game.Enums;
using UnityEngine;
using _Game.Interfaces;
using _Game.Managers;
using _Game.Utils;
using Unity.Mathematics.Geometry;
using Unity.VisualScripting;

namespace _Game.CombatSystem
{
    public class Projectile : MonoBehaviour
    {
        
        [SerializeField] private Rigidbody rigidbody;
        private ObjectPool<Projectile> _pool;
        private Weapon _weapon;
        private IDamageable _target;
        private float _shootingSpeed;
        private float _damage;
        private float _time;
        private int _ricochetCount;
        private bool _usingUnityPhysics;
        private Vector3 _targetPosition;
        private Vector3 _startPosition;
        private List<ProjectileBehavior> _behaviors = new List<ProjectileBehavior>();
        private List<EnemyCharacter> _targetedEnemies = new List<EnemyCharacter>();

        public void Initialize(Weapon weapon, List<ProjectileBehavior> behaviors, ObjectPool<Projectile> sourcePool)
        {
            _weapon = weapon;
            _shootingSpeed = weapon.Owner.StatController.GetStatValue(StatType.AttackSpeed);
            _damage = weapon.ActiveProjectileData.Damage;
            _target = weapon.CurrentTarget;
            _targetPosition = weapon.CurrentTarget.GetPosition() + Vector3.up;
            _behaviors.Clear();
            _behaviors.AddRange(behaviors);
            _pool = sourcePool;
            EventManager.OnTargetDeath += OnTargetDeath;
        }
        

        public void Launch(float ricochetCount, bool usingUnityPhysics)
        {
            _ricochetCount = (int)ricochetCount;
            _usingUnityPhysics = usingUnityPhysics;
            
            KinematicLaunch(_weapon.FirePoint.position, _targetPosition);
        }


        
        public void UnityPhysicsLaunch(Vector3 origin ,Vector3 target)
        {
            transform.position = origin;
        
            if (!CalculateLaunchVelocity(origin, target, out Vector3 velocity))
            {
                Debug.LogError("Not enough speed to reach the target!");
                return;
            }
        
            rigidbody.linearVelocity = velocity;
        }
        
        private bool CalculateLaunchVelocity(Vector3 start, Vector3 target, out Vector3 velocity)
        {
            velocity = Vector3.zero;
            Vector3 toTarget = target - start;
            float horizontalDistance = new Vector3(toTarget.x, 0, toTarget.z).magnitude;
            float heightDifference = target.y - start.y;
            float gravity = Mathf.Abs(Physics.gravity.y);
        
            float minSpeedRequired = Mathf.Sqrt(gravity * horizontalDistance * horizontalDistance / (2 * heightDifference));
            if (_shootingSpeed < minSpeedRequired)
            {
                _shootingSpeed = minSpeedRequired;
            }
        
            float termInsideSqrt = (_shootingSpeed * _shootingSpeed * _shootingSpeed * _shootingSpeed) -
                                   gravity * (gravity * horizontalDistance * horizontalDistance + 2 * heightDifference * _shootingSpeed * _shootingSpeed);
        
            if (termInsideSqrt < 0)
                return false;
        
            float theta = Mathf.Atan((_shootingSpeed * _shootingSpeed + Mathf.Sqrt(termInsideSqrt)) / (gravity * horizontalDistance));
        
            float vx = _shootingSpeed * Mathf.Cos(theta);
            float vy = _shootingSpeed * Mathf.Sin(theta);
        
            velocity = new Vector3(toTarget.x / horizontalDistance * vx, vy, toTarget.z / horizontalDistance * vx);
            return true;
        }
        
        [SerializeField] private float baseApexHeight = 50f; // Default apex height at low speed
        [SerializeField] private float speedFactor = 0.5f;
        
        private IEnumerator KinematicMovementCoroutine(Vector3 start, Vector3 target, IDamageable targetDamageable)
    {
        Vector3 startPos = start;
        Vector3 targetPos = target;
        float distance = Vector3.Distance(startPos, targetPos);

        // Dynamically calculate apex height based on speed
        float apexHeight = CalculateApexHeight(_weapon.Owner.StatController.GetStatValue(StatType.AttackSpeed));

        // Calculate the horizontal midpoint and set apex height
        Vector3 midPoint = (startPos + targetPos) / 2f;
        midPoint.y = Mathf.Max(startPos.y, targetPos.y) + apexHeight;

        float totalTime = distance / _weapon.Owner.StatController.GetStatValue(StatType.AttackSpeed);
        float elapsedTime = 0f;

        while (elapsedTime < totalTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / totalTime);

            // Quadratic Bezier interpolation
            Vector3 position = CalculateBezierPoint(startPos, midPoint, targetPos, t);
            transform.position = position;

            yield return null;
        }

        transform.position = targetPos;
        HitTarget(targetDamageable);
    }

    private float CalculateApexHeight(float speed)
    {
        // Inverse relationship: higher speed → lower apex height
        return baseApexHeight / (1 + speed * speedFactor);
    }

    private Vector3 CalculateBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 point = uu * p0; // (1-t)^2 * p0
        point += 2 * u * t * p1; // 2*(1-t)*t * p1
        point += tt * p2; // t^2 * p2

        return point;
    }

    // Modify the KinematicLaunch method to use the new coroutine
    private void KinematicLaunch(Vector3 origin, Vector3 target)
    {
        _startPosition = origin;
        _targetPosition = target;
        transform.position = _startPosition;
        Debug.Log("shooting speed" + _weapon.Owner.StatController.GetStatValue(StatType.AttackSpeed));
        if (this.gameObject.activeSelf)
        {
            StartCoroutine(KinematicMovementCoroutine(_startPosition, _targetPosition, _target));
        }
    }
        
        
        // private bool CalculateLaunchVelocity2(out Vector3 velocity)
        // {
        //     velocity = Vector3.zero;
        //     Vector3 toTarget = _targetPosition - _startPosition;
        //     float horizontalDistance = new Vector3(toTarget.x, 0, toTarget.z).magnitude;
        //     float heightDifference = (_targetPosition.y - _startPosition.y);
        //     float gravity = Mathf.Abs(Physics.gravity.y);
        //
        //     // If speed is too low, adjust it to the minimum required speed
        //     float minSpeedRequired = Mathf.Sqrt(gravity * horizontalDistance * horizontalDistance / (2 * heightDifference));
        //     if (_shootingSpeed < minSpeedRequired)
        //     {
        //         _shootingSpeed = minSpeedRequired;
        //     }
        //
        //     // Solve for the required angle
        //     float termInsideSqrt = (_shootingSpeed * _shootingSpeed * _shootingSpeed * _shootingSpeed) -
        //                            gravity * (gravity * horizontalDistance * horizontalDistance + 2 * heightDifference * _shootingSpeed * _shootingSpeed);
        //
        //     if (termInsideSqrt < 0)
        //         return false; // No valid solution for given speed
        //
        //     float thetaLow = Mathf.Atan((_shootingSpeed * _shootingSpeed - Mathf.Sqrt(termInsideSqrt)) / (gravity * horizontalDistance));
        //     float thetaHigh = Mathf.Atan((_shootingSpeed * _shootingSpeed + Mathf.Sqrt(termInsideSqrt)) / (gravity * horizontalDistance));
        //
        //     // Choose the lower angle for a more natural feel
        //     float theta = Mathf.Min(thetaLow, thetaHigh);
        //
        //     // Convert to velocity components
        //     float vx = _shootingSpeed * Mathf.Cos(theta);
        //     float vy = _shootingSpeed * Mathf.Sin(theta);
        //
        //     velocity = new Vector3(toTarget.x / horizontalDistance * vx, vy, toTarget.z / horizontalDistance * vx);
        //     return true;
        // }
        //
        //
        //
        //
        // IEnumerator KinematicMovementCoroutine(Vector3 velocity, Vector3 target, IDamageable targetDamageable, float distSign = 1)
        // {
        //     float mDistSign = distSign;
        //     while (Vector3.Distance(transform.position, target) > 1f && mDistSign * distSign > 0)
        //     {
        //         _time += Time.deltaTime;
        //
        //         // Apply arc motion using kinematic equations
        //         Vector3 displacement = velocity * _time + Physics.gravity * (0.5f * (_time * _time));
        //         transform.position = _startPosition + displacement;
        //         mDistSign = Mathf.Sign(transform.position.magnitude - _targetPosition.magnitude);
        //         yield return null;
        //     }
        //     transform.position = target;
        //     HitTarget(targetDamageable);
        // }
        
        private void HitTarget(IDamageable target)
        {
            _targetedEnemies.Add((EnemyCharacter)target);
            foreach (var behavior in _behaviors)
            {
                behavior.ApplyEffect(target, _weapon.Owner);
            }
            
            if (_ricochetCount > 0)
            {
                _ricochetCount--;
                var mOldTarget = _target;
                _shootingSpeed = _shootingSpeed = _weapon.ShootingSpeed;
                _target = CombatManager.Instance.FindNearestEnemy(transform.position, 50,(EnemyCharacter)target,_targetedEnemies);
                if(target!=null)
                    _targetPosition = _target.GetPosition();
                if(_usingUnityPhysics)
                    UnityPhysicsLaunch(transform.position+Vector3.up*.1f, _targetPosition);
                else
                    KinematicLaunch(transform.position + Vector3.up*.1f, _targetPosition);
                mOldTarget.TakeDamage(_damage);
            }
            else
            {
                target.TakeDamage(_damage);
                ReturnToPool();
            }
        }
        

        public void ReturnToPool()
        {
            EventManager.OnTargetDeath -= OnTargetDeath;
            StopAllCoroutines();
            _pool.Return(this);
        }
        
        private void OnTargetDeath(IDamageable target)
        {
            if (target == _target)
            {
                ReturnToPool();
            }
        }
    }
}
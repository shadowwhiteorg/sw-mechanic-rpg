using System;
using System.Collections.Generic;
using _Game.Managers;
using _Game.Enums;
using _Game.Interfaces;
using _Game.SkillSystem;
using UnityEngine;
using _Game.Utils;
using UnityEngine.Serialization;

namespace _Game.CombatSystem
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private bool usingUnityPhysics = false;
        [SerializeField] private Transform firePoint;
        [SerializeField] private List<ProjectileData> allProjectiles;
        [SerializeField] private List<ProjectileBehavior> extraProjectileBehaviors;
        [SerializeField] private WeaponData weaponData;
        [SerializeField] private ProjectileData defaultProjectile;
        
        public float ShootingSpeed => weaponData.ShootingSpeed;
        public float AttackRate => weaponData.AttackRate;
        public Transform FirePoint => firePoint;
        private BaseCharacter _owner;
        public BaseCharacter Owner => _owner;
        private Dictionary<StatusEffectType, ObjectPool<Projectile>> _pools = new Dictionary<StatusEffectType, ObjectPool<Projectile>>();
        private ProjectileData _activeProjectileData; // Stores the current projectile type
        public ProjectileData ActiveProjectileData => _activeProjectileData;
        private IDamageable _currentTarget;
        public IDamageable CurrentTarget => _currentTarget;

        private void Awake()
        {
            InitializeProjectilePools();
            SetActiveProjectile(StatusEffectType.Default); // Default projectile type
        }

        public void Initialize(BaseCharacter character)
        {
            _owner = character;
        }

        private void OnEnable()
        {
            // EventManager.OnEnemyDeath += ClearActiveProjectiles;
            EventManager.OnEnemyDeath += SetCurrentTarget;
        }

        private void OnDisable()
        {
            // EventManager.OnEnemyDeath -= ClearActiveProjectiles;
            EventManager.OnEnemyDeath -= SetCurrentTarget;
        }

        private void InitializeProjectilePools()
        {
            foreach (var projectileData in allProjectiles)
            {
                if (!_pools.ContainsKey(projectileData.Type))
                {
                    _pools[projectileData.Type] = new ObjectPool<Projectile>(projectileData.Prefab.GetComponent<Projectile>(), 10,this.transform);
                }
            }
        }

        public void SetCurrentTarget()
        {
            _currentTarget = CombatManager.Instance.FindNearestEnemy(transform.position, 50);
            _owner.transform.LookAt(CurrentTarget.GetPosition());
        }
        
        public void Attack()
        {
            if (_activeProjectileData == null)
                _activeProjectileData = defaultProjectile;

            List<ProjectileBehavior> projectileBehaviors = new List<ProjectileBehavior>();
            projectileBehaviors.AddRange(_activeProjectileData.DefaultBehaviors);
            projectileBehaviors.AddRange(extraProjectileBehaviors);

            if (_pools.TryGetValue(_activeProjectileData.Type, out var pool))
            {
                Projectile projectile = pool.Get();
                projectile.Initialize(this, projectileBehaviors, pool);
                projectile.transform.position = firePoint.position;
                projectile.transform.rotation = firePoint.rotation;
                projectile.Launch(_owner.StatController.GetStatValue(StatType.RicochetCount),usingUnityPhysics);
            }
        }

        public void SetActiveProjectile(StatusEffectType newType)
        {
            ProjectileData newProjectile = allProjectiles.Find(p => p.Type == newType);
            if (newProjectile != null)
            {
                _activeProjectileData = newProjectile;
            }
        }
        
        

        public void AddExtraProjectileBehavior(ProjectileBehavior behavior)
        {
            if (!extraProjectileBehaviors.Contains(behavior))
                extraProjectileBehaviors.Add(behavior);
        }
        
        public void RemoveProjectileBehavior(ProjectileBehavior behavior)
        {
            if (extraProjectileBehaviors.Contains(behavior))
                extraProjectileBehaviors.Remove(behavior);
        }
    }
}
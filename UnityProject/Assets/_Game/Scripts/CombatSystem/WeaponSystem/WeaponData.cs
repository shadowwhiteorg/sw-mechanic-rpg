using UnityEngine;

namespace _Game.CombatSystem
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "CombatSystem/WeaponData", order = 0)]
    public class WeaponData : ScriptableObject
    {
        [Tooltip("Unique identifier for the weapon")]
        public string WeaponID;
        [Tooltip("Rate of attack per second")]
        public float AttackRate;
        public float ShootingSpeed;
        [Tooltip("* Indicates if the weapon has a horizontal angle between multiple attacks")]
        public bool HasAngularAttack;
        [Tooltip("Number of projectiles fired per attack")]
        public int AttackCount;
        [Tooltip("Particle effect played at the muzzle when attacking")]
        public ParticleSystem MuzzleParticle;
        [Tooltip("Projectile prefab to be fired by the weapon without any projectile related skill")]
        public Projectile DefaultProjectile;

    }
}
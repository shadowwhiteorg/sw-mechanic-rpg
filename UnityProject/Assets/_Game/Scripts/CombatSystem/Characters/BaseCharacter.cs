using System.Collections.Generic;
using System.Collections;
using System.Linq;
using _Game.Enums;
using _Game.Interfaces;
using _Game.Managers;
using _Game.DataStructures;
using _Game.Scripts.UISystem;
using _Game.SkillSystem;
using _Game.StatSystem;
using DamageNumbersPro;
using UnityEngine;

namespace _Game.CombatSystem
{
    public class BaseCharacter : MonoBehaviour , IDamageable
    {
        [SerializeField] private StatConfig statConfig;
        [SerializeField] private List<BaseSkill> initialSkills;
        [SerializeField] private List<BaseSkill> activeSkills;
        [SerializeField] private CharacterModel characterModel;
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private DamageNumber damageNumber;
        [SerializeField] private DamageNumber burnDamageNumber;
        public CharacterState CharacterState;
        public StatController StatController;
        public MovingActor MovingActor => GetComponent<MovingActor>();
        public AttackingActor AttackingActor => GetComponent<AttackingActor>();
        
        public CharacterModel CharacterModel => characterModel;
        private float _currentHealth;
        public float CurrentHealth => _currentHealth;
        public float BaseHealth { get; set; }
        private Dictionary<StatusEffectType, StatusEffect> _activeEffects = new Dictionary<StatusEffectType, StatusEffect>();
        private List<BaseSkill> _duplicatedSkills = new List<BaseSkill>();

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            StatController = new StatController(statConfig);
            BaseHealth = StatController.GetStatValue(StatType.Health);
            _currentHealth = BaseHealth;
            healthBar?.UpdateHealthBar(_currentHealth, BaseHealth);
            healthBar?.transform.LookAt(Camera.main?.transform);
            foreach (var skill in initialSkills)
            {
                LearnSkill(skill);
            }
        }
        

        public virtual Vector3 GetPosition()
        {
            return transform.position;
        }
        
        public void ApplyStatusEffect(StatusEffectType effectType, float effectDuration, float effectValue)
        {
            if (_activeEffects.TryGetValue(effectType, out StatusEffect existingEffect))
            {
                // Extend duration if effect is already active
                existingEffect.ExtendDuration(effectDuration);
            }
            else
            {
                // Create and start a new effect
                StatusEffect newEffect = new StatusEffect(effectType, effectValue, effectDuration);
                _activeEffects[effectType] = newEffect;
                StartCoroutine(HandleStatusEffect(newEffect));
            }
        }
        
        
        
        
        private IEnumerator HandleStatusEffect(StatusEffect effect)
        {
            while (effect.Duration > 0)
            {
                if(effect.Type==StatusEffectType.Fire)
                    burnDamageNumber.Spawn(transform.position + Vector3.up*3f, effect.DamagePerSecond);
                TakeDamage(effect.DamagePerSecond,true);
                yield return new WaitForSeconds(1f);
                effect.Duration -= 1f;
            }

            // Remove expired effect
            _activeEffects.Remove(effect.Type);
        }

        public void TakeDamage(float damage, bool fromStatusEffect = false)
        {
            _currentHealth -= damage;
            healthBar.UpdateHealthBar(_currentHealth , BaseHealth);
            if(!fromStatusEffect)
                damageNumber.Spawn(transform.position+Vector3.up*2.5f, damage);
            if (_currentHealth <= 0)
            {
                StopAllCoroutines();
                Die();
            }
            else
            {
                characterModel.PlayHitAnimation();
            }
        }

        protected virtual void Die()
        {
            characterModel.PlayDeathAnimation();
            healthBar.gameObject.SetActive(false);
            StartCoroutine(DisableAfterDie());
            EventManager.FireOnTargetDeath(this);
        }
        
        public void LearnSkill(BaseSkill skill)
        {
            skill.ApplySkill(this);
            activeSkills.Add(skill);
        }

        public void RemoveSkill(BaseSkill skill)
        {
            if (AttackingActor.InRage)
            {
                skill.CoupleSkill?.RemoveSkill(this);
                activeSkills.Remove(skill.CoupleSkill);
            }
            skill.RemoveSkill(this);
            activeSkills.Remove(skill);
        }
        
        private IEnumerator DisableAfterDie()
        {
            yield return new WaitForSeconds(1f);
            while(transform.position.y > -2)
            {
                transform.position -= Vector3.up * Time.deltaTime;
                yield return null;
            }
            Destroy(gameObject);
        }
        
        public void DuplicateActiveSkills()
        {
            List<BaseSkill> duplicatedSkills = new List<BaseSkill>();

            foreach (var skill in activeSkills)
            {
                var duplicatedSkill = skill.CreateDuplicate();
                duplicatedSkill.SetCoupleSkill(skill);
                duplicatedSkills.Add(duplicatedSkill);
            }

            foreach (var duplicatedSkill in duplicatedSkills)
            {
                LearnSkill(duplicatedSkill);
            }
        }
        
        public void RemoveAllDuplicatedSkills()
        {
            var mSkillsToRemove = activeSkills.Where(skill => skill.CoupleSkill != null).ToList();
            foreach (var skill in mSkillsToRemove)
            {
                skill.RemoveSkill(this);
                activeSkills.Remove(skill);
            }
        }
        
    }
}
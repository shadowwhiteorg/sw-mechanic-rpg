using _Game.CombatSystem;
using UnityEngine;

namespace _Game.SkillSystem
{
    public abstract class SkillEffectData : ScriptableObject
    {
        public abstract void ApplyEffect(BaseCharacter character);
        public abstract void RemoveEffect(BaseCharacter character);
    }
}
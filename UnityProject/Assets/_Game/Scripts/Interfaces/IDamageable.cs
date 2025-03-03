using System.Collections.Generic;
using _Game.Enums;
using UnityEngine;

namespace _Game.Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(float damage, bool fromStatusEffect = false);
        Vector3 GetPosition();
        void ApplyStatusEffect(StatusEffectType statusEffect, float effectDuration = 0, float effectValue = 0);
    }
    
}
using System.Collections.Generic;
using System.Linq;
using _Game.DataStructures;
using _Game.Enums;
using Mono.Cecil.Cil;
using UnityEngine;

namespace _Game.StatSystem
{
    public class Stat
    {
        public float BaseValue { get; private set; }
        private float _currentValue;
        private List<StatModifier> _modifiers = new List<StatModifier>();

        public Stat(float baseValue)
        {
            BaseValue = baseValue;
            _currentValue = baseValue;
        }

        public float GetValue() => _currentValue;

        public void AddModifier(StatModifier modifier)
        {
            _modifiers.Add(modifier);
            RecalculateValue();
        }

        public void RemoveModifier(StatModifier modifier)
        {
            var existingModifier = _modifiers.FirstOrDefault(m => m.Value == modifier.Value && m.Type == modifier.Type);
            if (existingModifier != null)
            {
                Debug.Log("remove modifier at stat "+_modifiers.Contains(existingModifier));
                _modifiers.Remove(existingModifier);
            }
            RecalculateValue();
        }

        private void RecalculateValue()
        {
            Debug.Log("Number of modifiers: " + _modifiers.Count);
            _currentValue = BaseValue;
            float percentMultiplier = 1f;
            foreach (var mod in _modifiers.Where(m => m.Type == ModifierType.Flat))
            {
                _currentValue += mod.Value;
            }
            foreach (var mod in _modifiers.Where(m => m.Type == ModifierType.Percentage))
            {
                percentMultiplier *= 1f + mod.Value / 100f;
            }
            _currentValue *= percentMultiplier;
        }
    }
}
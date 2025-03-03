using System.Collections.Generic;
using _Game.DataStructures;
using _Game.Enums;

namespace _Game.StatSystem
{
    public class StatController
    {
        private Dictionary<StatType, Stat> _stats = new Dictionary<StatType, Stat>();
    
        public StatController(StatConfig statConfig)
        {
            foreach (var statData in statConfig.StatDataList)
            {
                _stats[statData.Type] = new Stat(statData.BaseValue);
            }
        }
    
        public float GetStatValue(StatType type)
        {
            return _stats.ContainsKey(type) ? _stats[type].GetValue() : 0;
        }
    
        public void AddStatModifier(StatType type, StatModifier modifier)
        {
            if (_stats.ContainsKey(type))
                _stats[type].AddModifier(modifier);
        }
    
        public void RemoveStatModifier(StatType type, StatModifier modifier)
        {
            if (_stats.ContainsKey(type))
                _stats[type].RemoveModifier(modifier);
        }
    }
}
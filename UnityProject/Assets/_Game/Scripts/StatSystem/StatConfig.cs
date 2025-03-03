using System;
using System.Collections.Generic;
using _Game.Enums;
using UnityEngine;

namespace _Game.StatSystem
{
    [CreateAssetMenu(fileName = "StatConfigCatalog", menuName = "StatSystem/Stat Confuguration Catalog", order = 0)]
    public class StatConfig : ScriptableObject
    {
        public List<StatData> StatDataList = new List<StatData>();
    }
    [Serializable]
    public class StatData
    {
        public StatType Type;
        public float BaseValue;
    }
}
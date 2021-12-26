using System.Collections.Generic;
using UnityEngine;

namespace Kira
{
    [CreateAssetMenu]
    public class Entity : ScriptableObject
    {
        public string entityName = "empty";
        public EntityStats entityStats;
        public Dictionary<StatType, Stat> stats = new();

        public Stat GetStat(StatType statType)
        {
            return stats[statType];
        }

        private void OnEnable()
        {
            stats.Clear();
            stats.Add(StatType.HEALTH, entityStats.health);
            stats.Add(StatType.MANA, entityStats.mana);
        }
    }
}
using System;

namespace Kira
{
    [Serializable]
    public class EntityStats
    {
        public Stat health = new Stat(50, StatType.HEALTH);
        public Stat mana = new Stat(100, StatType.MANA);
        public Stat speed = new Stat(10, StatType.SPEED);
    }
}
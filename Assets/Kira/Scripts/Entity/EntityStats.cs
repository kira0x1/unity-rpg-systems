using System;

namespace Kira
{
    [Serializable]
    public class EntityStats
    {
        public Stat health = new Stat(50, StatType.HEALTH);
        public Stat maxHealth = new Stat(50, StatType.MAXHEALTH);
        public Stat speed = new Stat(10, StatType.SPEED);
        public Stat maxSpeed = new Stat(10, StatType.MAXSPEED);
    }
}
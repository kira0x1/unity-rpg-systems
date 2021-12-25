using System;

namespace Kira
{
    [Serializable]
    public class Effect
    {
        public float value;
        public bool instant = true;
        public float effectTime;
        public float effectTick;
        public StatType effectsStat = StatType.HEALTH;

        public void OnEffect(Entity entity)
        {
            var stat = entity.GetStat(effectsStat);

            if (instant)
            {
                stat.Increase(value);
            }
        }
    }
}
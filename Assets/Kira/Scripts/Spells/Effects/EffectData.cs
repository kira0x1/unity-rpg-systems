using System;

namespace Kira
{
    [Serializable]
    public class EffectData
    {
        public float value;
        public bool instant = true;
        public float effectDuration;
        public float effectFrequency;
        public float effectTick;
        public StatType effectsStat = StatType.HEALTH;

        public EffectData(float value, bool instant, float effectDuration, float effectFrequency, float effectTick, StatType effectsStat = StatType.HEALTH)
        {
            this.value = value;
            this.instant = instant;
            this.effectDuration = effectDuration;
            this.effectFrequency = effectFrequency;
            this.effectTick = effectTick;
            this.effectsStat = effectsStat;
        }

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
using System;
using UnityEngine;

namespace Kira
{
    [Serializable]
    public class EffectData
    {
        public Guid randomId;
        public string name;
        public Sprite icon;
        public float value;
        public bool instant;
        public float effectDuration;
        public float effectFrequency;
        public float effectTick;
        public float effectTickTime;
        public StatType effectsStat;
        public bool canBeDispelled;

        public float timeLeft;

        public EffectData(string name, Sprite icon, float value, bool instant, float effectDuration, float effectFrequency, float effectTick, StatType effectsStat = StatType.HEALTH, bool canBeDispelled = true)
        {
            randomId = Guid.NewGuid();
            this.name = name;
            this.icon = icon;
            this.value = value;
            this.instant = instant;
            this.effectDuration = effectDuration;
            this.effectFrequency = effectFrequency;
            this.effectTick = effectTick;
            this.effectsStat = effectsStat;
            this.canBeDispelled = canBeDispelled;
            timeLeft = this.effectDuration;
        }

        public EffectData(Effect effect)
        {
            randomId = Guid.NewGuid();
            name = effect.name;
            icon = effect.icon;
            value = effect.value;
            instant = effect.instant;
            effectDuration = effect.effectDuration;
            effectFrequency = effect.effectFrequency;
            effectTick = effect.effectTick;
            effectsStat = effect.effectStat;
            canBeDispelled = effect.canBeDispelled;
            timeLeft = effectDuration;
        }

        public void OnEffect(Entity entity)
        {
            if (instant)
            {
                DealEffect(entity);
            }
        }

        public void DealEffect(Entity entity)
        {
            Stat stat = entity.GetStat(effectsStat);
            stat.Increase(value);
        }
    }
}
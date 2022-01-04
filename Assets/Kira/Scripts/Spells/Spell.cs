using System.Collections.Generic;
using UnityEngine;

namespace Kira
{
    public enum TargetType
    {
        NONE,
        SELF,
        FRIENDLY,
        HOSTILE
    }

    [CreateAssetMenu]
    public class Spell : ScriptableObject
    {
        public string spellName;
        public Sprite icon;
        public float resourceCost;
        public float coolDownTime;
        public float castTime;
        public bool requiresTarget = true;
        public bool canCastOnDead;

        public TargetType targetType = TargetType.SELF;

        public EffectData[] effectDatas;
        public Effect[] effects;

        public SpellData GetData()
        {
            List<EffectData> data = new();

            for (int i = 0; i < effectDatas.Length; i++)
            {
                data.Add(effectDatas[i]);
            }

            foreach (Effect effect in effects)
            {
                data.Add(effect.CreateEffectData());
            }

            return new SpellData(spellName, icon, resourceCost, coolDownTime, castTime, requiresTarget, data.ToArray(), canCastOnDead);
        }
    }

    public class SpellData
    {
        public string spellName;
        public Sprite icon;
        public float cost;
        public float curCD;
        public float cdTime;
        public float castTime;
        public EffectData[] effects;
        public bool requiresTarget;
        public bool canCastOnDead;

        public SpellData(string spellName, Sprite icon, float cost, float cdTime, float castTime, bool requiresTarget, EffectData[] effects, bool canCastOnDead)
        {
            this.spellName = spellName;
            this.icon = icon;
            this.cost = cost;
            this.cdTime = cdTime;
            this.castTime = castTime;
            this.requiresTarget = requiresTarget;
            this.effects = effects;
            this.canCastOnDead = canCastOnDead;
            curCD = 0;
        }

        public void OnCast(Entity entity)
        {
            if (cdTime <= 0) curCD = 0;
            else curCD = cdTime;

            foreach (EffectData e in effects)
            {
                var effectCopy = new EffectData(e.name, e.icon, e.value, e.instant, e.effectDuration, e.effectFrequency, e.effectTick, e.effectsStat);
                effectCopy.randomId = e.randomId;
                effectCopy.OnEffect(entity);

                if (effectCopy.effectDuration > 0)
                {
                    effectCopy.timeLeft = effectCopy.effectDuration;
                    entity.AddDebuff(effectCopy);
                }
            }
        }
    }
}
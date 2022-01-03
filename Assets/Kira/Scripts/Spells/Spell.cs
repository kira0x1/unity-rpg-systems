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
            EffectData[] data = new EffectData[effects.Length + effectDatas.Length];

            for (int i = 0; i < effectDatas.Length; i++)
            {
                data[i] = effectDatas[i];
            }

            for (int i = effectDatas.Length; i < effects.Length; i++)
            {
                var e = effects[i].CreateEffectData();
                data[i] = e;
            }

            return new SpellData(spellName, icon, resourceCost, coolDownTime, castTime, requiresTarget, data, canCastOnDead);
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
            Debug.Log($"Spell: {spellName} cast, setting curCD to {cdTime}");

            if (cdTime <= 0) curCD = 0;
            else curCD = cdTime;

            foreach (EffectData effect in effects)
            {
                effect.OnEffect(entity);
            }
        }
    }
}
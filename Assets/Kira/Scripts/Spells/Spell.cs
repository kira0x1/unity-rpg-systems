using UnityEngine;

namespace Kira
{
    [CreateAssetMenu]
    public class Spell : ScriptableObject
    {
        public string spellName;
        public Sprite icon;
        public float resourceCost;
        public float coolDownTime;
        public float castTime;
        public bool requiresTarget = true;

        public enum TargetType
        {
            NONE,
            SELF,
            FRIENDLY,
            HOSTILE
        }

        public TargetType targetType = TargetType.SELF;
        public Effect[] effects;

        public SpellData GetData()
        {
            return new SpellData(spellName, icon, resourceCost, coolDownTime, castTime, requiresTarget, effects);
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
        public Effect[] effects;
        public bool requiresTarget;

        public SpellData(string spellName, Sprite icon, float cost, float cdTime, float castTime, bool requiresTarget, Effect[] effects)
        {
            this.spellName = spellName;
            this.icon = icon;
            this.cost = cost;
            this.cdTime = cdTime;
            this.castTime = castTime;
            this.requiresTarget = requiresTarget;
            this.effects = effects;
            curCD = 0;
        }

        public void OnCast(Entity entity)
        {
            Debug.Log($"Spell: {spellName} cast, setting curCD to {cdTime}");
            curCD = cdTime;
            foreach (Effect effect in effects)
            {
                effect.OnEffect(entity);
            }
        }
    }
}
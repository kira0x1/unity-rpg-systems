using UnityEngine;

namespace Kira
{
    [CreateAssetMenu]
    public class Effect : ScriptableObject
    {
        public string effectName;
        public Sprite icon;
        public float value;
        public bool instant = true;
        public bool canBeDispelled = true;
        public float effectDuration;
        public float effectFrequency;
        public float effectTick;
        public StatType effectStat = StatType.HEALTH;

        // Create an instance of the effect
        public EffectData CreateEffectData()
        {
            return new EffectData(this);
        }
    }
}
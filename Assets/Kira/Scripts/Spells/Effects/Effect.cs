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
        public float effectDuration;
        public float effectFrequency;
        public float effectTick;
        public StatType effectStat = StatType.HEALTH;

        // Create an instance of the effect
        public EffectData CreateEffectData()
        {
            return new EffectData(effectName, icon, value, instant, effectDuration, effectFrequency, effectTick, effectStat);
        }
    }
}
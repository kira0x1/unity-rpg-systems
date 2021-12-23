using UnityEngine;

namespace Kira
{
    [CreateAssetMenu]
    public class Spell : ScriptableObject
    {
        public string spellName;
        public float resourceCost;
        public float coolDownTime;
        public float castTime;

        public SpellData GetData()
        {
            return new SpellData(spellName, resourceCost, coolDownTime, castTime);
        }
    }

    public struct SpellData
    {
        public string spellName;
        public float cost;
        public float cdTime;
        public float castTime;

        public SpellData(string spellName, float cost, float cdTime, float castTime)
        {
            this.spellName = spellName;
            this.cost = cost;
            this.cdTime = cdTime;
            this.castTime = castTime;
        }
    }
}
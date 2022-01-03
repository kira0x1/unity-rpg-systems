using UnityEngine;

namespace Kira
{
    public class EntityCharacter : MonoBehaviour
    {
        public Entity entity;
        public bool IsDead => entity.GetStat(StatType.HEALTH).value <= 0;

        [SerializeField]
        private bool _resetVitalsOnStart = true;

        private void Awake()
        {
            if (_resetVitalsOnStart)
            {
                var health = entity.GetStat(StatType.HEALTH);
                var mana = entity.GetStat(StatType.MANA);

                health.SetValue(health.max);
                mana.SetValue(mana.max);
            }
        }

        public Stat GetStat(StatType statType)
        {
            return entity.GetStat(statType);
        }
    }
}
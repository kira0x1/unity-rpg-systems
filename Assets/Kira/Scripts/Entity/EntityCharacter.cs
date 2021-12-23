using UnityEngine;

namespace Kira
{
    public class EntityCharacter : MonoBehaviour
    {
        public Entity entity;

        public Stat GetStat(StatType statType)
        {
            return entity.GetStat(statType);
        }
    }
}
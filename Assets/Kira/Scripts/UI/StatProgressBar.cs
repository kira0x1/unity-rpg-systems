using UnityEngine;

namespace Kira
{
    public class StatProgressBar : ProgressBar
    {
        [Header("Stat")]
        [SerializeField] private StatType statType = StatType.HEALTH;
        [SerializeField] private Entity entity;
        private Stat stat;

        private void Start()
        {
            stat = entity.GetStat(statType);
            stat.OnValueChanged += OnValueChanged;
            OnValueChanged();
        }

        private void OnDisable()
        {
            stat.OnValueChanged -= OnValueChanged;
        }

        private void OnValueChanged()
        {
            SetValues(stat.value, stat.max);
        }
    }
}
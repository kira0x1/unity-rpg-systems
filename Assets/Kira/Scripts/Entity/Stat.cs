using System;

namespace Kira
{
    [Serializable]
    public class Stat
    {
        public float value = 100;
        public float max = 100;
        public Action OnValueChanged;

        public Stat(float value, StatType statType)
        {
            this.value = value;
            this.StatType = statType;
        }

        public void SetValue(float value)
        {
            this.value = value;
            OnValueChanged?.Invoke();
        }

        public void SetMaxValue(float value)
        {
            max = value;
            OnValueChanged?.Invoke();
        }

        public void Reduce(float value)
        {
            this.value -= value;
            if (this.value < 0) this.value = 0;
            else if (this.value > max) this.value = max;

            OnValueChanged?.Invoke();
        }

        public void Increase(float value)
        {
            Reduce(-value);
        }

        public StatType StatType { get; private set; }
    }

    public enum StatType
    {
        MAXHEALTH,
        HEALTH,
        MAXSPEED,
        SPEED,
    }
}
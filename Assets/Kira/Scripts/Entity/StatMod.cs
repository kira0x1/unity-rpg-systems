using System;

namespace Kira
{
    [Serializable]
    public class StatMod
    {
        public float value;
        public bool isPercentage;

        public StatMod(float value, bool isPercentage = false)
        {
            this.value = value;
            this.isPercentage = isPercentage;
        }
    }
}
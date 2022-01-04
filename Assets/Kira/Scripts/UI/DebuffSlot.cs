using UnityEngine;
using UnityEngine.UI;

namespace Kira
{
    public class DebuffSlot : MonoBehaviour
    {
        public Image icon;
        public Image durationImage;
        public float duration;
        public EffectData effect;

        public void SetBuff(EffectData effect)
        {
            this.effect = effect;
            icon.sprite = effect.icon;
            duration = this.effect.effectDuration;
        }

        public void UpdateTick()
        {
            var timeLeft = effect.timeLeft;
            durationImage.fillAmount = timeLeft / duration;
        }

        public void RemoveBuff()
        {
            Destroy(gameObject);
        }
    }
}
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kira
{
    public class DebuffSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

        private void ShowToolTip()
        {
        }

        private void HideToolTip()
        {
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ShowToolTip();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HideToolTip();
        }
    }
}
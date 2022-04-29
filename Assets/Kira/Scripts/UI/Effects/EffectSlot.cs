using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kira
{
    public class EffectSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Image icon;
        public Image durationImage;
        public float duration;
        public EffectData effect;

        private bool showToolTip;

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

        private void Update()
        {
            if (!showToolTip) return;
            TooltipManager.Instance.GetTooltipUI().ShowEffectTooltip(effect);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            showToolTip = true;
            TooltipManager.Instance.GetTooltipUI().ShowEffectTooltip(effect);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            showToolTip = false;
            TooltipManager.Instance.GetTooltipUI().HideTooltip();
        }
    }
}
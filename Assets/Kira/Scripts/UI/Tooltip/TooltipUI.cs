using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Kira
{
    public class TooltipUI : MonoBehaviour
    {
        [Header("Tooltip Offset"), SerializeField]
        private float offsetX = 15;
        [SerializeField]
        private float offsetY = 50;

        [Header("Tooltip Components"), SerializeField]
        private TextMeshProUGUI headerText;

        [SerializeField]
        private TextMeshProUGUI descriptionText;

        [SerializeField]
        private Image icon;

        private bool tooltipEnabled;
        private Canvas _parentCanvas;
        private CanvasGroup _canvas;

        private void Awake()
        {
            _parentCanvas = GetComponentInParent<Canvas>();
            _canvas = GetComponent<CanvasGroup>();
            _canvas.blocksRaycasts = false;
            HideTooltip();
        }

        public void ShowEffectTooltip(EffectData effect)
        {
            icon.sprite = effect.icon;
            headerText.text = effect.name;
            descriptionText.text = $"Time: {effect.timeLeft:F1}";
            ShowTooltip();
        }

        private void Update()
        {
            if (!tooltipEnabled) return;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentCanvas.transform as RectTransform, Input.mousePosition, _parentCanvas.worldCamera, out Vector2 pointerPos);
            transform.position = _parentCanvas.transform.TransformPoint(pointerPos + new Vector2(offsetX, offsetY));
        }

        public void ShowTooltip()
        {
            tooltipEnabled = true;
            _canvas.alpha = 1f;
            _canvas.interactable = true;
        }

        public void HideTooltip()
        {
            _canvas.alpha = 0f;
            _canvas.interactable = false;
            tooltipEnabled = false;
        }
    }
}
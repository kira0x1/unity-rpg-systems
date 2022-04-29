using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Kira
{
    public class TooltipUI : MonoBehaviour
    {
        private CanvasGroup _canvas;

        [SerializeField]
        private TextMeshProUGUI headerText;

        [SerializeField]
        private TextMeshProUGUI descriptionText;

        [SerializeField]
        private Image icon;

        private void Awake()
        {
            _canvas.blocksRaycasts = false;
            HideTooltip();
        }

        public void ShowTooltip()
        {
            _canvas.alpha = 0f;
            _canvas.interactable = true;
        }

        public void HideTooltip()
        {
            _canvas.alpha = 0f;
            _canvas.interactable = false;
        }
    }
}
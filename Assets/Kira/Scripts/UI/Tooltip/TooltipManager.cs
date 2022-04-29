using UnityEngine;

namespace Kira
{
    public class TooltipManager : MonoBehaviour
    {
        public static TooltipManager Instance;
        private TooltipUI tooltipUI;

        private void Awake()
        {
            InitSingleton();
            tooltipUI = FindObjectOfType<TooltipUI>();
        }

        private void InitSingleton()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public TooltipUI GetTooltipUI()
        {
            return tooltipUI;
        }
    }
}
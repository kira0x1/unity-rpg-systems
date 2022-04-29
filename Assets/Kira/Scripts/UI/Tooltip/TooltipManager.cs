using UnityEngine;

namespace Kira
{
    public class TooltipManager : MonoBehaviour
    {
        public static TooltipManager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        // Should return a tooltip object that can be accessed by whoevers calling
        // public void ShowToolTip()
        // {
        // }
    }
}
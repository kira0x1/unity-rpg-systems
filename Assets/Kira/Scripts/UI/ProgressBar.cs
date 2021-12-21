using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Kira
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField]
        private Image _fill;
        [SerializeField]
        private Image _slowFill;
        [SerializeField]
        private TextMeshProUGUI _text;

        private float max = 100;
        private float cur = 100;
        private float slow = 100;

        private float t;
        
        public void SetValues(float cur, float max)
        {
            this.cur = cur;
            this.max = max;
            t = 0f;
        }

        public void Reduce(float amount)
        {
            cur -= amount;
            if (cur < 0) cur = 0;
            else if (cur > max) cur = max;
            t = 0f;
        }

        public void Increase(float amount)
        {
            Reduce(-amount);
        }

        private void Update()
        {
            if (Math.Abs(slow - cur) > 0.05f)
            {
                slow = Mathf.Lerp(slow, cur, t);
                t += 1.0f * Time.deltaTime;
            }

            _text.text = $"{cur:F0} / {max:F0}";
            _fill.fillAmount = cur / max;
            _slowFill.fillAmount = slow / max;
        }
    }
}
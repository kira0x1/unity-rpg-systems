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
        protected TextMeshProUGUI _text;

        protected bool _hasSlowBar;

        protected float max = 100;
        protected float cur = 100;
        protected float slow = 100;
        private float t;

        private void Awake()
        {
            _hasSlowBar = _slowFill != null;
        }

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

        protected virtual void Update()
        {
            UpdateText();
            if (_hasSlowBar) UpdateSlowBar();
            _fill.fillAmount = cur / max;
        }

        private void UpdateSlowBar()
        {
            if (Math.Abs(slow - cur) > 0.05f)
            {
                slow = Mathf.Lerp(slow, cur, t);
                t += 1.0f * Time.deltaTime;
            }

            _slowFill.fillAmount = slow / max;
        }

        protected virtual void UpdateText()
        {
            _text.text = ($"{cur:F0} / {max:F0}");
        }
    }
}
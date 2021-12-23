using System.Collections;
using UnityEngine;

namespace Kira
{
    public class CastBar : ProgressBar
    {
        [SerializeField]
        private GameObject _barParent;
        public bool BarEnabled { get; private set; }
        private CastJob job;

        private Coroutine jobCoroutine;
        private Coroutine updateCastCoroutine;

        public void SetSpell(CastJob job)
        {
            this.job = job;
            _barParent.SetActive(true);
            BarEnabled = true;
            jobCoroutine = StartCoroutine(job.StartCast());
            updateCastCoroutine = StartCoroutine(UpdateCast());
        }

        private IEnumerator UpdateCast()
        {
            while (job.curCastTime < job.targetCastTime)
            {
                SetValues(job.curCastTime, job.targetCastTime);
                yield return null;
            }

            HideBar();
        }

        private void HideBar()
        {
            _barParent.SetActive(false);
            BarEnabled = false;
        }

        protected override void UpdateText()
        {
            _text.text = $"{cur:F1} / {max:F0}";
        }

        public void CancelCast()
        {
            StopCoroutine(jobCoroutine);
            StopCoroutine(updateCastCoroutine);
            HideBar();
        }
    }
}
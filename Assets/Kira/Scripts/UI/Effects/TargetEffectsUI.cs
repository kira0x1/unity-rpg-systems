using System.Collections.Generic;
using UnityEngine;

namespace Kira
{
    public class TargetEffectsUI : EffectsUI
    {
        private TargetingManager targetingManager;
        private bool hasTarget;

        protected override void Start()
        {
            base.Start();
            targetingManager = TargetingManager.Instance;
            targetingManager.OnTarget += OnTarget;
            targetingManager.OnDeselectTarget += OnDeselectTarget;
        }

        private void OnDeselectTarget()
        {
            ClearSlots();
            hasTarget = false;
            debuffManager.OnEffect -= OnTargetEffect;
        }

        protected override void OnTargetEffect(EffectData e, bool isNewEffect = true)
        {
            base.OnTargetEffect(e, isNewEffect);
            OnTarget(targetingManager.Target);
        }

        private void OnTarget(Targetable target)
        {
            ClearSlots();
            debuffManager = target.debuffManager;
            SpawnSlots();
            hasTarget = true;
        }

        private void Update()
        {
            if (!hasTarget) return;
            UpdateSlots();
        }
    }
}
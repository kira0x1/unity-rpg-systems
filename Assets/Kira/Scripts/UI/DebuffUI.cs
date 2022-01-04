using System.Collections.Generic;
using UnityEngine;

namespace Kira
{
    public class DebuffUI : MonoBehaviour
    {
        public DebuffSlot slotPrefab;
        public Transform grid;
        public List<DebuffSlot> slots = new();

        private TargetingManager targetingManager;
        private Targetable target;
        private DebuffManager debuffManager;
        private bool hasTarget;
        private List<DebuffSlot> slotsToRemove = new();

        private void Start()
        {
            slots.Clear();
            targetingManager = TargetingManager.Instance;
            targetingManager.OnTarget += OnTarget;
            targetingManager.OnDeselectTarget += OnDeselectTarget;
        }

        private void ClearSlots()
        {
            foreach (var slot in slots)
            {
                Destroy(slot.gameObject);
            }

            slots.Clear();
        }

        private void OnDeselectTarget()
        {
            ClearSlots();
            hasTarget = false;
            target.debuffManager.OnEffect -= OnTargetEffect;
        }

        private void OnTarget(Targetable target)
        {
            ClearSlots();

            this.target = target;
            debuffManager = target.debuffManager;

            foreach (var effect in debuffManager._effects)
            {
                var slot = Instantiate(slotPrefab, grid);
                slot.SetBuff(effect);
                slots.Add(slot);
            }

            target.debuffManager.OnEffect += OnTargetEffect;
            hasTarget = true;
        }

        private void OnTargetEffect(EffectData e, bool isNewEffect = true)
        {
            // if (!isNewEffect) return;
            // var slot = Instantiate(slotPrefab, grid);
            // slot.SetBuff(e);
            OnTarget(target);
        }

        private void Update()
        {
            if (!hasTarget) return;
            slotsToRemove.Clear();

            foreach (DebuffSlot slot in slots)
            {
                slot.UpdateTick();
                if (slot.effect.timeLeft <= 0)
                {
                    slotsToRemove.Add(slot);
                }
            }

            foreach (var s in slotsToRemove)
            {
                slots.Remove(s);
                Destroy(s.gameObject);
            }
        }
    }
}
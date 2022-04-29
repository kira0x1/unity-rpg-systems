using System.Collections.Generic;
using UnityEngine;

namespace Kira
{
    public abstract class EffectsUI : MonoBehaviour
    {
        public EffectSlot slotPrefab;
        public Transform grid;
        public List<EffectSlot> slots = new();

        protected DebuffManager debuffManager;
        protected List<EffectSlot> slotsToRemove = new();

        protected virtual void Start()
        {
            slots.Clear();
        }

        protected void ClearSlots()
        {
            foreach (var slot in slots)
            {
                Destroy(slot.gameObject);
            }

            slots.Clear();
        }

        protected void SpawnSlots()
        {
            ClearSlots();

            foreach (var effect in debuffManager._effects)
            {
                var slot = Instantiate(slotPrefab, grid);
                slot.SetBuff(effect);
                slots.Add(slot);
            }

            debuffManager.OnEffect += OnTargetEffect;
        }

        protected virtual void OnTargetEffect(EffectData e, bool isNewEffect = true)
        {
            // if (!isNewEffect) return;
            // var slot = Instantiate(slotPrefab, grid);
            // slot.SetBuff(e);
        }

        protected void UpdateSlots()
        {
            slotsToRemove.Clear();

            foreach (EffectSlot slot in slots)
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
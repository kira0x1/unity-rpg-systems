using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Kira
{
    public class DebuffManager : MonoBehaviour
    {
        public Entity entity;
        public Action<EffectData, bool> OnEffect;

        [FormerlySerializedAs("_effects")]
        public List<EffectData> effects = new List<EffectData>();

        private List<EffectData> _effectsToRemove = new List<EffectData>();

        private void Awake()
        {
            entity = GetComponent<Targetable>().entity;
            entity.OnEffected += AddDebuff;
        }

        private void OnDisable()
        {
            if (entity)
            {
                entity.OnEffected -= AddDebuff;
            }
        }

        public void AddDebuff(EffectData effect)
        {
            bool hasEffect = false;

            foreach (EffectData e in effects)
            {
                if (e.randomId == effect.randomId) hasEffect = true;
                e.timeLeft = e.effectDuration;
                e.effectTickTime = 0;
                break;
            }

            if (!hasEffect)
            {
                effects.Add(effect);
            }

            OnEffect?.Invoke(effect, hasEffect);
        }

        private void Update()
        {
            _effectsToRemove.Clear();

            foreach (EffectData effect in effects)
            {
                effect.timeLeft -= Time.deltaTime;
                effect.effectTickTime += Time.deltaTime;

                if (effect.effectTickTime >= effect.effectFrequency)
                {
                    effect.DealEffect(entity);
                    effect.effectTickTime = 0;
                }

                if (effect.timeLeft < 0)
                {
                    effect.timeLeft = 0;
                    _effectsToRemove.Add(effect);
                }
            }

            // Remove all effects queued to be removed
            foreach (var e in _effectsToRemove)
            {
                effects.Remove(e);
            }
        }
    }
}
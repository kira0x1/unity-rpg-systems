using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kira
{
    public class DebuffManager : MonoBehaviour
    {
        public List<EffectData> _effects = new();
        public Entity entity;
        public Action<EffectData, bool> OnEffect;
        private List<EffectData> effectsToRemove = new();

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

            foreach (EffectData e in _effects)
            {
                if (e.randomId == effect.randomId) hasEffect = true;
                e.timeLeft = e.effectDuration;
                e.effectTickTime = 0;
                break;
            }

            if (!hasEffect)
            {
                _effects.Add(effect);
            }

            OnEffect?.Invoke(effect, hasEffect);
        }

        private void Update()
        {
            effectsToRemove.Clear();

            foreach (EffectData effect in _effects)
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
                    effectsToRemove.Add(effect);
                }
            }

            foreach (var e in effectsToRemove)
            {
                _effects.Remove(e);
            }
        }
    }
}
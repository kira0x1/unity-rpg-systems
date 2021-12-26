using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace Kira
{
    public class CastJob
    {
        public Action<SpellData, Entity> OnSpellDoneCast;
        public SpellData spellData;
        public float curCastTime;
        public float targetCastTime;
        public Entity target;

        public CastJob(SpellData spellData, [CanBeNull] Entity target = null)
        {
            this.spellData = spellData;
            targetCastTime = spellData.castTime;
            curCastTime = 0f;
            this.target = target;
        }

        public IEnumerator StartCast()
        {
            while (curCastTime < targetCastTime)
            {
                curCastTime += Time.deltaTime;
                yield return null;
            }

            curCastTime = targetCastTime;
            OnSpellDoneCast?.Invoke(spellData, target);
        }
    }
}
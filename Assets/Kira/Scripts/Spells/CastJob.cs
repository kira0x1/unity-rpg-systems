using System;
using System.Collections;
using UnityEngine;

namespace Kira
{
    public class CastJob
    {
        public Action<SpellData> OnSpellDoneCast;
        public SpellData spellData;
        public float curCastTime;
        public float targetCastTime;

        public CastJob(SpellData spellData)
        {
            this.spellData = spellData;
            targetCastTime = spellData.castTime;
            curCastTime = 0f;
        }

        public IEnumerator StartCast()
        {
            while (curCastTime < targetCastTime)
            {
                curCastTime += Time.deltaTime;
                yield return null;
            }

            curCastTime = targetCastTime;
            OnSpellDoneCast?.Invoke(spellData);
        }
    }
}
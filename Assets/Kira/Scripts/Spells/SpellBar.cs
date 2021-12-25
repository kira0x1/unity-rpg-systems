using System.Collections.Generic;
using UnityEngine;

namespace Kira
{
    public class SpellBar : MonoBehaviour
    {
        public List<Spell> spells = new List<Spell>();
        // private SpellData[] spellDatas;
        public SpellSlot[] spellSlots = new SpellSlot[5];
        private EntityCaster entityCaster;

        private void Awake()
        {
            entityCaster = FindObjectOfType<EntityCaster>();
            // spellDatas = new SpellData[spells.Count];

            for (int i = 0; i < spellSlots.Length && i < spells.Count; i++)
            {
                Spell spell = spells[i];
                SpellData spellData = spell.GetData();
                // spellDatas[i] = spellData;
                // Debug.Log("Spell " + i + ": " + spell.spellName);
                var slot = spellSlots[i];
                slot.SetSpell(spellData);
                slot.OnSpellCast += OnSpellCast;
            }
        }

        private void OnSpellCast(SpellData spell)
        {
            entityCaster.CastSpell(spell);
        }
    }
}
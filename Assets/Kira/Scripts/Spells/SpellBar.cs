using System.Collections.Generic;
using UnityEngine;

namespace Kira
{
    public class SpellBar : MonoBehaviour
    {
        public List<Spell> spells = new List<Spell>();
        public SpellSlot[] spellSlots = new SpellSlot[5];
        private EntityCaster entityCaster;

        private void Awake()
        {
            entityCaster = FindObjectOfType<EntityCaster>();

            for (int i = 0; i < spellSlots.Length && i < spells.Count; i++)
            {
                Spell spell = spells[i];
                SpellData spellData = spell.GetData();
                SpellSlot slot = spellSlots[i];

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
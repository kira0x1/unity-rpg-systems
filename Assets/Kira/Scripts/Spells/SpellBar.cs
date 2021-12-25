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
                Debug.Log("Spell " + i + ": " + spell.spellName);
                var slot = spellSlots[i];
                slot.SetSpell(spell);
                slot.OnSpellCast += OnSpellCast;
            }
        }

        private void OnSpellCast(Spell spell)
        {
            entityCaster.CastSpell(spell);
        }
    }
}
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Kira
{
    public class SpellSlot : MonoBehaviour
    {
        public Spell spell;
        public Image spellIcon;
        public TextMeshProUGUI keyText;
        public KeyCode slotKeybind;
        public Action<Spell> OnSpellCast;

        private void Start()
        {
            if (slotKeybind != KeyCode.None) SetKey(slotKeybind);
            else SetKey(KeyCode.Alpha1);

            if (spell != null)
                SetSpell(spell);
        }

        private void Update()
        {
            if (Input.GetKeyDown(slotKeybind))
            {
                OnClick();
            }
        }

        public void OnClick()
        {
            OnSpellCast?.Invoke(spell);
        }

        public void SetKey(KeyCode key)
        {
            slotKeybind = key;
            keyText.text = key.ToString().Replace("Alpha", "");
        }

        public void SetSpell(Spell spell)
        {
            this.spell = spell;
            spellIcon.enabled = true;
            spellIcon.sprite = spell.icon;
        }
    }
}
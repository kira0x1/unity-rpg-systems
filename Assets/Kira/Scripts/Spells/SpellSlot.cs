using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Kira
{
    public class SpellSlot : MonoBehaviour
    {
        public SpellData spell;
        public Image spellIcon;
        public TextMeshProUGUI keyText;
        public KeyCode slotKeybind;
        public Action<SpellData> OnSpellCast;

        [SerializeField] private Image _cdImage;

        private float cdTime;
        private float curCD;
        private bool hasSpell;

        private void Start()
        {
            if (slotKeybind != KeyCode.None) SetKey(slotKeybind);
            else SetKey(KeyCode.Alpha1);
            SetSpell(spell);
        }

        private void Update()
        {
            if (!hasSpell) return;

            if (Input.GetKeyDown(slotKeybind))
            {
                OnClick();
            }

            if (spell.curCD > 0)
            {
                spell.curCD -= Time.deltaTime;
            }
            else
            {
                spell.curCD = 0;
            }

            _cdImage.fillAmount = spell.curCD / spell.cdTime;
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

        public void SetSpell(SpellData spell)
        {
            if (spell == null)
                return;

            this.spell = spell;
            spellIcon.enabled = true;
            spellIcon.sprite = spell.icon;
            hasSpell = true;
        }
    }
}
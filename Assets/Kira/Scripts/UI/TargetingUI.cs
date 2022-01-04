using TMPro;
using UnityEngine;

namespace Kira
{
    public class TargetingUI : MonoBehaviour
    {
        public CanvasGroup uiParent;

        [SerializeField]
        private TextMeshProUGUI _targetNameText;

        [SerializeField]
        private StatProgressBar _healthBar;

        [SerializeField]
        private StatProgressBar _manaBar;

        private Entity _entity;
        private EntityStats _stats;
        private Stat _health;
        private Stat _mana;

        public void SetTarget(Targetable target)
        {
            _entity = target.entity;
            _stats = _entity.entityStats;
            _health = _stats.health;
            _mana = _stats.mana;

            _healthBar.SetValues(_health.value, _health.max, true);
            _manaBar.SetValues(_mana.value, _mana.max, true);

            _targetNameText.text = _entity.entityName;

            ShowUI();

            _stats.health.OnValueChanged += OnHealthChanged;
            _stats.mana.OnValueChanged += OnManaChanged;
        }

        public void Deselect()
        {
            HideUI();
            _stats.health.OnValueChanged -= OnHealthChanged;
            _stats.mana.OnValueChanged -= OnManaChanged;
        }

        private void OnHealthChanged()
        {
            _healthBar.SetValues(_health.value, _health.max);
        }

        private void OnManaChanged()
        {
            _manaBar.SetValues(_mana.value, _mana.max);
        }

        private void ShowUI()
        {
            uiParent.blocksRaycasts = true;
            uiParent.interactable = true;
            uiParent.alpha = 1f;
        }

        private void HideUI()
        {
            uiParent.blocksRaycasts = false;
            uiParent.interactable = false;
            uiParent.alpha = 0f;
        }
    }
}
using UnityEngine;

namespace Kira
{
    public class TargetingUI : MonoBehaviour
    {
        public GameObject uiParent;

        [SerializeField]
        private StatProgressBar _healthBar;

        [SerializeField]
        private StatProgressBar _manaBar;

        private bool _hasTarget;

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
            _hasTarget = true;
            uiParent.SetActive(true);

            _healthBar.SetValues(_health.value, _health.max);
            _manaBar.SetValues(_mana.value, _mana.max);

            _stats.health.OnValueChanged += OnHealthChanged;
            _stats.mana.OnValueChanged += OnManaChanged;
        }

        public void Deselect()
        {
            _hasTarget = false;
            uiParent.SetActive(false);

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
    }
}
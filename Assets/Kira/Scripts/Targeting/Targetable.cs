using UnityEngine;

namespace Kira
{
    [SelectionBase]
    [RequireComponent(typeof(DebuffManager))]
    public class Targetable : MonoBehaviour
    {
        public Entity entity;
        [SerializeField] private GameObject _highlight;
        [SerializeField] private GameObject _selected;
        private bool _isSelected;

        public DebuffManager debuffManager;

        [SerializeField]
        private bool _healOnStart = true;

        public bool IsDead => entity.GetStat(StatType.HEALTH).value <= 0;

        private void Awake()
        {
            debuffManager = GetComponent<DebuffManager>();
        }

        private void Start()
        {
            if (_healOnStart)
            {
                var health = entity.GetStat(StatType.HEALTH);
                health.SetValue(health.max);
            }
        }

        public void SetHover(bool on = true)
        {
            // _highlight.SetActive(on);
        }

        public void SetSelected(bool selected)
        {
            _isSelected = selected;
            _selected.SetActive(selected);
        }
    }
}
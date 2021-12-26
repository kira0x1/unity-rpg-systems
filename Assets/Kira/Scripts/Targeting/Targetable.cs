using UnityEngine;

namespace Kira
{
    [SelectionBase]
    public class Targetable : MonoBehaviour
    {
        public Entity entity;
        [SerializeField] private GameObject _highlight;
        [SerializeField] private GameObject _selected;
        private bool _isSelected;

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
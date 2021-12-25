using UnityEngine;

namespace Kira
{
    public class TargetingManager : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _targetableLayer;
        private GameObject _curTarget;
        private Camera cam;
        private Targetable _prevTarget;

        private bool _hasPreviousTarget;

        private bool _hasSelected;
        private Targetable _previousSelected;

        [SerializeField] private float clickCoolDown = 0.1f;
        private float _nextClickTime;
        private bool _hoveringOnTarget;

        private TargetingUI targetingUI;

        private void Awake()
        {
            cam = Camera.main;
            targetingUI = FindObjectOfType<TargetingUI>();
        }

        private void Update()
        {
            HandleTargeting();
            HandleClicking();
        }

        private void HandleClicking()
        {
            if (Input.GetMouseButton(0) && Time.time >= _nextClickTime)
            {
                OnClick();
            }
        }

        private void OnClick()
        {
            _nextClickTime = Time.time + clickCoolDown;

            if (_hasSelected)
            {
                _previousSelected.SetSelected(false);
                _hasSelected = false;
                targetingUI.Deselect();
            }

            if (_hoveringOnTarget)
            {
                _previousSelected = _prevTarget;
                _previousSelected.SetSelected(true);
                _hasSelected = true;
                targetingUI.SetTarget(_previousSelected);
            }
        }

        private void HandleTargeting()
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, _targetableLayer))
            {
                Targetable target = hit.collider.GetComponent<Targetable>();

                if (_hasPreviousTarget)
                {
                    _prevTarget.SetHover(false);
                }

                _hoveringOnTarget = true;
                target.SetHover(true);
                _prevTarget = target;
                _hasPreviousTarget = true;
            }
            else if (_prevTarget)
            {
                _hoveringOnTarget = false;
                _prevTarget.SetHover(false);
                _hasPreviousTarget = false;
            }
        }
    }
}
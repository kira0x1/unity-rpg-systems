using System;
using UnityEngine;

namespace Kira
{
    public class TargetingManager : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _targetableLayer;
        private Targetable _curTarget;
        private Camera cam;

        private Targetable _prevTarget;
        private bool _hasPreviousTarget;
        private Targetable _previousSelected;

        [SerializeField]
        private float clickCoolDown = 0.1f;

        private float _nextClickTime;
        private bool _hoveringOnTarget;

        private TargetingUI targetingUI;

        public static bool HasTarget { get; private set; }
        public static TargetingManager Instance { get; private set; }

        public Targetable Target => _curTarget;

        public Action OnDeselectTarget;
        public Action<Targetable> OnTarget;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            HasTarget = false;

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

            if (HasTarget)
            {
                // if (_previousSelected.GetInstanceID() == _prevTarget.GetInstanceID()) return;

                _previousSelected.SetSelected(false);
                HasTarget = false;
                targetingUI.Deselect();
                OnDeselectTarget?.Invoke();
            }

            if (_hoveringOnTarget)
            {
                _previousSelected = _prevTarget;
                _previousSelected.SetSelected(true);
                HasTarget = true;
                targetingUI.SetTarget(_previousSelected);
                _curTarget = _previousSelected;
                OnTarget?.Invoke(_curTarget);
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
using CMF;
using UnityEngine;

namespace Kira
{
    public class PlayerMovement : EntityController
    {
        private Controller _controller;

        private void Awake()
        {
            _controller = GetComponentInParent<Controller>();
        }

        public override bool IsGrounded => _controller.IsGrounded();
        public override Vector3 GetVelocity => _controller.GetVelocity();
        public override Vector3 GetMovementVelocity => _controller.GetMovementVelocity();
        public override bool IsMoving => _controller.GetMovementVelocity().sqrMagnitude > 0.0f;
    }
}
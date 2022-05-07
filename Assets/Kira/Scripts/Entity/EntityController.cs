using UnityEngine;

namespace Kira
{
    public abstract class EntityController : MonoBehaviour
    {
        public abstract bool IsGrounded { get; }
        public abstract Vector3 GetVelocity { get; }
        public abstract Vector3 GetMovementVelocity { get; }
        public abstract bool IsMoving { get; }
    }
}
using CMF;
using UnityEngine;

namespace Kira
{
    public class PlayerCameraExtended : CameraController
    {
        public static bool IsTurningCamera;

        private enum MouseButtons
        {
            LEFT = 0,
            RIGHT = 1
        }

        [SerializeField]
        private MouseButtons turnCameraBtn = MouseButtons.RIGHT;

        protected override void HandleCameraRotation()
        {
            if (Input.GetMouseButton((int)turnCameraBtn))
            {
                IsTurningCamera = true;
                base.HandleCameraRotation();
            }
            else
            {
                IsTurningCamera = false;
            }
        }
    }
}
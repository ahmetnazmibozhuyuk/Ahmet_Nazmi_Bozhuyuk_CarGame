using UnityEngine;

namespace CarGame.Control
{
    public abstract class InputAbstract : MonoBehaviour
    {
        protected InputState leftInput;
        protected InputState rightInput;

        protected virtual void Update()
        {
            MouseInput();
            KeyboardInput();
        }
        private void MouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Input.mousePosition.x < Screen.width / 2)
                {
                    leftInput = InputState.InputDown;
                }
                else if (Input.mousePosition.x > Screen.width / 2)
                {
                    rightInput = InputState.InputDown;
                }
            }
            else if (Input.GetMouseButton(0))
            {
                if (Input.mousePosition.x < Screen.width / 2)
                {
                    leftInput = InputState.InputActive;
                }
                else if (Input.mousePosition.x > Screen.width / 2)
                {
                    rightInput = InputState.InputActive;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if(leftInput == InputState.InputActive)
                {
                    leftInput = InputState.InputUp;
                    rightInput = InputState.InputUp;
                }
                else
                {
                    leftInput = InputState.InputInactive;
                    rightInput = InputState.InputInactive;
                }

            }
        }
        private void KeyboardInput()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                leftInput = InputState.InputDown;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                leftInput = InputState.InputActive;
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                if (leftInput == InputState.InputActive)
                {
                    leftInput = InputState.InputUp;
                }
                else
                {
                    leftInput = InputState.InputInactive;
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                rightInput = InputState.InputDown;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rightInput = InputState.InputActive;
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                if (rightInput == InputState.InputActive)
                {
                    rightInput = InputState.InputUp;
                }
                else
                {
                    rightInput = InputState.InputInactive;
                }
            }

        }
    }
}

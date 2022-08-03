using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class InputManager : MonoBehaviour
    {
        public delegate void Move(Vector2 movementDelta);
        public Move Moving;

        public Joystick JoystickController;

        private void Update()
        {
            if (JoystickController.Direction != Vector2.zero)
                Moving?.Invoke(JoystickController.Direction);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerMoverManager : MonoBehaviour
    {
        public MovingProperty MovingInfo;
        public Transform PlayerTransform;

        private Vector3 preferrablePosition;
        private Vector3 refPreferrablePosition;


        public void Init(InputManager manager)
        {
            preferrablePosition = PlayerTransform.position;

            manager.Moving += movingReceived;
        }

        private void Update()
        {
            PlayerTransform.position = Vector3.SmoothDamp(PlayerTransform.position, preferrablePosition, ref refPreferrablePosition, 0.2f);
        }

        private void movingReceived(Vector2 moveDelta)
        {
            Vector3 deltaForMoving = new Vector3(moveDelta.x, 0f, moveDelta.y) * MovingInfo.Sensivity;
            Vector3 newPosition = PlayerTransform.position + deltaForMoving;

            newPosition.x = Mathf.Clamp(newPosition.x, MovingInfo.BoundsMinCoordinates.x, MovingInfo.BoundsMaxCoordinates.x);
            newPosition.z = Mathf.Clamp(newPosition.z, MovingInfo.BoundsMinCoordinates.z, MovingInfo.BoundsMaxCoordinates.z);

            PlayerTransform.LookAt(newPosition);
            preferrablePosition = newPosition;
        }
    }
}

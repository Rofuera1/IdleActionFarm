using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class CollectedGrass : MonoBehaviour
    {
        private Vector3 refPosition;
        private Vector3 localNeededPosition;
        private float delayFollowPosition;

        public void Init(Vector3 localPosition, float delayTime)
        {
            localNeededPosition = localPosition;
            delayFollowPosition = delayTime;
        }

        private void Update()
        {

        }
    }
}

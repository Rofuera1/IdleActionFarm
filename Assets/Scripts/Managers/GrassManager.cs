using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GrassManager : MonoBehaviour
    {
        private float timeToResetGrass;
        private Grass[] grassesAtScene;

        public Transform GrassHandlerParent;

        public void Init(float TimeToResetGrass)
        {
            timeToResetGrass = TimeToResetGrass;

            grassesAtScene = GrassHandlerParent.GetComponentsInChildren<Grass>();
            foreach (Grass grass in grassesAtScene)
                grass.CreateFullGrass();
        }
    }
}

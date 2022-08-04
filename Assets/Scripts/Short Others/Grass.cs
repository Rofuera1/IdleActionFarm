using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Grass : MonoBehaviour
    {
        [SerializeField] private GameObject FullGrassPrafab;
        [SerializeField] private BoxCollider TriggerCollider;

        public bool isAnyLeft { get => GrassGameobjectForCutting!= null; }
        public GameObject GrassGameobjectForCutting { get; private set; }
        private GameObject GrassLeftAfterCutting;

        public delegate void GrassDestroyedCallback(Grass grass);
        public GrassDestroyedCallback GrassDestroyed;

        public void CreateFullGrass()
        {
            if (GrassLeftAfterCutting) Destroy(GrassLeftAfterCutting);
            GrassGameobjectForCutting = Instantiate(FullGrassPrafab, transform);
        }

        public void GrassHaveBeenCuttedFull(GameObject grassLeft)
        {
            GrassLeftAfterCutting = grassLeft;
            GrassGameobjectForCutting = null;

            GrassDestroyed?.Invoke(this);
        }
    }
}
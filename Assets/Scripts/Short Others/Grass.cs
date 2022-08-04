using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Grass : MonoBehaviour
    {
        [SerializeField] private GameObject FullGrassPrafab;
        [SerializeField] private BoxCollider TriggerCollider;

        public bool isAnyLeft => GrassGameobjectForCutting;
        public GameObject GrassGameobjectForCutting { get; private set; }
        private GameObject GrassLeftAfterCutting;

        public void CreateFullGrass()
        {
            if (GrassLeftAfterCutting) Destroy(GrassLeftAfterCutting);
            GrassGameobjectForCutting = Instantiate(FullGrassPrafab, transform);
            TriggerCollider.enabled = true;
        }

        public void GrassHaveBeenCuttedFull(GameObject grassLeft)
        {
            GrassLeftAfterCutting = grassLeft;
            GrassGameobjectForCutting = null;
            TriggerCollider.enabled = false;
        }
    }
}

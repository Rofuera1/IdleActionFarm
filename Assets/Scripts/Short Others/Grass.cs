using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Grass : MonoBehaviour
    {
        [SerializeField] private GameObject FullGrassPrafab;

        private GameObject grassLeftover;
        public bool isAnyLeft => grassLeftover;
        public bool isGrassFull { get; private set; }
        public GameObject GrassGameobjectForCutting => grassLeftover;

        public void CreateFullGrass()
        {
            if (grassLeftover) Destroy(grassLeftover);
            grassLeftover = Instantiate(FullGrassPrafab, transform);
            isGrassFull = true;
        }

        public void GrassHaveBeenCuttedHalf(GameObject objectToLeave)
        {
            grassLeftover = objectToLeave;
            grassLeftover.AddComponent<MeshCollider>().convex = true;
            isGrassFull = false;
        }

        public void GrassHaveBeenCuttedFull()
        {
            grassLeftover = null;
            isGrassFull = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GrassManager : MonoBehaviour
    {
        protected static GrassManager Instance;

        private float timeToResetGrass;
        private Grass[] grassesAtScene;

        public Transform GrassHandlerParent;
        public GameObject GrassCutEffectPrefab;
        public Material MaterialForCollectableCutGrass;
        public Material MaterialForLeftOnGroundCutGrass;

        public void Init(float TimeToResetGrass)
        {
            Instance = this;
            timeToResetGrass = TimeToResetGrass;

            grassesAtScene = GrassHandlerParent.GetComponentsInChildren<Grass>();
            foreach (Grass grass in grassesAtScene)
            {
                grass.CreateFullGrass();
                grass.GrassDestroyed += grassHaveBeenDestroyed;
            }
        }

        public static void AssignMaterialsToCuttedGrass(MeshRenderer grassCuttedDropped, MeshRenderer grassCuttedToPickUp)
        {
            grassCuttedDropped.material = Instance.MaterialForLeftOnGroundCutGrass;
            grassCuttedToPickUp.material = Instance.MaterialForCollectableCutGrass;
        }

        private void grassHaveBeenDestroyed(Grass grass)
        {
            StartCoroutine(rebuildGrass(grass));
            Instantiate(GrassCutEffectPrefab, grass.transform.position, Quaternion.identity);
        }

        private IEnumerator rebuildGrass(Grass grassToRebuild)
        {
            yield return new WaitForSeconds(timeToResetGrass);
            grassToRebuild.CreateFullGrass();
        }
    }
}

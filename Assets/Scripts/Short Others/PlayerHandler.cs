using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerHandler : MonoBehaviour
    {
        public Transform PlaceGrassTR;
        public GameObject GrassToPlaceBehindPrefab;
        private PlayerInfoReceiver playerManager;
        [Space]
        public Scythe ScytheScript;
        private Grass _grassCollidedWith;

        private Vector3 grassPrefabSize;
        private Stack<GameObject> placedGrassStack;

        public void Init(int maxGrassCapacity, PlayerInfoReceiver PlayerManager)
        {
            placedGrassStack = new Stack<GameObject>(maxGrassCapacity);
            grassPrefabSize = GrassToPlaceBehindPrefab.GetComponent<BoxCollider>().size;
            Debug.Log("GRASS_BEHIND_SIZE_IS_" + grassPrefabSize);
            playerManager = PlayerManager;
            ScytheScript.Init(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Grass>())
                playerManager.PlayerCollidedWithFullGrass(other.GetComponent<Grass>());
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "CuttedGrassAtGround")
                playerManager.PlayerCollidedWithCuttedGrass(collision.gameObject);
        }

        public void ActivateScythe(Grass grassToCut, bool cutAtHalfNotFull)
        {
            ScytheScript.Cut(grassToCut);
        }

        public void GrassCuttedWithResult(Grass grassCutted, GameObject grassAtGround, GameObject grassToCollect)
        {
            if (grassCutted.isAnyLeft) grassCutted.GrassHaveBeenCuttedFull();
            else grassCutted.GrassHaveBeenCuttedHalf(grassAtGround);

            playerManager.PlayerCuttedGrass(grassToCollect);
        }

        public Vector3 AddSingleGrassToPlayer(int grassNumber)
        {
            GameObject newGrass = Instantiate(GrassToPlaceBehindPrefab, PlaceGrassTR);
            Vector3 newGrassLocalPosition = new Vector3(0f, grassPrefabSize.x * grassNumber * 0.5f, 0f);

            newGrass.transform.parent = PlaceGrassTR;
            newGrass.transform.localPosition = newGrassLocalPosition;
            placedGrassStack.Push(newGrass);

            return newGrassLocalPosition;
        }

        public GameObject RemoveGrassFromTopAtPlayer()
        {
            return placedGrassStack.Pop();
        }
    }
}

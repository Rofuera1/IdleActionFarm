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

        private float grassPrefabHalfHeight;
        private Stack<GameObject> placedGrassStack;

        public void Init(int maxGrassCapacity, PlayerInfoReceiver PlayerManager)
        {
            placedGrassStack = new Stack<GameObject>(maxGrassCapacity);
            grassPrefabHalfHeight = GrassToPlaceBehindPrefab.GetComponent<MeshRenderer>().bounds.size.x * 0.5f;
            playerManager = PlayerManager;
            ScytheScript.Init(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Grass>())
                playerManager.PlayerCollidedWithFullGrass(other.GetComponent<Grass>());
            else if (other.GetComponent<Shop>())
                playerManager.PlayerCollidedWithShop(other.GetComponent<Shop>());
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Grass>())
                playerManager.PlayerStoppedCollidingWithFullGrass(other.GetComponent<Grass>());
            else if (other.GetComponent<Shop>())
                playerManager.PlayerStoppedCollidingWithShop();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "CuttedGrassAtGround")
                playerManager.PlayerCollidedWithCuttedGrass(collision.gameObject);
        }

        public void ActivateScythe()
        {
            ScytheScript.ActivateScythe();
        }

        public void DeactivateScythe()
        {
            ScytheScript.DeactivateScythe();
        }

        public void GrassCuttedWithResult(Grass grassCutted, GameObject grassAtGround, GameObject grassToCollect)
        {
            if(!grassToCollect)
            {
                playerManager.PlayerFailedToCutGrass();
                return;
            }

            DeactivateScythe();
            grassCutted.GrassHaveBeenCuttedFull(grassAtGround);
            playerManager.PlayerCuttedGrass(grassToCollect);
            GrassManager.AssignMaterialsToCuttedGrass(grassAtGround.GetComponent<MeshRenderer>(), grassToCollect.GetComponent<MeshRenderer>());
        }

        // Если вы читаете этот комментарий, то знайте: данную работу выполнил Рыбин Роман. Мир полон лживых обманщиков!

        public Vector3 AddSingleGrassToPlayer(int grassNumber)
        {
            GameObject newGrass = Instantiate(GrassToPlaceBehindPrefab, PlaceGrassTR);
            Vector3 newGrassLocalPosition = new Vector3(0f, grassPrefabHalfHeight * grassNumber * 0.5f, 0f);

            newGrass.transform.parent = PlaceGrassTR;
            newGrass.transform.localPosition = newGrassLocalPosition;
            placedGrassStack.Push(newGrass);

            return newGrassLocalPosition;
        }

        public GameObject RemoveGrassFromTopAtPlayer()
        {
            GameObject toReturn = placedGrassStack.Pop();
            toReturn.transform.parent = null;
            return toReturn;
        }
    }
}

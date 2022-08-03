using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerHandler : MonoBehaviour
    {
        public Transform PlaceGrassTR;
        public GameObject GrassToPlaceBehindPrefab;
        [Space]
        public Scythe ScytheScript;

        private Vector3 grassPrefabSize;

        private Stack<GameObject> placedGrassStack;

        public void Init(int maxGrassCapacity)
        {
            placedGrassStack = new Stack<GameObject>(maxGrassCapacity);
            grassPrefabSize = GrassToPlaceBehindPrefab.GetComponent<BoxCollider>().bounds.size;
            ScytheScript.Init(this);
        }

        public void ActivateScythe()
        {

        }

        public void AddSingleGrass(int grassNumber)
        {
            GameObject newGrass = Instantiate(GrassToPlaceBehindPrefab, PlaceGrassTR);
            Vector3 newGrassLocalPosition = new Vector3(0f, grassPrefabSize.y * grassNumber, 0f);

            newGrass.transform.localPosition = newGrassLocalPosition;
            placedGrassStack.Push(newGrass);
        }

        public GameObject RemoveGrassAtTop()
        {
            return placedGrassStack.Pop();
        }
    }
}

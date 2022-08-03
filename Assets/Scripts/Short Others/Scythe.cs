using EzySlice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Scythe : MonoBehaviour
    {
        private PlayerHandler player;
        public void Init(PlayerHandler Player)
        {
            player = Player;
        }

        public void Cut(Grass grass)
        {
            GameObject grassToCollect, grassToKeep;
            cutGrass(grass, transform.position, out grassToKeep, out grassToCollect);

            player.GrassCuttedWithResult(grass, grassToKeep, grassToCollect);
        }

        private void cutGrass(Grass grassToCut, Vector3 cutAtPosition, out GameObject grassToKeepAtGround, out GameObject grassToCollect)
        {
            SlicedHull result = grassToCut.GrassGameobjectForCutting.Slice(cutAtPosition, Vector3.up);
            if(result == null) { Debug.Log("DIDNT_CUT"); grassToKeepAtGround = grassToCut.GrassGameobjectForCutting; grassToCollect = null; return; }

            GameObject lowerHull = result.CreateLowerHull();
            GameObject higherHull = result.CreateUpperHull();

            if(lowerHull.transform.position.y > higherHull.transform.position.y)
            {
                grassToCollect = lowerHull;
                grassToKeepAtGround = higherHull;
            }
            else
            {
                grassToKeepAtGround = lowerHull;
                grassToCollect = higherHull;
            }

            Transform neededTransform = grassToCut.GrassGameobjectForCutting.transform;
            lowerHull.transform.parent = grassToCut.transform;
            higherHull.transform.parent = grassToCut.transform;

            lowerHull.transform.localScale = neededTransform.localScale;
            higherHull.transform.localScale = neededTransform.localScale;
            lowerHull.transform.localPosition = neededTransform.localPosition;
            higherHull.transform.localPosition = neededTransform.localPosition;
            lowerHull.transform.localRotation = neededTransform.localRotation;
            higherHull.transform.localRotation = neededTransform.localRotation;

            Destroy(grassToCut.GrassGameobjectForCutting);
            Debug.Log("GRASS_CUTTED");
        }
    }
}

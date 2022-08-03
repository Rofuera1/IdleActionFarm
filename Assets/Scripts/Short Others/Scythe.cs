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

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Grass")
                CollidedWithGrass(collision);
        }

        private void CollidedWithGrass(Collision coll)
        {
            GameObject grassToCollect, grassToKeep;
            cutGrass(coll.gameObject, coll.contacts[0].point, out grassToKeep, out grassToCollect);
        }

        private void cutGrass(GameObject grassToCut, Vector3 cutAtPosition, out GameObject grassToKeepAtGround, out GameObject grassToCollect)
        {
            SlicedHull result = grassToCut.Slice(cutAtPosition, Vector3.right);

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

            Destroy(grassToCut);
        }
    }
}

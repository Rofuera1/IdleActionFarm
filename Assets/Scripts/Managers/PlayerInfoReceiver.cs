using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Game
{
    public enum PlayerState
    {
        Cutting,
        Selling,
        Moving
    }

    public class PlayerInfoReceiver : MonoBehaviour
    {
        protected static PlayerInfoReceiver Instance;

        private PlayerHandler player;
        private StatsProperty stats;

        private int grassMaxCapacity;
        private int grassCurrentCapacity;

        private Grass grassCurrentlyCollidingWith;

        public PlayerState currentPlayerState;

        public void Init(PlayerHandler Player, StatsProperty Stats)
        {
            Instance = this;

            player = Player;
            stats = Stats;
            grassMaxCapacity = stats.PlayerMaxGrassCapacity;

            currentPlayerState = PlayerState.Moving;
        }

        public void PlayerCollidedWithFullGrass(Grass grass)
        {
            if (grass.isAnyLeft)
            {
                player.ActivateScythe();
                grassCurrentlyCollidingWith = grass;
            }
        }

        public void PlayerStoppedCollidingWithFullGrass(Grass grass)
        {
            if(!grass.isAnyLeft && !grassCurrentlyCollidingWith.isAnyLeft)
                player.DeactivateScythe();
            grassCurrentlyCollidingWith = grass;
        }

        public void PlayerCollidedWithCuttedGrass(GameObject grassLeftower)
        {
            if (grassCurrentCapacity >= grassMaxCapacity) return;

            changeGrassCapacity(++grassCurrentCapacity);
            deactivateGrassObject(grassLeftower);
            lerpGrassToPlayer(grassLeftower);
        }

        private void deactivateGrassObject(GameObject grass)
        {
            grass.transform.tag = "Untagged";
            grass.GetComponent<Collider>().enabled = false;
        }

        private void lerpGrassToPlayer(GameObject grass)
        {
            float LERP_TIME = 0.3f;

            Vector3 placeGrassAt = player.AddSingleGrassToPlayer(grassCurrentCapacity);
            grass.transform.parent = player.PlaceGrassTR;
            grass.transform.DOLocalMove(placeGrassAt, LERP_TIME);
            grass.transform.DOScale(Vector3.zero, LERP_TIME);
            StartCoroutine(Coroutines.SetActiveAfterTime(grass, LERP_TIME, false));

            Destroy(grass, LERP_TIME * 2);
        }

        public void PlayerCuttedGrass(GameObject grassToCollect)
        {
            grassToCollect.transform.parent = null;
            grassToCollect.tag = "CuttedGrassAtGround";
            grassToCollect.AddComponent<MeshCollider>().convex = true;
            grassToCollect.AddComponent<Rigidbody>();
        }

        public void PlayerFailedToCutGrass()
        {
        }

        public void PlayerCollidedWithShop(Shop shop)
        {
            if (currentPlayerState != PlayerState.Moving)
                return;

            currentPlayerState = PlayerState.Selling;
            StartCoroutine(sellGrassOneByOne(shop));
        }

        public void PlayerStoppedCollidingWithShop()
        {
            currentPlayerState = PlayerState.Moving;
        }

        private IEnumerator sellGrassOneByOne(Shop shop)
        {
            float DELAY_TIME_BETWEEN_SELLING = 0.3f;

            while(grassCurrentCapacity > 0 && currentPlayerState == PlayerState.Selling)
            {
                shop.ReceiveGrassForSelling(player.RemoveGrassFromTopAtPlayer());
                changeGrassCapacity(--grassCurrentCapacity);

                yield return new WaitForSeconds(DELAY_TIME_BETWEEN_SELLING);
            }
        }

        private void changeGrassCapacity(int newGrassCapacity)
        {
            grassCurrentCapacity = newGrassCapacity;
            UIManager.UpdateGrassCapacity(grassCurrentCapacity, grassMaxCapacity);
        }
    }
}

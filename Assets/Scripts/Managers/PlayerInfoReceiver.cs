using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerInfoReceiver : MonoBehaviour
    {
        protected static PlayerInfoReceiver Instance;

        private IterationLogicManager logicManager;
        private PlayerHandler player;

        private StatsProperty stats;

        private int grassMaxCapacity;
        private int grassCurrentCapacity;
        public static int CurrentGrassCapacity { get => Instance.grassCurrentCapacity; }

        public void Init(IterationLogicManager LogicManager, PlayerHandler Player, StatsProperty Stats)
        {
            Instance = this;

            logicManager = LogicManager;
            player = Player;
            stats = Stats;
            grassMaxCapacity = stats.PlayerMaxGrassCapacity;
        }

        public void PlayerCollidedWithFullGrass(Grass grass)
        {
            if (IterationLogicManager.CurrentPlayerState != PlayerCycles.Moving) return;

            if (grass.isAnyLeft)
            {
                logicManager.ChangeState(PlayerCycles.Cutting);
                player.ActivateScythe(grass, grass.isGrassFull);
            }
        }

        public void PlayerCollidedWithCuttedGrass(GameObject grass)
        {
            if (grassCurrentCapacity >= grassMaxCapacity) return;
            grassCurrentCapacity++;

            deactivateGrassObject(grass);
            lerpGrassToPlayer(grass);

            logicManager.ChangeState(PlayerCycles.Collecting);
        }

        private void deactivateGrassObject(GameObject grass)
        {
            grass.transform.tag = "Untagged";
            grass.GetComponent<Collider>().enabled = false;
        }

        private void lerpGrassToPlayer(GameObject grass)
        {
            float LERP_TIME = 5f;

            Vector3 placeGrassAt = player.AddSingleGrassToPlayer(grassCurrentCapacity);
            StartCoroutine(Coroutines.LerpLocalPosition(grass.transform, placeGrassAt, LERP_TIME, EasingFunction.EaseInOutCirc));
            StartCoroutine(Coroutines.LerpScale(grass.transform, Vector3.zero, LERP_TIME, EasingFunction.Linear));
            StartCoroutine(Coroutines.SetActiveAfterTime(grass, LERP_TIME, false));

            Destroy(grass, LERP_TIME * 2);
        }

        public void PlayerCuttedGrass(GameObject grassToCollect)
        {
            logicManager.ChangeState(PlayerCycles.EndCutting);

            grassToCollect.transform.parent = null;
            grassToCollect.tag = "CuttedGrassAtGround";
            grassToCollect.AddComponent<MeshCollider>().convex = true;
            grassToCollect.AddComponent<Rigidbody>();
        }
    }
}

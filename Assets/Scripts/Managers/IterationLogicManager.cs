using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public enum PlayerCycles
    {
        Moving,
        Cutting,
        EndCutting,
        Collecting,
        Selling,
        EndSelling
    }

    public class IterationLogicManager : MonoBehaviour
    {
        public static PlayerCycles CurrentPlayerState;
        [SerializeField] public PlayerCycles cycleToDisplay;
        private UIManager _ui;

        private int goldForSingleGrassCost;
        private int maxGrassCapacity;

        public void Init(UIManager UI, StatsProperty stats)
        {
            CurrentPlayerState = PlayerCycles.Moving;
            _ui = UI;

            goldForSingleGrassCost = stats.GoldForSingleGrass;
            maxGrassCapacity = stats.PlayerMaxGrassCapacity;
        }

        private void Update()
        {
            cycleToDisplay = CurrentPlayerState;
        }

        public void ChangeState(PlayerCycles newState)
        {
            CurrentPlayerState = newState;

            switch (newState)
            {
                case PlayerCycles.Moving:
                    startMoving();
                    break;
                case PlayerCycles.Cutting:
                    cuttingGrass();
                    break;
                case PlayerCycles.EndCutting:
                    endCuttingGrass();
                    break;
                case PlayerCycles.Collecting:
                    collectGrass();
                    break;
                case PlayerCycles.Selling:
                    sellingGrass();
                    break;
                case PlayerCycles.EndSelling:
                    endSellingGrass();
                    break;
            }
        }

        private void startMoving()
        {

        }

        private void cuttingGrass()
        {

        }

        private void endCuttingGrass()
        {
            ChangeState(PlayerCycles.Moving);
        }

        private void collectGrass()
        {
            _ui.UpdateGrassCapacity(PlayerInfoReceiver.CurrentGrassCapacity, maxGrassCapacity);
            ChangeState(PlayerCycles.Moving);
        }

        private void sellingGrass()
        {

        }

        private void endSellingGrass()
        {

        }
    }
}

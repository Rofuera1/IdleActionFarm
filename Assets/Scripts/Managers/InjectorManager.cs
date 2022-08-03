using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class InjectorManager : MonoBehaviour
    {
        [SerializeField] private InputManager Input;
        [SerializeField] private PlayerMoverManager Mover;
        [SerializeField] private PlayerHandler Player;
        [SerializeField] private PlayerInfoReceiver Info;
        [SerializeField] private IterationLogicManager Logic;
        [SerializeField] private UIManager UI;
        [SerializeField] private GrassManager GrassManage;
        [Space(10)]
        [SerializeField] private StatsProperty Stats;

        private void Awake()
        {
            Mover.Init(Input);
            Player.Init(Stats.PlayerMaxGrassCapacity, Info);
            Info.Init(Logic, Player, Stats);
            Logic.Init(UI, Stats);
            GrassManage.Init(Stats.SecondsForGrassToRegrow);

            Destroy(this);
        }
    }
}

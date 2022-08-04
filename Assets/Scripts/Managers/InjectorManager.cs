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
        [SerializeField] private UIManager UI;
        [SerializeField] private GrassManager GrassManage;
        [SerializeField] private Shop ShopManager;
        [SerializeField] private PlayerAnimatorManager PlayerAnimator;
        [SerializeField] private Camera MainCamera;
        [Space(10)]
        [SerializeField] private StatsProperty Stats;

        private void Awake()
        {
            Mover.Init(Input);
            Player.Init(Stats.PlayerMaxGrassCapacity, Info);
            Info.Init(Player, Input, PlayerAnimator, Stats);
            ShopManager.Init(Stats, Player.transform);
            GrassManage.Init(Stats.SecondsForGrassToRegrow);
            UI.Init(Stats, MainCamera);

            Destroy(this);
        }
    }
}

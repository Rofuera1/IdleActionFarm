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
        [Space(10)]
        [SerializeField] private StatsProperty Stats;

        private void Awake()
        {
            Mover.Init(Input);
            Player.Init(Stats.PlayerMaxGrassCapacity, Info);
            Info.Init(Player, Stats);
            ShopManager.Init(Stats);
            GrassManage.Init(Stats.SecondsForGrassToRegrow);
            UI.Init(Stats);

            Destroy(this);
        }
    }
}

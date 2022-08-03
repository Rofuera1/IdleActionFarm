using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class InjectorManager : MonoBehaviour
    {
        [SerializeField] private InputManager Input;
        [SerializeField] private PlayerMoverManager Mover;

        private void Awake()
        {
            Mover.Init(Input);
            Destroy(this);
        }
    }
}

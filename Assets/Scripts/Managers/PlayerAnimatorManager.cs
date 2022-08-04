using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerAnimatorManager : MonoBehaviour
    {
        [SerializeField] private Animator PlayerAnimator;

        public void Walking(bool isEnabled)
        {
            PlayerAnimator.SetBool("Walking", isEnabled);
        }

        public void Cut()
        {
            PlayerAnimator.SetBool("StopCutting", false);
            PlayerAnimator.ResetTrigger("Cut");
            PlayerAnimator.SetTrigger("Cut");
        }

        public void StopCutting()
        {
            PlayerAnimator.SetBool("StopCutting", true);
        }
    }
}

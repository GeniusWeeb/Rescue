

using Rescue.InputManagers;
using Unity.VisualScripting;
using UnityEngine;

namespace  Rescue.CharacterFSM
{ 
    public class CharacterJump : CharacterBase
    {
            
        private CharacterState thisState = CharacterState.Jumping;
        private CharacterSecondaryState secondState = CharacterSecondaryState.AirBound;


      

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            PlayerManager.Instance.SetPlayerState(thisState);
          //  PlayerManager.Instance.SetJumpStatus(false);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
         
            PlayerManager.Instance.SetPlayerSecondaryState(secondState);
            InputManager.Instance.CheckAndUpdateInput();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {   
            
            PlayerManager.Instance.SetPlayerSecondaryState(PlayerManager.Instance.CheckIfPlayerGrounded ? CharacterSecondaryState.Grounded : CharacterSecondaryState.AirBound);
            PlayerManager.Instance.SetJumpStatus(false);

        }


    }
}
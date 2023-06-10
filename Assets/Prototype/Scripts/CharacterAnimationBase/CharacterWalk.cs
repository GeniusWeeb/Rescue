
using Rescue.InputManagers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Rescue.CharacterFSM
{


    public class CharacterWalk : CharacterBase
    {

        
        private CharacterState thisState = CharacterState.Walking;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            PlayerManager.Instance.SetPlayerState(thisState);
            PlayerManager.Instance.SetPlayerIsMoving(true);
          

        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
         
            InputManager.Instance.CheckAndUpdateInput();
           


        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
           
        }
        
    

    }
}

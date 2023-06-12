
using Rescue.InputManagers;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Rescue.CharacterFSM
{
    public class CharacterIdle : CharacterBase
    {

        private CharacterState thisState = CharacterState.Idle; 
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {   
            
            PlayerManager.Instance.SetPlayerState(thisState);
            PlayerManager.Instance.SetPlayerIsMoving(false);
           
     

        }
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Rigidbody body = GetRCController(animator);
            InputManager.Instance.CheckAndUpdateInput();
        

        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }

      

    }

}

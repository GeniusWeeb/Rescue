
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
            Debug.LogError("Entering Walk state");

        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
         
            CheckAndUpdateInput();

        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

            Debug.LogError("Exiting player");
            PlayerManager.Instance.SetPlayerIsMoving(false);
        }
        
        void CheckAndUpdateInput()
        { 
         
            Vector2 move = InputManager.Instance.GetControls().Player.Movement.ReadValue<Vector2>();
            
            if (move == Vector2.zero || move.y == -1 || !PlayerManager.Instance.GetMovement().GetIsGrounded)
            {
                PlayerManager.Instance.SetPlayerIsMoving(false);
                return;
            }
           
            PlayerManager.Instance.SetPlayerIsMoving(true);
            PlayerManager.Instance.SetPlayerTempMovement(move);
            
            
        }

    }
}

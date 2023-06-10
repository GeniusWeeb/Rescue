
using Rescue.InputManagers;
using UnityEngine;
using UnityEngine.Animations;
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
            Debug.LogError("Entering Idle state");

        }
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Rigidbody body = GetRCController(animator);
            CheckAndUpdateInput();
        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
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
         
            
        }
    }

}

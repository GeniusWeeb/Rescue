
using Rescue.InputManagers;
using UnityEngine;


namespace Rescue.CharacterFSM
{
    public class CharacterRun : CharacterBase
    {
            
        private CharacterState thisState = CharacterState.Running;


      

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            PlayerManager.Instance.SetPlayerState(thisState);
            PlayerManager.Instance.GetPlayerDataSO.sprintSpeed = 2f;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
         
            InputManager.Instance.CheckAndUpdateInput();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
           animator.SetBool("SetRun",false);
           PlayerManager.Instance.GetPlayerDataSO.sprintSpeed = 1f;
        }
        

    }
    
}

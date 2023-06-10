using UnityEngine;


namespace Rescue.CharacterFSM
{ 
    public class CharacterBase : StateMachineBehaviour
    {
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
           
        }
        
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }

        public Rigidbody GetRCController(Animator animator)
        {
            return animator.gameObject.GetComponentInParent<Rigidbody>();
        }

    }



    public enum CharacterState
    {
        Idle,Walking,Running,Jumping,Swimming,HoldingWeapon
    }

    public enum CharacterSecondaryState
    {
        AirBound ,
        Grounded,
        InWater
    }
}
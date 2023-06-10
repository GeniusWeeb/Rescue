using System;
using Rescue.CharacterFSM;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerManager : MonoBehaviour
{
    [Header("Main player Script")] [SerializeField]
    private CharacterState playerState;
    
    [Header("Is Player moving")] [SerializeField]
    private bool isMoving;
    
    [Header("Player Movement Data Holder")] [SerializeField]
    private PlayerSO playerMovementSO;

    [Header("Player Animator")]
    public Animator playerAnimator;

    

    private Vector2 playerMovementWorldToScreen;
    private Rigidbody playerBody;
    private Vector3 internalMovement, movement; //similar to 22  
    
    //use this to get ref
    public static PlayerManager Instance { get; private set; }

    private Movement playerMovement;

     private void Awake()
     {
         Instance = this;
         playerMovement = GetComponent<Movement>();
         playerBody = GetComponent<Rigidbody>();

     }

     public Movement GetMovement() => playerMovement;
     public PlayerSO GetPlayerDataSO => playerMovementSO;

     public void SetPlayerTempMovement(Vector2 tempMove) => playerMovementWorldToScreen = tempMove;
     public void SetPlayerState(CharacterState newState) => playerState = newState;
     public void SetPlayerIsMoving(bool status) => isMoving = status;    
     
     
     #region Update and Fixed Update for player movement

             private void Update()
             {
                playerAnimator.SetBool("SetWalk", isMoving);
                HandleRotation();
             }

             private void FixedUpdate()
             {


                 if (isMoving)
                     PlayerMove();
             }

     #endregion

    

     private void PlayerMove()
     {
         movement = (playerMovementWorldToScreen.y * this.transform.forward) + (playerMovementWorldToScreen.x * this.transform.right);
         playerBody.MovePosition(transform.position + movement * (playerMovementSO.moveSpeed * playerMovementSO.sprintSpeed * Time.fixedDeltaTime));
       
     }

     private void HandleRotation()
     {
         if (movement == Vector3.zero) return;
         Vector3 tempVec;
         tempVec.x = movement.x;
         tempVec.y = 0f;
         tempVec.z = movement.z;
         Quaternion targetRotation = Quaternion.LookRotation(tempVec);
        
         if(isMoving)
          this.transform.rotation = (Quaternion.SlerpUnclamped(transform.rotation, targetRotation, playerMovementSO.turnRate * Time.deltaTime));}

     public void PerformSprint(InputAction.CallbackContext context)
     {
         if (context.performed)
         {
             playerAnimator.SetBool("SetRun", true);
         }
         else if (context.canceled)
         {
             playerAnimator.SetBool( "SetRun", false);

         }

     }
    


}

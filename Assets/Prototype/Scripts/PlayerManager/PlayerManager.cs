using System;
using System.Collections;
using Rescue.CharacterFSM;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerManager : MonoBehaviour
{
    [Header("Main player Script")] [SerializeField]
    private CharacterState playerState;

    [Header("Secondary States")][SerializeField] private CharacterSecondaryState 
        playerSecondaryState;

    [Header("Is Player moving")] [SerializeField]

    private bool isMoving;

    [Header("Jumping")][SerializeField] private bool canJump;

    [Header("Is Grounded?")] [SerializeField]
    private bool isGrounded;
    
    [Header("Player Movement Data Holder")] [SerializeField]
    private PlayerSO playerMovementSO;

    [Header("Player Animator")] public Animator playerAnimator;
    
    //Needs a more robust system
    [Header("Ground detector - > needs to be modified")]
    private GroundCheckerCast cast;
    
    

    private Vector2 playerMovementWorldToScreen;
    private CharacterController playerController;
    private Vector3 internalMovement, movement; //similar to 22  

    //use this to get ref
    public static PlayerManager Instance { get; private set; }



#region UnityMethods
    private void Awake()
    
    {
        Instance = this;
      
        playerController = GetComponent<CharacterController>();
        cast = GetComponentInChildren<GroundCheckerCast>();

    }


    private bool canStartJump ; 
    
    private void Update()
    {   
        
        CheckGravity();
        isGrounded = cast.CheckGrounded();
        playerAnimator.SetBool("SetWalk", isMoving);
        HandleRotation();
        
    }

    private void FixedUpdate()
    {

      
        if (isMoving)
            PlayerMove();
       

    }



#endregion
    
    public PlayerSO GetPlayerDataSO => playerMovementSO;
    public bool CheckIfPlayerGrounded => isGrounded;

    public void SetPlayerTempMovement(Vector2 tempMove) => playerMovementWorldToScreen = tempMove;
    public void SetPlayerState(CharacterState newState) => playerState = newState;
    public void SetPlayerSecondaryState(CharacterSecondaryState newState) => playerSecondaryState = newState;
    
    public void SetPlayerIsMoving(bool status) => isMoving = status;
    public void SetJumpStatus(bool status) => canJump = status;
    

 

    private void PlayerMove()
    {
        movement = (playerMovementWorldToScreen.y * this.transform.forward) +
                   (playerMovementWorldToScreen.x * this.transform.right);
        playerController.Move(movement * (playerMovementSO.moveSpeed * playerMovementSO.sprintSpeed *
                                          Time.fixedDeltaTime));
    }


  [SerializeField]  private float gravity;
    [SerializeField]private float verticalVelocity ;   // this is  the main driving force for the jump
    [SerializeField]private float jumpForce ;

   [SerializeField] private bool startJumpPhase ;
    
    private void CheckGravity(){
        
        playerAnimator.SetFloat("SetFallStart" , verticalVelocity);
        if(cast.CheckGrounded()){
            
           playerAnimator.SetBool("SetIdle",true);
          
            
            verticalVelocity =  startJumpPhase ? jumpForce : -gravity * Time.fixedDeltaTime ;

            }
        else 
        {

            verticalVelocity -= gravity * Time.deltaTime ;        
            playerAnimator.SetFloat("SetFallStart" , verticalVelocity);
            // gravity added while in air
        }
        
       
        
        playerController.Move(  new Vector3 (0f ,verticalVelocity ,0f)* Time.fixedDeltaTime);



        }

    private void HandleRotation()
    {
        
        if (movement == Vector3.zero) return;
        Vector3 tempVec;
        tempVec.x = movement.x;
        tempVec.y = 0f;
        tempVec.z = movement.z;
        Quaternion targetRotation = Quaternion.LookRotation(tempVec);

        if (isMoving)
            this.transform.rotation = (Quaternion.SlerpUnclamped(transform.rotation, targetRotation,
                playerMovementSO.turnRate * Time.deltaTime));
                
    }

    #region Sprint and jump

                public void PerformSprint(InputAction.CallbackContext context)
                {
                    if (context.performed)
                    {
                        playerAnimator.SetBool("SetRun", true);
                    }
                    else if (context.canceled)
                    {
                        playerAnimator.SetBool("SetRun", false);
                    
                        

                    }

                }
                
                public void PerformJump(InputAction.CallbackContext context)
                {
                    if (context.performed)
                    {
                        //  body.isKinematic = false;
                        playerAnimator.SetBool("SetJump", true);
                       startJumpPhase = true ;
                      

                    }

                    else if (context.canceled)
                    {   
                    
                       StartCoroutine(JumpDelay());
                   
                      
                    }
                }


                private IEnumerator JumpDelay()
                {
                 yield return new WaitForSeconds(1.5f);
                 startJumpPhase = false ;
                 playerAnimator.SetBool("SetJump",false);
                 Debug.Log("Jump is canceled");
                }
              
                //Main Jump perform here
            

               
    #endregion
}


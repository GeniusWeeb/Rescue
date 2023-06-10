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

    private void Awake()
    {
        Instance = this;
      
        playerController = GetComponent<CharacterController>();
        cast = GetComponentInChildren<GroundCheckerCast>();

    }
    
    public PlayerSO GetPlayerDataSO => playerMovementSO;
    public bool CheckIfPlayerGrounded => isGrounded;

    public void SetPlayerTempMovement(Vector2 tempMove) => playerMovementWorldToScreen = tempMove;
    public void SetPlayerState(CharacterState newState) => playerState = newState;
    public void SetPlayerSecondaryState(CharacterSecondaryState newState) => playerSecondaryState = newState;
    
    public void SetPlayerIsMoving(bool status) => isMoving = status;
    public void SetJumpStatus(bool status) => canJump = status;
    

    #region Update and Fixed Update for player movement

    private void Update()
    {  
        isGrounded = cast.CheckGrounded();
        playerAnimator.SetBool("SetWalk", isMoving);
        HandleRotation();
    }

    private void FixedUpdate()
    {

        CheckGravity();
        if (isMoving)
            PlayerMove();
        if (canJump)  //Check for grounded too
            PlayerPerformJump();

    }

    #endregion



    private void PlayerMove()
    {
        movement = (playerMovementWorldToScreen.y * this.transform.forward) +
                   (playerMovementWorldToScreen.x * this.transform.right);
        playerController.Move(movement * (playerMovementSO.moveSpeed * playerMovementSO.sprintSpeed *
                                          Time.fixedDeltaTime));
    }

    private void CheckGravity()
    {
        if (!cast.CheckGrounded())
        {
            playerController.Move(Vector3.down * (playerMovementSO.fallDownSpeed * Time.deltaTime));
        }

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
                        canJump = true;

                    }

                    else if (context.canceled)
                    {
                        playerAnimator.SetBool("SetJump", false);
                        StartCoroutine(delay());
                    }
                }

              
                //Main Jump perform here
              private  void PlayerPerformJump()
              {
              }

                IEnumerator delay()
                {   
                    yield return new WaitForSeconds(playerMovementSO.jumpSpeed);
                    canJump = false;
                }
    #endregion
}


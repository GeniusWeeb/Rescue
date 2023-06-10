using System;
using Rescue.CharacterFSM;
using UnityEngine;


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

     public void SetPlayerTempMovement(Vector2 tempMove) => playerMovementWorldToScreen = tempMove;
     public void SetPlayerState(CharacterState newState) => playerState = newState;
     public void SetPlayerIsMoving(bool status) => isMoving = status;     
     #region Update and Fixed Update for player movement

             private void Update()
             {
                playerAnimator.SetBool("SetWalk", isMoving);
             }

             private void FixedUpdate()
             {


                 if (isMoving)
                     PlayerMove();
             }

     #endregion

    

     private void PlayerMove()
     {
         Vector3 movement = (playerMovementWorldToScreen.y * this.transform.forward) + (playerMovementWorldToScreen.x * this.transform.right);
         playerBody.MovePosition(transform.position + movement * (playerMovementSO.moveSpeed * Time.fixedDeltaTime));
     }

}

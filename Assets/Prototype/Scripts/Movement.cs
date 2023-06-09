
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{

    [SerializeField] private Vector2 localMove;
    private Rigidbody body;
    [SerializeField] private float moveSpeed ;
    [SerializeField] private GroundCheckerCast cast;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private bool  grounded;
    [Range(0,1)]
    [SerializeField] private float jumpDuration ;

    [SerializeField] private CameraMovement cameraControl;
    [SerializeField] private bool  isMoving;
    [SerializeField] private Vector3 movement;
    [Range(1,10)]
    [SerializeField] private float turnRate;

    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Vector3 jumpHeight;
    [SerializeField] private float gravitySpeed;
    [SerializeField] private bool canJump;
    
 

    private void Awake()    
    {
        
        body = this.GetComponent<Rigidbody>();
     
    }

    private void Update()
    {

        HandleAnimation();
        HandleRotation();
        ApplyGravityIfNotGrounded();
    }

    void HandleAnimation()
    {
        if(isMoving)
            playerAnimator.SetBool("SetWalk", true);
        else
        {
            playerAnimator.SetBool("SetWalk", false);
        }
        
    }

    private void FixedUpdate()
    {
       
        if (canJump) Jump();
    }

    private void HandleRotation()
    {
        if (movement == Vector3.zero) return;
        Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
        
        this.transform.rotation  = Quaternion.SlerpUnclamped(transform.rotation, targetRotation, turnRate * Time.deltaTime);
        
       //  this.transform.rotation = finalRot;
    }




      
    public void MovePerFrame(Vector2 tempMove)
    {
        localMove = tempMove;
        isMoving = true;
        movement = (tempMove.y * this.transform.forward) + (tempMove.x * this.transform.right) ;
      
        body.MovePosition(transform.position + movement * (moveSpeed * Time.fixedDeltaTime));
        
        // Rotate towards the movement direction
    }
    
    void ApplyGravityIfNotGrounded()
    {
        if (!cast.CheckGrounded())
        {
            body.MovePosition(transform.position  +  Vector3.down  * (gravitySpeed   *  Time.deltaTime ) );
        }
        
    }


  public   void PerformJump(InputAction.CallbackContext context)
    {
        if (context.performed )
        {
            //  body.isKinematic = false;
            playerAnimator.SetBool("SetJump", true);
            canJump = true;
         
        }

        if (context.canceled)
        {
            playerAnimator.SetBool("SetJump", false);
            StartCoroutine(delay());
        }

    }

    void Jump()
    {
       
        
        body.MovePosition(this.transform.position + jumpHeight * (jumpSpeed * Time.deltaTime) );
    }   

    IEnumerator delay()
    {   
        yield return new WaitForSeconds(jumpDuration);
        canJump = false;
    }
  

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


    public Vector3 SetMovement(Vector3 tempMovement) => movement =  tempMovement;

    public bool GetIsGrounded() => cast.CheckGrounded(); 
    //this can be moved to the grounded part of a statemachine like idle or whatever
    public bool SetIsMoving(bool status) => isMoving = status;

   

}

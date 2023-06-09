
using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{

    private GameControls control; 
    private PlayerInput playerInput;
    
    
    [SerializeField] private Vector2 move;
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




    private void OnEnable()
    {
        control.Enable();
        
    }

    private void OnDisable()
    {
        control.Disable();
    }


    private void Awake()    
    {
        control = new GameControls();
      
        body = this.GetComponent<Rigidbody>();
        control.Player.Jump.performed += PerformJump;
        control.Player.Jump.canceled += PerformJump;
        control.Player.Sprint.performed += SprintPerformed; 
        control.Player.Sprint.canceled += SprintPerformed; 
        
        

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
        MovePerFrame();
        if (canJump) Jump();
    }

    private void HandleRotation()
    {
        if (movement == Vector3.zero) return;
        Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
        
        var finalRot = Quaternion.Slerp(transform.rotation, targetRotation, turnRate * Time.deltaTime);
        
        if(isMoving) this.transform.rotation = finalRot;
    }
         
    

 
 
    void MovePerFrame()
    {
        move = control.Player.Movement.ReadValue<Vector2>();
        if (move == Vector2.zero || move.y == -1  ||   !cast.CheckGrounded())
        {
            isMoving = false;
            return; }
        
        movement = (move.y * this.transform.forward) + (move.x * this.transform.right) ;
        isMoving = true;
       
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


    void PerformJump(InputAction.CallbackContext context)
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
  

    void SprintPerformed(InputAction.CallbackContext context)
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


    //this can be moved to the grounded part of a statemachine like idle or whatever
 
}

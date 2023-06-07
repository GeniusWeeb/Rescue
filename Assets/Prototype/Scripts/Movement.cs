using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Vector3 sampleMove;
    [SerializeField] private GroundCheckerCast cast;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private bool  grounded;
    [Range(0,1)]
    [SerializeField] private float jumpDuration ;

    [SerializeField] private CameraMovement cameraControl; 
    
    
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
      
    }

    private void Update()
    {
            PlayerRotate(); 
           ApplyGravityIfNotGrounded();
        
    }
    

    private void FixedUpdate()
    {
      MovePerFrame();   
     
    }


    void PlayerRotate()
    {
        var move = cameraControl.CameraMove();
        this.transform.Rotate(0f,move.y ,0f);

    }
 
    void MovePerFrame()
    {
        move = control.Player.Movement.ReadValue<Vector2>();
        if (move == Vector2.zero || !cast.CheckGrounded()) return; 
        Vector3 movement = (move.y * cameraControl.transform.forward) + (move.x * cameraControl.transform.right) ;
        body.MovePosition( transform.position + movement * moveSpeed * Time.fixedDeltaTime  );
    }


    void ApplyGravityIfNotGrounded()
    {
        
        if (!cast.CheckGrounded())
        {
           
            body.MovePosition(transform.position  + 5f  * Time.deltaTime * Vector3.down );
          
        }
       

    }


    void PerformJump(InputAction.CallbackContext context)
    {
        if (context.performed && cast.CheckGrounded())
        {
            body.isKinematic = false;
            body .AddForce(transform.up * jumpSpeed , ForceMode.Impulse);
            StartCoroutine(JumpCheck());
        }
    }

    
    //this can be moved to the grounded part of a statemachine like idle or whatever
    IEnumerator JumpCheck()
    {
        yield return new WaitForSeconds(jumpDuration);
        body.isKinematic = true;
    }
}

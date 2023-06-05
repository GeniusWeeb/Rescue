using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    
    
    private   GameControls  controls ;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private  float mouseX;
    [SerializeField] private  float mouseY;
    [SerializeField] private  float rotationX;
    [SerializeField] private  float rotationY;
    [SerializeField] private Vector3  cameraOffsetValue ;
    
    private Vector2 mouseLook;
    


    [SerializeField] private GameObject cameraFollowThis;
    
    
    private void Awake()
    {
        controls = new GameControls();
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update()
    {
        LookPerFrame();
    }

    void LookPerFrame()
    {
        mouseLook  = controls.Player.Look.ReadValue<Vector2>();
        mouseX = mouseLook.x * Time.deltaTime * mouseSensitivity; 
        mouseY = mouseLook.y * Time.deltaTime * mouseSensitivity;


         rotationX -= mouseY;  //Remove every y aspect from X
         rotationY -= mouseX;  // Remove every x asepct form Y
        rotationX = Mathf.Clamp(rotationX, -45f, 45f);  
        rotationY = Mathf.Clamp(rotationY, -45f, 45f);  
        this.transform.localRotation = Quaternion.Euler(rotationX, -rotationY , 0);
  //  this.transform.Rotate(Vector3.up * mouseX);

    }

    private void LateUpdate()
    {

        this.transform.position = cameraFollowThis.transform.position + cameraOffsetValue;
    }


    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
}
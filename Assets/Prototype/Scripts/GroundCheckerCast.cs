
using Rescue.CharacterFSM;
using UnityEngine;
using UnityEditor;



public class GroundCheckerCast : MonoBehaviour
{


    [SerializeField] private LayerMask layerGround;
    private RaycastHit hit;
    private bool detect;
    [SerializeField] private float maxDistance ;

    [SerializeField] private bool isGrounded;
    [SerializeField]private Vector3 castSize ; 
    private CharacterController playerController ;
    
    private void Awake()
    {
      
      playerController = GetComponentInParent<CharacterController>(); 
       
    }

    private void FixedUpdate()
    {
        
     detect =    Physics.BoxCast(this.transform.position, castSize/2,
         Vector3.down,  out hit, transform.rotation ,maxDistance,layerGround);
     if ( detect)
     {   
         isGrounded = true;
        
     }
     else
     {
        
        Debug.Log("Not Grounded");
         isGrounded = false;    
     }
    }
    
    public bool CheckGrounded() => isGrounded;
    public void ForceSetGroundedTrue(bool status) => isGrounded = status;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;  
        Gizmos.DrawRay(this.transform.position ,Vector3.down * maxDistance );
        Gizmos.DrawWireCube(this.transform.position ,castSize);
     
         
        if (detect)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube( this.transform.position,castSize/2);
            Gizmos.DrawRay(this.transform.position,Vector3.down * maxDistance );
        }
    }
}


using UnityEngine;


public class GroundCheckerCast : MonoBehaviour
{


    [SerializeField] private LayerMask layerGround;
    private RaycastHit hit;
    private bool detect;
    [SerializeField] private float maxDistance ;

    [SerializeField] private bool isGrounded;
    
    private void Awake()
    {
       //  collider = this.GetComponent<Collider>();
    }

    private void Update()
    {
     detect =    Physics.BoxCast(this.transform.position, this.transform.localScale/2,
         -transform.up,  out hit, transform.rotation , maxDistance     ,layerGround);
     if (detect)
     {
         
         isGrounded = true;


     }
     else
     {
         isGrounded = false;    
     }
    }
    
    public bool CheckGrounded() => isGrounded;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;  
        Gizmos.DrawRay(this.transform.position,-transform.up );
        Gizmos.DrawWireCube(this.transform.position , this.transform.localScale/2);
     
         
        if (detect)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube( this.transform.position , this.transform.localScale/2);
        }
    }
}

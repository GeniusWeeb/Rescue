using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{

    [SerializeField] private float playerViewDetectDistance;
    
    private void OnDrawGizmos()
    {
        Gizmos.color =  Color.yellow;
        Gizmos.DrawRay(this.transform.position , transform.forward * 10f);
   
    }
}

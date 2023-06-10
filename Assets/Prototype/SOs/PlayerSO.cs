
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Data/Player/Movement", fileName = "MovementData")]
public class PlayerSO : ScriptableObject
{
    [Range(0, 100)] public float moveSpeed;
    [Range(0, 100)] public float fallDownSpeed;
    [Range(0, 100)] public float turnRate;

    public Vector3 jumpHeight;
    [Range(0, 100)] public float jumpSpeed;
    [Range(0, 100)] public float sprintSpeed;
    [Range(0, 100)] public float sprintStamina;
    

    [Header("Player Death animation")] //gameobejct or addresbales
    public GameObject deathAnimation;

}

 

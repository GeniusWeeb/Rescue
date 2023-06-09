using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private static PlayerManager instance;
    
    //use this to get ref
    public static PlayerManager Instance
    {
        get => instance;
        set => instance = value;
    }
    
     private Movement playerMovement;

     private void Awake()
     {
         Instance = this;
         playerMovement = GetComponent<Movement>();

     }

     public Movement GetMovement() => playerMovement;
}

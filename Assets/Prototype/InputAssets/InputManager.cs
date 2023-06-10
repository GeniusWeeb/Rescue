
using UnityEngine;
using UnityEngine.InputSystem;

//Sincwe are using the C# class generated  by the input system , we will directly refeence the generated class

namespace  Rescue.InputManagers
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;
        
        private GameControls controls;
        private PlayerManager player;

        private void OnEnable()
        {
            controls.Enable();
            //Sprinting
            controls.Player.Jump.performed += JumpPerforming;
            controls.Player.Jump.canceled += JumpPerforming;

            // Sprinting
            controls.Player.Sprint.performed += SprintPerforming;
            controls.Player.Sprint.canceled += SprintPerforming;

        }

        private void OnDisable()
        {
            controls.Disable();
        }

        private void Awake()
        {
            Instance = this;
            controls = new GameControls();
            player = PlayerManager.Instance;
            //Subscribe to the diff actions from the input class

        }

    

        private void SprintPerforming(InputAction.CallbackContext context)
        {
            player.GetMovement().PerformSprint(context);
        }

        private void JumpPerforming(InputAction.CallbackContext context)
        {
            player.GetMovement().PerformJump(context);
        }

        public GameControls GetControls() => controls;
        public PlayerManager GetPlayer() => player;

    }

}


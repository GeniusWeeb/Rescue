
using UnityEngine;
using UnityEngine.InputSystem;

//Sincwe are using the C# class generated  by the input system , we will directly refeence the generated class

namespace  Rescue.InputManagers
{


    public class InputManager : MonoBehaviour
    {
        
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
            controls = new GameControls();
            player = PlayerManager.Instance;
            //Subscribe to the diff actions from the input class

        }

        private void FixedUpdate()
        {
            Vector2 move = controls.Player.Movement.ReadValue<Vector2>();
            if (move == Vector2.zero || move.y == -1 || !player.GetMovement().GetIsGrounded())
            {
                player.GetMovement().SetIsMoving(false);
                return;
            }

            player.GetMovement().MovePerFrame(move);

        }

        private void SprintPerforming(InputAction.CallbackContext context)
        {
            player.GetMovement().PerformSprint(context);
        }

        private void JumpPerforming(InputAction.CallbackContext context)
        {
            player.GetMovement().PerformJump(context);
        }

    }

}


namespace Player
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using Global;

    public class PlayerController : MonoBehaviour
    {
        public float horizontalSpeed;
        public float maxHorizontalSpeed;

        private Rigidbody2D playerRB;
        private float movementInput;

        private InputActions controls;

        // Activates all of the controls for the player
        private void Awake()
        {
            // We need to first set this to be a new object before we can do anything
            controls = new InputActions();

            // Then we can set up calllbacks to specific methods that we want the controls to listen to
            controls.Player.Movement.performed += ctx => GrabMovement(ctx);
            controls.Player.Movement.canceled += ctx => GrabMovement(ctx);
        }

        // Enables the controls when the player is active
        private void OnEnable()
        {
            controls.Enable();
        }

        // Diables the controls when the player is not active
        private void OnDisable()
        {
            controls.Disable();
        }

        // Sets private Components
        private void Start()
        {
            movementInput = 0f;

            playerRB = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (movementInput != 0f)
            {
                float newXMovement = Mathf.Clamp(playerRB.velocity.x + (movementInput * horizontalSpeed), 0f, maxHorizontalSpeed);

                Vector2 newVelocity = new Vector2(newXMovement, playerRB.velocity.y);
                playerRB.velocity = Vector2.Lerp(playerRB.velocity, newVelocity, 0.5f * Time.deltaTime);
            }
            else
            {
                Vector2 neutral = new Vector2(0f, playerRB.velocity.y);
                playerRB.velocity = Vector2.Lerp(playerRB.velocity, neutral, 0.5f * Time.deltaTime);
            }
        }

        private void GrabMovement(InputAction.CallbackContext ctx)
        {
            movementInput = ctx.ReadValue<float>();
        }
    }

}
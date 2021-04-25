namespace Player
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using Global;
    using Obstacle;
    using Level;

    public class PlayerController : MonoBehaviour
    {
        public SpriteRenderer playerSprite;
        public Animator playerAnimations;

        public float horizontalSpeed;
        public float horizontalAcceleration;
        public float stopAcceleration;

        public float maxYAcceleration;
        public float fastFallGravity;

        private bool isAlive;

        private InputActions controls;
        private Rigidbody2D playerRB;
       
        private float movementInput;
        private bool isFastFalling;
        private float initialGravity;

        public bool SetAlive
        {
            set { 
                
                if (isAlive == false && value == true)
                {
                    isAlive = value;
                }
            }
        }

        // Activates all of the controls for the player
        private void Awake()
        {
            // We need to first set this to be a new object before we can do anything
            controls = new InputActions();

            // Then we can set up calllbacks to specific methods that we want the controls to listen to
            controls.Player.Movement.performed += ctx => GrabMovement(ctx);
            controls.Player.Movement.canceled += ctx => GrabMovement(ctx);

            controls.Player.FastFall.performed += ctx => GrabPlunge(ctx);
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
            isFastFalling = false;
            isAlive = false;

            playerRB = GetComponent<Rigidbody2D>();

            initialGravity = playerRB.gravityScale;

            if (GameManager.Instance != null)
            {
                GameManager.Instance.ResetScore();
            }
        }

        private void FixedUpdate()
        {
            if (movementInput != 0f)
            {
                float newXMovement = movementInput * horizontalSpeed;
                float clampY = Mathf.Clamp(playerRB.velocity.y, 0f, maxYAcceleration);

                Vector2 newVelocity = new Vector2(newXMovement, clampY);
                playerRB.velocity = Vector2.Lerp(playerRB.velocity, newVelocity, horizontalAcceleration * Time.deltaTime);
            }
            else
            {
                float clampY = Mathf.Clamp(playerRB.velocity.y, 0f, maxYAcceleration);

                Vector2 newVelocity = new Vector2(0f, clampY);
                playerRB.velocity = Vector2.Lerp(playerRB.velocity, newVelocity, stopAcceleration * Time.deltaTime);
            }
        }

        private void GrabMovement(InputAction.CallbackContext ctx)
        {
            if(isAlive)
            {
                movementInput = ctx.ReadValue<float>();
                
                switch (Mathf.Sign(movementInput))
                {
                    case 1:
                        playerSprite.flipX = true;
                        break;
                    case -1:
                        playerSprite.flipX = false;
                        break;
                }
            }
        }
    
        private void GrabPlunge(InputAction.CallbackContext ctx)
        {
            if(isAlive)
            {
                isFastFalling = !isFastFalling;
                playerAnimations.SetBool("isPlunging", isFastFalling);

                if (isFastFalling)
                {
                    playerRB.gravityScale = fastFallGravity;
                }
                else
                {
                    playerRB.gravityScale = initialGravity;
                }

            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
           if(collision.collider.CompareTag("Breakable"))
            {
                if(isFastFalling)
                {
                    GameManager.Instance.Score = collision.gameObject.GetComponent<PointSystem>().GetPointAmount;
                    Destroy(collision.gameObject);
                }
                else
                {
                    // game over
                    print("You died!");
                    GameManager.Instance.ToGameOver();
                }
            }
           else if(collision.collider.CompareTag("Invincible"))
            {
                // game over
                print("You died!");
                GameManager.Instance.ToGameOver();
            }
        }
    
        public void PlayIntroAnimation()
        {
            if(isAlive == false)
            {
                playerAnimations.SetBool("isReady", true);
            }
        }
    }

}
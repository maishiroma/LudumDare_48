namespace Player
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using Global;
    using Level;

    public class PlayerController : MonoBehaviour
    {
        public AudioSource sfx;
        public AudioClip deadSound;
        public AudioClip plummetStart;
        public AudioClip plummetEnd;

        public SpriteRenderer playerSprite;
        public Animator playerAnimations;

        public float horizontalSpeed;
        public float horizontalAcceleration;
        public float stopAcceleration;

        public float timeGravityAdd;
        public float addToGravity;
        
        public float maxYAcceleration;
        public float fastFallGravity;

        public float diffMaxGravity;
        public float diffMaxFastFall;

        public Vector2 drillHitBoxOffset;
        public Vector2 drillHitBoxSize;

        private bool isAlive;

        private InputActions controls;
        private Rigidbody2D playerRB;
        private BoxCollider2D playerHitBox;

        private float movementInput;
        private bool isFastFalling;
        private float initialGravity;
        private float initialFastFall;
        private float currGravity;
        private Vector2 currHitBoxSize;
        private Vector2 currHitBoxOffset;

        private float currTimeFallling;

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
            playerHitBox = GetComponent<BoxCollider2D>();

            initialGravity = playerRB.gravityScale;
            currGravity = initialGravity;
            currHitBoxSize = playerHitBox.size;
            currHitBoxOffset = playerHitBox.offset;

            if (GameManager.Instance != null)
            {
                GameManager.Instance.ResetScore();
            }
        }

        private void Update()
        {
            if(isFastFalling == false && isAlive == true)
            {
                currTimeFallling += Time.deltaTime;

                if(currTimeFallling > timeGravityAdd)
                {
                    playerRB.gravityScale += addToGravity;
                    currTimeFallling = 0f;
                }
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
                
                if(movementInput != 0)
                {
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
        }
    
        private void GrabPlunge(InputAction.CallbackContext ctx)
        {
            if(isAlive)
            {
                isFastFalling = !isFastFalling;
                playerAnimations.SetBool("isPlunging", isFastFalling);

                if (isFastFalling)
                {
                    sfx.PlayOneShot(plummetStart, 0.5f);
                    playerRB.gravityScale = fastFallGravity;
                    playerHitBox.offset = drillHitBoxOffset;
                    playerHitBox.size = drillHitBoxSize;
                }
                else
                {
                    sfx.PlayOneShot(plummetEnd, 0.5f);
                    playerRB.gravityScale = currGravity;
                    playerHitBox.offset = currHitBoxOffset;
                    playerHitBox.size = currHitBoxSize;
                    currTimeFallling = 0f;
                }

            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
           if(collision.collider.CompareTag("Breakable"))
            {
                if(isFastFalling)
                {
                    StartCoroutine(collision.gameObject.GetComponent<BreakableBlocks>().DestroyBlock(sfx));
                }
                else
                {
                    StartCoroutine(PlayDeathScene());
                }
            }
           else if(collision.collider.CompareTag("Invincible"))
            {
                StartCoroutine(PlayDeathScene());
            }
        }
    

        private IEnumerator PlayDeathScene()
        {
            if(isAlive == true)
            {
                isAlive = false;
                playerAnimations.SetBool("isDead", true);
                yield return new WaitForFixedUpdate();

                movementInput = 0f;
                playerRB.isKinematic = true;
                playerRB.velocity = Vector2.zero;
                yield return new WaitForFixedUpdate();

                sfx.PlayOneShot(deadSound, 1f);
                yield return new WaitForSeconds(1f);

                GameManager.Instance.ToGameOver();
            }
            yield return null;
        }

        public void IncreaseFallSpeeds()
        {
            fastFallGravity = Mathf.Clamp(fastFallGravity + 2f, initialFastFall, diffMaxFastFall);
            currGravity = Mathf.Clamp(currGravity + 2f, initialGravity, diffMaxGravity);

            playerRB.gravityScale = fastFallGravity;
            maxYAcceleration = fastFallGravity;
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
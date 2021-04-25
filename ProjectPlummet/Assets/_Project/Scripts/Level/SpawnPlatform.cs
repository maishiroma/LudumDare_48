namespace Level
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using Global;
    using Player;
    using TMPro;

    public class SpawnPlatform : MonoBehaviour
    {
        public GameObject blockGenerator;
        public PlayerController player;
        public SpriteRenderer platformVisual;
        public TextMeshProUGUI startText;
        
        public AudioSource bgm;
        public AudioClip startSound;

        public float fadeSpeed;

        private InputActions controls;
        private bool hasStarted;
        private bool isDissapear;

        private Color platformColor;
        private float alphaLerp;

        // Activates all of the controls for the player
        private void Awake()
        {
            // We need to first set this to be a new object before we can do anything
            controls = new InputActions();

            // Then we can set up calllbacks to specific methods that we want the controls to listen to
            controls.Player.FastFall.performed += ctx => StartGame(ctx);
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

        private void Start()
        {
            hasStarted = false;
            isDissapear = false;

            platformColor = platformVisual.material.color;
            alphaLerp = platformVisual.material.color.a;

            startText.text = "Tap Spacebar to START!";
        }

        private void Update()
        {
            if(isDissapear == true)
            {
                alphaLerp = Mathf.Lerp(alphaLerp, 0f, fadeSpeed * Time.deltaTime);
                platformVisual.material.color = new Color(platformColor.r, platformColor.g, platformColor.b, alphaLerp);

                if(alphaLerp <= 0.1f)
                {
                    bgm.Play();
                    blockGenerator.SetActive(true);
                    player.SetAlive = true;
                    gameObject.SetActive(false);
                }
            }

        }

        private void StartGame(InputAction.CallbackContext ctx)
        {
            if(hasStarted == false)
            {
                bgm.PlayOneShot(startSound, 1f);
                startText.text = "";
                player.PlayIntroAnimation();
                hasStarted = true;
            }
        }

        public void StartDissapear()
        {
            if(isDissapear == false)
            {
                isDissapear = true;
            }
        }
    }

}
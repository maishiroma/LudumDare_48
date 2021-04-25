namespace Player
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Level;

    public class PlayerStart : MonoBehaviour
    {
        public SpawnPlatform spawnPlatform;
        public AudioSource sfx;
        public AudioClip jumpSound;

        public void InvokeDissapear()
        {
            if(spawnPlatform != null)
            {
                spawnPlatform.StartDissapear();
            }
        }

        public void PlayJumpSound()
        {
            if(sfx != null)
            {
                sfx.PlayOneShot(jumpSound, 1f);
            }
        }
 
    }

}
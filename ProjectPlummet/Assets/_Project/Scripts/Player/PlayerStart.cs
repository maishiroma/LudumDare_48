namespace Player
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Level;

    public class PlayerStart : MonoBehaviour
    {
        public SpawnPlatform spawnPlatform;

        public void InvokeDissapear()
        {
            if(spawnPlatform != null)
            {
                spawnPlatform.StartDissapear();
            }
        }
 
    }

}
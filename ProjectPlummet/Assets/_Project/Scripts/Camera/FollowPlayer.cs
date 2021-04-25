namespace Camera
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class FollowPlayer : MonoBehaviour
    {
        public Rigidbody2D player;

        public float offset;

        private void FixedUpdate()
        {
            float newPos = player.position.y + offset;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, newPos, gameObject.transform.position.z);
        }

    }

}
namespace Camera
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class FollowPlayer : MonoBehaviour
    {
        public Rigidbody2D player;

        public Vector2 offset;

        private void FixedUpdate()
        {
            Vector2 newPos = new Vector2(player.position.x + offset.x, player.position.y + offset.y);
            gameObject.transform.position = new Vector3(newPos.x, newPos.y, gameObject.transform.position.z);
        }

    }

}
namespace Player
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AttackHitbox : MonoBehaviour
    {
        private BoxCollider2D hitbox;

        private void Start()
        {
            hitbox = GetComponent<BoxCollider2D>();
            ToggleHitBox();
        }

        public void ToggleHitBox()
        {
            //hitbox.enabled = !hitbox.enabled;
            hitbox.enabled = true;
        }
    }

}
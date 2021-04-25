namespace Level
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Global;

    public class BreakableBlocks : MonoBehaviour
    {
        public Animator blockAnimations;
        public bool isDestroyed;

        public AudioClip blockBreak;

        [SerializeField]
        private int pointAmount;
        private BoxCollider2D blockCollider;

        private void Start()
        {
            isDestroyed = false;
            blockCollider = GetComponent<BoxCollider2D>();
        }

        public IEnumerator DestroyBlock(AudioSource sfx)
        {
            if(GameManager.Instance != null && isDestroyed == false)
            {
                isDestroyed = true;
                blockCollider.enabled = false;
                blockAnimations.SetBool("isBroken", true);
                sfx.PlayOneShot(blockBreak, 1f);
                yield return new WaitForFixedUpdate();

                GameManager.Instance.Score = pointAmount;
                yield return new WaitForSeconds(blockAnimations.GetCurrentAnimatorStateInfo(0).length);

                Destroy(gameObject);
            }
        }
        
        
        
        public int GetPointAmount
        {
               get { return pointAmount; }
        }

    }

}
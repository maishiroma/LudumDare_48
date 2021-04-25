namespace Camera
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class YParallaxScrolling : MonoBehaviour
    {
        public GameObject mainCamera;
        public float effectAmount;
        public float offsetTimes;

        public Sprite[] bgList;

        private SpriteRenderer bgSprite;
        private float yLength;
        private float startYPos;
        private int currIndex;

        private void Start()
        {
            bgSprite = GetComponent<SpriteRenderer>();

            startYPos = transform.position.y;
            yLength = bgSprite.bounds.size.y;
        }

        private void Update()
        {
            float temp = mainCamera.transform.position.y * (1 - effectAmount);
            float disitance = mainCamera.transform.position.y * effectAmount;
            transform.position = new Vector3(transform.position.x, startYPos + disitance, transform.position.z);

            if(temp > startYPos + yLength)
            {
                startYPos += yLength * offsetTimes;
            }
            else if(temp < startYPos - yLength)
            {
                startYPos -= yLength * offsetTimes;
            }
        }

        public void ChangeBG()
        {
            if(currIndex + 1 > bgList.Length)
            {
                currIndex = 0;
            }
            else
            {
                currIndex += 1;
            }
            bgSprite.sprite = bgList[currIndex];
        }
    }

}
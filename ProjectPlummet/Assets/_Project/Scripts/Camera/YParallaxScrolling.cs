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

        private float yLength;
        private float startYPos;

        private void Start()
        {
            startYPos = transform.position.y;
            yLength = GetComponent<SpriteRenderer>().bounds.size.y;
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
    }

}
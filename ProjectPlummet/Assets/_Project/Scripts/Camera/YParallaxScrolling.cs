namespace Camera
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class YParallaxScrolling : MonoBehaviour
    {
        public GameObject mainCamera;
        public float effectAmount;

        private float yLength;
        private float startYPos;

        void Start()
        {
            startYPos = transform.position.y;
            yLength = GetComponent<SpriteRenderer>().bounds.size.y;
        }

        void FixedUpdate()
        {
            float temp = mainCamera.transform.position.y * (1 - effectAmount);
            float disitance = mainCamera.transform.position.y * effectAmount;
            transform.position = new Vector3(transform.position.x, startYPos + disitance, transform.position.z);

            if(temp > startYPos + yLength)
            {
                startYPos += yLength;
            }
            else if(temp < startYPos - yLength)
            {
                startYPos -= yLength;
            }
        }
    }

}
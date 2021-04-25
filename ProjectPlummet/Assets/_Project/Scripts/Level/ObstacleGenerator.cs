namespace Level
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ObstacleGenerator : MonoBehaviour
    {
        public GameObject[] prefabArray;

        public float xRange;

        public float maxSequential;

        public float minSpawnRate;
        public float maxSpawnRate;

        private float currTime;
        private float nextSpawnTime;

        private void Start()
        {
            nextSpawnTime = Random.Range(minSpawnRate, maxSpawnRate);
        }

        private void Update()
        {
            if(currTime >= nextSpawnTime)
            {
                float randomXSpot = Random.Range(gameObject.transform.position.x - xRange, gameObject.transform.position.x + xRange);
                int randomIndex = Random.Range(0, prefabArray.Length);
                float randomSequentialAmount = Random.Range(0, maxSequential);

                // Sequential Spawn Up or Sides?
                int ranXOrYSeq = Random.Range(0, 2);

                if(ranXOrYSeq == 0)
                {
                    // Spawn left or right?
                    int ranLeftOrRight = Random.Range(0, 2);
                    int multiplier = -1;
                    if(ranLeftOrRight == 0)
                    {
                        multiplier = 1;
                    }

                    // Iterates on the spawning
                    for (int iterator = 0; iterator <= randomSequentialAmount; iterator++)
                    {
                        Instantiate(prefabArray[randomIndex], new Vector3(randomXSpot, gameObject.transform.position.y, 0f), Quaternion.identity);
                        randomXSpot += prefabArray[randomIndex].GetComponentInChildren<SpriteRenderer>().bounds.size.x * multiplier;
                    }
                }
                else
                {
                    // Iterates on the spawning
                    float currYPos = gameObject.transform.position.y;
                    for (int iterator = 0; iterator <= randomSequentialAmount; iterator++)
                    {
                        Instantiate(prefabArray[randomIndex], new Vector3(randomXSpot, currYPos, 0f), Quaternion.identity);
                        currYPos += prefabArray[randomIndex].GetComponentInChildren<SpriteRenderer>().bounds.size.y;
                    }
                }

                currTime = 0f;
                nextSpawnTime = Random.Range(minSpawnRate, maxSpawnRate);
            }
            else
            {
                currTime += Time.deltaTime;
            }
        }
    }

}
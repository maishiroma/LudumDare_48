namespace Level
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Global;
    using Camera;
    using Player;

    public class ObstacleGenerator : MonoBehaviour
    {
        public GameObject[] prefabArray;
        public YParallaxScrolling[] bgList;
        public PlayerController player;

        public float xRange;
        public float maxSequential;

        public float minSpawnRate;
        public float maxSpawnRate;

        public int scoreThreshold;
        public int modCheck;

        public float diffMaxSequential;
        public float diffMinSpawnRate;
        public float diffMaxSpawnRate;

        private float currTime;
        private float nextSpawnTime;
        private float startingSequential;
        private float startingMinSpawnRate;
        private float startingMaxSpawnRate;
        private int numbTimesDifficultyChanged;

        private void Start()
        {
            nextSpawnTime = Random.Range(minSpawnRate, maxSpawnRate);

            startingSequential = maxSequential;
            startingMinSpawnRate = minSpawnRate;
            startingMaxSpawnRate = maxSpawnRate;
            numbTimesDifficultyChanged = 0;
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
                IncreaseDifficulty();
            }
        }
    
        private void IncreaseDifficulty()
        {
            if(GameManager.Instance.Score >= scoreThreshold)
            {
                maxSequential = Mathf.Clamp(maxSequential + 1, startingSequential, diffMaxSequential);
                minSpawnRate = Mathf.Clamp(minSpawnRate - 0.2f, diffMinSpawnRate, startingMinSpawnRate);
                maxSpawnRate = Mathf.Clamp(maxSpawnRate - 0.2f, diffMaxSpawnRate, startingMaxSpawnRate);

                if(numbTimesDifficultyChanged % modCheck == 0 && numbTimesDifficultyChanged != 0)
                {
                    foreach (YParallaxScrolling currItem in bgList)
                    {
                        currItem.ChangeBG();
                    }
                    player.IncreaseFallSpeeds();
                }

                numbTimesDifficultyChanged += 1;
                scoreThreshold += scoreThreshold;
                print(numbTimesDifficultyChanged);
            }
        }
    }

}
namespace Level
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ObstacleGenerator : MonoBehaviour
    {
        public GameObject[] prefabArray;

        public Transform spawnArea;
        public float xRange;

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
                float randomXSpot = Random.Range(spawnArea.position.x - xRange, spawnArea.position.x + xRange);
                int randomIndex = Random.Range(0, prefabArray.Length);

                Instantiate(prefabArray[randomIndex], new Vector3(randomXSpot, spawnArea.position.y, 0f), Quaternion.identity);

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
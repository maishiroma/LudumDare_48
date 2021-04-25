namespace Global
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField]
        private int gameOverIndex;

        private int currScore;
        private int currHighScore;

        public int Score
        {
            get { return currScore; }
            set { currScore += value; }
        }

        public int HighScore
        {
            get { return currHighScore; }
        }

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            currScore = 0;
            currHighScore = 0;
        }

        public void ResetScore()
        {
            currScore = 0;
        }

        public void ToGameOver()
        {
            if(currHighScore < currScore)
            {
                currHighScore = currScore;
            }

            SceneManager.LoadScene(gameOverIndex);
        }
    }

}
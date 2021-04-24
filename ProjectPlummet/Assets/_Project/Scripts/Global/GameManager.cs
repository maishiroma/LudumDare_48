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

        private int score;

        public int Score
        {
            get { return score; }
            set { score += value; }
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

        public void ResetScore()
        {
            score = 0;
        }

        public void ToGameOver()
        {
            SceneManager.LoadScene(gameOverIndex);
        }
    }

}
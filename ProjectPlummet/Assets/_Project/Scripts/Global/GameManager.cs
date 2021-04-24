namespace Global
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

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
    }

}
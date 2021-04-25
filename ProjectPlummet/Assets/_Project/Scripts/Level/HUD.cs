namespace Level
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
    using Global;

    public class HUD : MonoBehaviour
    {
        public TextMeshProUGUI roundScore;
        public TextMeshProUGUI highScore;

        private void Update()
        {
            if(GameManager.Instance != null)
            {
                roundScore.text = GameManager.Instance.Score.ToString();

                if(highScore != null)
                {
                    if (GameManager.Instance.HighScore == GameManager.Instance.Score)
                    {
                        highScore.text = "New High Score!";
                    }
                    else
                    {
                        highScore.text = "High Score: " + GameManager.Instance.HighScore.ToString();
                    }
                }
            }
        }
    }

}
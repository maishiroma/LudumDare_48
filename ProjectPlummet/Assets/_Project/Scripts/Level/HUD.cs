namespace Level
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
    using Global;

    public class HUD : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;

        private void Update()
        {
            if(GameManager.Instance != null)
            {
                scoreText.text = "Score: " + GameManager.Instance.Score.ToString();
            }
            else
            {
                scoreText.text = "Score: ";
            }
        }
    }

}
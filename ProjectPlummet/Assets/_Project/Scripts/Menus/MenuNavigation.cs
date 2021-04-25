namespace Menus
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.EventSystems;

    public class MenuNavigation : MonoBehaviour
    {
        public int mainLevelIndex;
        public int titleScreenIndex;

        public AudioClip buttonSound;
        public AudioSource audioPlayer;

        public EventSystem menuEvents;
        public GameObject mainMenuPanel;
        public GameObject controlsPanel;
        public GameObject creditsPanel;

        private GameObject prevButton;

        public void NavigateToSubButtons(string type)
        {
            if(mainMenuPanel != null)
            {
                prevButton = menuEvents.currentSelectedGameObject;

                mainMenuPanel.SetActive(false);
                switch(type)
                {
                    case "controls":
                        controlsPanel.SetActive(true);
                        break;
                    case "credits":
                        creditsPanel.SetActive(true);
                        break;
                }
                audioPlayer.PlayOneShot(buttonSound, 1f);
                menuEvents.SetSelectedGameObject(GameObject.FindGameObjectWithTag("UI_Return"));
            }
        }

        public void NavigateBackToMain()
        {
            if (mainMenuPanel != null)
            {
                controlsPanel.SetActive(false);
                creditsPanel.SetActive(false);
                mainMenuPanel.SetActive(true);

                audioPlayer.PlayOneShot(buttonSound, 1f);
                menuEvents.SetSelectedGameObject(prevButton);
            }
        }

        public void LoadToMainLevel()
        {
            audioPlayer.PlayOneShot(buttonSound, 1f);
            SceneManager.LoadScene(mainLevelIndex);
        }

        public void LoadToTitleScreen()
        {
            audioPlayer.PlayOneShot(buttonSound, 1f);
            SceneManager.LoadScene(titleScreenIndex);
        }

        public void QuitGame()
        {
            audioPlayer.PlayOneShot(buttonSound, 1f);
            Application.Quit();
        }

    }

}
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

                menuEvents.SetSelectedGameObject(prevButton);
            }
        }

        public void LoadToMainLevel()
        {
            SceneManager.LoadScene(mainLevelIndex);
        }

        public void LoadToTitleScreen()
        {
            SceneManager.LoadScene(titleScreenIndex);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

    }

}
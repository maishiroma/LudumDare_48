namespace Menus
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class MenuNavigation : MonoBehaviour
    {
        public int mainLevelIndex;
        public int titleScreenIndex;

        public void LoadToMainLevel()
        {
            SceneManager.LoadScene(mainLevelIndex);
        }

        public void LoadToTitleScreen()
        {
            SceneManager.LoadScene(titleScreenIndex);
        }

    }

}
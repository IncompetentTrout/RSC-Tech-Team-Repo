using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Menu : MonoBehaviour {
    public GameObject mainMenu;
    public GameObject Title_Screen;


    private void Start() {
        TitleScreenButton();
    }


    public void StartButton() {
        // Play Now Button has been pressed loads first level
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameLevel");
    }

    public void TitleScreenButton() {
        // Shows title screen
        mainMenu.SetActive(false);
        Title_Screen.SetActive(true);
    }


    public void title_Screen_Play_Button() {
        // Show Main Menu
        mainMenu.SetActive(true);
        Title_Screen.SetActive(false);
    }

    public void QuitButton() {
        // Quit Game
        Application.Quit();
    }
}
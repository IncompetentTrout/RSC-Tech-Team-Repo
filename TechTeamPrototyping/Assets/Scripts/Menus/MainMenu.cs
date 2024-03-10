using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    public GameObject mainMenu;
    public GameObject titleScreen;
    
    private void Start() {
        TitleScreenButton();
    }
    
    public void StartButton() {
        // Play Now Button has been pressed loads first level
        SceneManager.LoadScene("GameLevel");
    }

    /*
     * shows the title screen
     */
    public void TitleScreenButton() {
        mainMenu.SetActive(false);
        titleScreen.SetActive(true);
    }

    /*
     * shows the main menu
     */
    public void title_Screen_Play_Button() {
        mainMenu.SetActive(true);
        titleScreen.SetActive(false);
    }

    public void QuitButton() {
        Application.Quit();
    }
}
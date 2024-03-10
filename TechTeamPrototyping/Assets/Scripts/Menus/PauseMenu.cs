using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    
    [SerializeField] private Slider sliderVolume;
    [SerializeField] private Slider sliderBrightness;
    [SerializeField] private Image brightnessOverlay;
    
    public TextMeshProUGUI VolumeText_Percentage;
    
    public static bool gameIsPaused;
    
    public GameObject pauseMenu;
    public GameObject pauseMenuMain;
    public GameObject settingsMenu;
    public AudioMixer Mixer;
    
    private void Start() {
        settingsMenu.SetActive(false);
    }

    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }

        if (gameIsPaused)
            pauseMenu.SetActive(true);
        else
            pauseMenu.SetActive(false);

        AdjustAudio();
    }

    public void resume() {
        gameIsPaused = !gameIsPaused;
    }

    // pauses game by reducing the time that passes to 0
    public void PauseGame() {
        if (gameIsPaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void settings() {
        pauseMenuMain.SetActive(false);
        settingsMenu.SetActive(true);
    }

    // disables setting menu and enbales the main pause menu
    public void back() {
        pauseMenuMain.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void AdjustAudio() {
        VolumeText_Percentage.text = $"{Mathf.Round(sliderVolume.value * 100)}%";
        Mixer.SetFloat("volume", sliderVolume.value);
    }

    // adjusts brightness by changing the alpha value of a dark background image
    public void adjustBrightness() {
        var tempColor = brightnessOverlay.color;
        tempColor.a = sliderBrightness.value;
        brightnessOverlay.color = tempColor;
    }

    public void pausebutton() {
        gameIsPaused = false;
    }
}
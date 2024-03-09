using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class Pause_Menu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseMenuMain;
    public GameObject SettingsMenu;
    public static bool gameIsPaused;
    public AudioMixer Mixer;
    [SerializeField] private Slider sliderVolume;
    public TMPro.TextMeshProUGUI VolumeText_Percentage;

    [SerializeField] private Slider sliderBrightness;
    [SerializeField] private Image brightnessOverlay;

    void Start()
    {
        SettingsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) ){
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }

        if (gameIsPaused){
            pauseMenu.SetActive(true);
           
            
        }
        else{
            pauseMenu.SetActive(false);
        }

        AdjustAudio();
    }

    public void resume() 
    {
        gameIsPaused = !gameIsPaused;

    }
    // pauses game by reducing the time that passes to 0
    public void PauseGame(){
        if (gameIsPaused){
            Time.timeScale = 0;
        }
        else{
            Time.timeScale = 1;
        }
    }

   public void settings()
    {
        pauseMenuMain.SetActive(false);
        SettingsMenu.SetActive(true);

        
    }
    // disables setting menu and enbales the main pause menu
    public void back() 
    {
        pauseMenuMain.SetActive(true);
        SettingsMenu.SetActive(false);
    
    }
    public void AdjustAudio()
    {
        VolumeText_Percentage.text = $"{Mathf.Round(sliderVolume.value * 100)}%";
        Mixer.SetFloat("volume", sliderVolume.value);
        
        
    }
    // adjusts brightness by changing the alpha value of a dark background image
    public void adjustBrightness() 
    {
        Color tempColor = brightnessOverlay.color;
        tempColor.a = sliderBrightness.value;
        brightnessOverlay.color = tempColor;
    }
    public void pausebutton(){
        gameIsPaused = false;
    }
}

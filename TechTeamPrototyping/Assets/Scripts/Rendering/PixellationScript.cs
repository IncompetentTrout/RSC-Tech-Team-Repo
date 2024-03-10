using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PixellationScript : MonoBehaviour {
    [SerializeField] private int downScale; //amount the pixel resolution is divided by

    [SerializeField] private Camera
        mainCamera, //reference to the main camera
        pixelCamera, //reference to the camera responsible for rendering the pixel effect
        depthCamera; // reference to the camera responsible for rendering depth

    [SerializeField] private RenderTexture
        pixelTexture, //reference to the render texture for pixellation used in the Shader
        depthTexture; //reference to the render texture for depth used in the Shader

    [SerializeField] private RawImage pixelEffect; //reference to the UI RawImage that renders the pixellation on screen


    //called before the start function
    private void Awake() {
        CameraSizeUpdate(); //calls the camera update just before the game starts to get settings in place
    }


    //Resizes the pixel resolution according to the main camera, call if the player is given an option to change the screen size
    public void CameraSizeUpdate() {
        var effectSize =
            pixelEffect
                .GetComponent<RectTransform>(); //obtains the rectTransform of the effect renderer to change its size
        effectSize.sizeDelta =
            new Vector2(mainCamera.pixelWidth,
                mainCamera.pixelHeight); //changes the size of the pixelEffect to the size of the camera

        var cameraSize =
            new Vector2(mainCamera.pixelWidth / downScale,
                mainCamera.pixelHeight / downScale); //creates a downscaled version of the main camera
        //sets the render textures to the downscaled size
        pixelTexture.width = (int)cameraSize.x;
        pixelTexture.height = (int)cameraSize.y;
        depthTexture.width = (int)cameraSize.x;
        depthTexture.height = (int)cameraSize.y;
    }
}
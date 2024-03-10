using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PixellationScript : MonoBehaviour {
    [SerializeField] private int downScale;

    [SerializeField] private Camera mainCamera, pixelCamera, depthCamera; 
  
    [SerializeField] private RenderTexture pixelTexture, depthTexture; 

    [SerializeField] private RawImage pixelEffect; //reference to the UI RawImage that renders the pixellation on screen

    
    private void Awake() {
        CameraSizeUpdate();
    }


   
    public void CameraSizeUpdate() {
        // obtains the rectTransform of the effect renderer to change its size
        var effectSize = pixelEffect.GetComponent<RectTransform>();

        // changes the size of the pixelEffect to the size of the camera
        effectSize.sizeDelta = new Vector2(mainCamera.pixelWidth, mainCamera.pixelHeight);

        //creates a downscaled version of the main camera
        var cameraSize = new Vector2(mainCamera.pixelWidth / downScale, mainCamera.pixelHeight / downScale);
        
        // sets the render textures to the downscaled size
        
        pixelTexture.width = (int)cameraSize.x;
        pixelTexture.height = (int)cameraSize.y;
        depthTexture.width = (int)cameraSize.x;
        depthTexture.height = (int)cameraSize.y;
    }
}
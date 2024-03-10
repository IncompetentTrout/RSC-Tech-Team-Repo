using UnityEngine;
using UnityEngine.UI;

public class Backgroundscaler : MonoBehaviour {
    private Image backgroundImage;
    private float ratio;
    private RectTransform rt;


    private void Start() {
        backgroundImage = GetComponent<Image>();
        rt = backgroundImage.rectTransform;
        ratio = backgroundImage.sprite.bounds.size.x / backgroundImage.sprite.bounds.size.y;
    }


    private void Update() {
        if (!rt)
            return;

        //Scale image proportionally to fit the screen dimensions, while preserving aspect ratio
        if (Screen.height * ratio >= Screen.width)
            rt.sizeDelta = new Vector2(Screen.height * ratio, Screen.height);
        else
            rt.sizeDelta = new Vector2(Screen.width, Screen.width / ratio);
    }
}
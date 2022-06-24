using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gamemanager : MonoBehaviour
{
    public float scrollSpeed;
    public float descentSpeed;
    public Transform cameraTransform;
    public float currentDepth;
    float currentDepthRounded;
    [SerializeField]TextMeshProUGUI text;
    [SerializeField]TextMeshProUGUI textZone;
    public shooting shooting;
    public SpriteRenderer background;
    public Color[] colors;
    int zone;
    void FixedUpdate()
    {
        if (currentDepth <= 10936f)
        {
            cameraTransform.Translate(-Vector2.up * Time.deltaTime * scrollSpeed);
             currentDepth += Time.deltaTime * descentSpeed;
             currentDepthRounded = Mathf.Round(currentDepth);
             text.text = currentDepthRounded.ToString() + "m";
        }
         switch(zone)
        {
            case 1:
            background.color = Color.Lerp(background.color, colors[zone - 1], Time.deltaTime * 0.025f);
            break;
            case 2:
            background.color = Color.Lerp(background.color, colors[zone - 1], Time.deltaTime * 0.035f);
            break;
        }
        switch(currentDepthRounded)
        {
            case 0:
            textZone.SetText("Sunlight Zone");
            zone = 1;
            scrollSpeed = 0.5f;
            descentSpeed = 3.33f;
            shooting.recoveryBounce = 0.5f;
            break;
            case 200:
            textZone.SetText("Twilight Zone");
            zone = 2;
            scrollSpeed = 0.6f;
            descentSpeed = 6.66f;
            shooting.recoveryBounce = 0.6f;
            break;
            case 1000:
            textZone.SetText("Midnight Zone");
            zone = 3;
            scrollSpeed = 0.8f;
            descentSpeed = 25f;
            shooting.recoveryBounce = 0.7f;
            break;
            case 4000:
            textZone.SetText("Abyssal Zone");
            zone = 4;
            scrollSpeed = 0.7f;
            descentSpeed = 16.66f;
            shooting.recoveryBounce = 0.8f;
            break;
            case 6000:
            textZone.SetText("Hadal Zone");
            zone = 5;
            scrollSpeed = 0.9f;
            descentSpeed = 27.77f;
            shooting.recoveryBounce = 0.9f;
            break;
            case 10935:
            textZone.SetText("CHALLENGER DEEP");
            break;
        }
    }
}

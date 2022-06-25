using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
//0: Brake
//1: Dash
//2: Auto
//3: Shotgun

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
    float minSpawnTime;
    float maxSpawnTime;
    float currentSpawnTime;
    public GameObject egg;
    public Transform[] spawns;
    public Slider slider;
    public float maxPower;
    public float powerRegenRate;
    public float currentPower;
    float powerDraw;
    public float powerLerp;
    public float chipSpeed;
    public Slider chipSlider;
    public float updatedPower;
    float updatedMaxPower;
    [SerializeField]bool transition;

    void Start()
    {
        currentSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        currentPower = maxPower;
        updatedMaxPower = maxPower;
         slider.maxValue = maxPower; 
         chipSlider.maxValue = maxPower;
        UpdatePower();
    }

    void Update()
    {
        UpdatePower();
        if(shooting.usedPower)
        {
            switch(shooting.powerType)
            {
                case 0:
                powerDraw = 5;
                if(currentPower - powerDraw < 0)
                {
                     shooting.outOfPower = true;
                }
                else
                {
                    shooting.outOfPower = false;
                    updatedPower = currentPower - powerDraw;
                    transition = true;
                }
                break;
                case 1:
                powerDraw = 5;
                if(currentPower - powerDraw < 0)
                {
                     shooting.outOfPower = true;
                }
                else
                {
                    shooting.outOfPower = false;
                    updatedPower = currentPower - powerDraw;
                     transition = true;
                }
                break;
                case 2:
                powerDraw = 1;
                if(currentPower - powerDraw < 0)
                {
                     shooting.outOfPower = true;
                }
                else
                {
                    shooting.outOfPower = false;
                    updatedPower = currentPower - powerDraw;
                     transition = true;
                }
                break;
                case 3:
                powerDraw = 5;
                if(currentPower - powerDraw < 0)
                {
                    shooting.outOfPower = true;
                }
                else
                {
                    shooting.outOfPower = false;
                    updatedPower = currentPower - powerDraw;
                     transition = true;
                }
                break;
            }
            shooting.usedPower = false;
        }
    }
    void FixedUpdate()
    {
        if (currentPower < maxPower && !transition)
        {
              currentPower += Time.deltaTime * powerRegenRate;
        }
         if(currentPower > updatedMaxPower)
        {
            updatedPower = updatedMaxPower;
            transition = true;
        }
        UpdatePower();
        if (Time.time >= currentSpawnTime)
        {
            GameObject spawn = Instantiate(egg, spawns[Random.Range(0, spawns.Length)].position, Quaternion.identity); 
            currentSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
        }
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
            minSpawnTime = 3;
            maxSpawnTime = 6;
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

    void UpdatePower()
    {
        maxPower = Mathf.Lerp(maxPower, updatedMaxPower, Time.deltaTime * powerLerp);
        if (currentPower - updatedPower > 0.99f && transition)
        {
            currentPower = Mathf.Lerp(currentPower, updatedPower, Time.deltaTime * powerLerp * powerDraw);
        }
        else
        {
            transition = false;
        }
        slider.value = currentPower;
        chipSlider.value = maxPower;
    }

    public void hit()
    {
        updatedMaxPower = maxPower - 10;
        UpdatePower();
        StartCoroutine(recover());
    }

    IEnumerator recover()
    {
        yield return new WaitForSeconds(10);
        updatedMaxPower = maxPower + 10;
        transition = true;
    }
}

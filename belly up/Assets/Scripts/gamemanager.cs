using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
 using UnityEngine.Experimental.Rendering.Universal;
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
    float powerCool = 10;
    [SerializeField]float currentPowerCool;
    public Light2D playerLight;
    public Light2D globalLight;
    [SerializeField]int limitManage;

    void Start()
    {
        currentSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        currentPower = maxPower;
        updatedMaxPower = maxPower;
         slider.maxValue = maxPower; 
         chipSlider.maxValue = maxPower;
         currentPowerCool = powerCool;
        UpdatePower();
    }

    void Update()
    {
        switch(zone)
        {
            case 1:
            background.color = Color.Lerp(background.color, colors[zone - 1], Time.deltaTime * 0.025f);
            break;
            case 2:
            background.color = Color.Lerp(background.color, colors[zone - 1], Time.deltaTime * 0.0015f);
            break;
            case 3:
            playerLight.intensity = Mathf.Lerp(playerLight.intensity,1,1*Time.deltaTime);
            globalLight.intensity = Mathf.Lerp(globalLight.intensity,0,1*Time.deltaTime);
            break;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Time.timeScale += 1;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Time.timeScale -=1;
        }
        UpdatePower();
        if(shooting.usedPower)
        {
            switch(shooting.powerType)
            {
                case 0:
                powerDraw = 3;
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
                powerDraw = 3;
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
                powerDraw = 10;
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

    void LateUpdate()
    {
         if (currentDepth <= 10936f)
        {
            cameraTransform.Translate(-Vector2.up * Time.deltaTime * scrollSpeed);
             currentDepth += Time.deltaTime * descentSpeed;
             currentDepthRounded = Mathf.Round(currentDepth);
             text.text = currentDepthRounded.ToString() + "m";
        }
    }
    void FixedUpdate()
    {
        if (maxPower < 100)
        {
            if (currentPowerCool > 0)
            {
                currentPowerCool -= Time.deltaTime * 0.5f;
            }
            else
            {
                updatedMaxPower = maxPower + 10;
                transition = true;
                currentPowerCool = powerCool;
            }
        }
        
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
            spawn.GetComponent<egg>().Spawn(limitManage);
            currentSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
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
            limitManage = 1;
            shooting.recoveryBounce = 0.5f;
            break;
            case 200:
            textZone.SetText("Twilight Zone");
            minSpawnTime = 2;
            maxSpawnTime = 5;
            limitManage = 2;
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
            limitManage = 3;
            shooting.recoveryBounce = 0.7f;
            break;
            case 4000:
            textZone.SetText("Abyssal Zone");
            zone = 4;
            minSpawnTime = 1;
            maxSpawnTime = 4;
            scrollSpeed = 0.7f;
            descentSpeed = 16.66f;
            limitManage = 4;
            shooting.recoveryBounce = 0.8f;
            break;
            case 6000:
            textZone.SetText("Hadal Zone");
            zone = 5;
            minSpawnTime = 1;
            maxSpawnTime = 3;
            scrollSpeed = 0.9f;
            descentSpeed = 27.77f;
            shooting.recoveryBounce = 0.9f;
            limitManage = 5;
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
        currentPowerCool = powerCool;
        
    }
}

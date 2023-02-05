using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
 
 using UnityEngine.SceneManagement;
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
    float powerCool = 10;
    [SerializeField]float currentPowerCool;
    public UnityEngine.Rendering.Universal.Light2D playerLight;
    public UnityEngine.Rendering.Universal.Light2D globalLight;
    [SerializeField]int limitManage;
    public GameObject plasticBag;
    public GameObject challengerDeepText;
    public Transform mommy;
    public bool spawning;
    public GameObject plasticHealthBar;
    public bool powerUpUsed;
    public int powerUpType;
    public Image powerFill;
    Color ogColor;
    public Transform center;
    public bool canLose = false;
    public CanvasGroup realUi;
    public CanvasGroup GameOver;
    public Animator black;
    public AudioClip[] musics;
    public AudioSource speaker;
    public AudioSource speaker2;
    public AudioSource speaker3;
    public plasticbag plasticScript;
    public ParticleSystem buble;
    public GameObject endText1;
    public GameObject endText2;
    public GameObject endBack;
    public bool isTutorial;
    public GameObject lowPower;
    bool concern = false;

    void Start()
    {
        minSpawnTime = 3;
            maxSpawnTime = 6;
        currentSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        currentPower = maxPower;
         slider.maxValue = maxPower; 
         chipSlider.maxValue = maxPower;
         currentPowerCool = powerCool;
        UpdatePower();
        ogColor = powerFill.color;
    }

    IEnumerator outOfPowerGents()
    {
        shooting.shakeAmount = 0.05f;
        shooting.shakeDuration = 0.25f;
        shooting.shaking = true;
        powerFill.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        powerFill.color = ogColor;
    }

    void Update()
    {
        currentPower = Mathf.Clamp(currentPower, 0, maxPower);
        maxPower = Mathf.Clamp(maxPower, 0, 150);

        if(!isTutorial)

        {
            if (plasticScript.death)
        {
            StartCoroutine(victory());
            plasticScript.death = false;
        }
        }
        if (canLose && shooting.out2)
        {
            Lose();
        }
        
        if (shooting.outOfPower)
        {
            StartCoroutine(outOfPowerGents());
        }
        if (powerUpUsed)
        {
            
            switch(powerUpType)
            {
                case 0:
                speaker.PlayOneShot(musics[7]);
                Battery();
                break;

                case 1:
                speaker.PlayOneShot(musics[8]);
                if (maxPower < 150)
                {
                    maxPower += 10;
                    concern = false;
                    speaker3.Stop();
                    lowPower.SetActive(false);
                }
                powerUpUsed = false;
                break;
            }
        }
        if (!isTutorial)
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
            globalLight.intensity = Mathf.Lerp(globalLight.intensity,0.09f,1*Time.deltaTime);
            break;
            case 6:
            playerLight.intensity = Mathf.Lerp(playerLight.intensity,0,1*Time.deltaTime);
            globalLight.intensity = Mathf.Lerp(globalLight.intensity,1,1*Time.deltaTime);
            break;
        }
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
                    currentPower -= powerDraw;
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
                    currentPower -= powerDraw;
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
                    currentPower -= powerDraw;
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
                    currentPower -= powerDraw;
                }
                break;
            }
            shooting.usedPower = false;
        }
    }

    void LateUpdate()
    {
         if (currentDepth <= 10935f)
        {
            cameraTransform.Translate(-Vector2.up * Time.deltaTime * scrollSpeed);
             currentDepth += Time.deltaTime * descentSpeed;
             currentDepthRounded = Mathf.Round(currentDepth);
             text.text = currentDepthRounded.ToString() + "m";
        }
    }
    void FixedUpdate()
    {
        
        if (currentPower < maxPower)
        {
              currentPower += Time.deltaTime * powerRegenRate;
        }
        UpdatePower();
        if (Time.time >= currentSpawnTime)
        {
            if (spawning)
            {
                GameObject spawn = Instantiate(egg, spawns[Random.Range(0, spawns.Length)].position, Quaternion.identity); 
            spawn.GetComponent<egg>().Spawn(limitManage);
            currentSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
            }
        }
        if(!isTutorial)
        {
            switch(currentDepthRounded)
        {
            case 0:
            textZone.SetText("Sunlight Zone");
            zone = 1;
            speaker.Stop();
            speaker.PlayOneShot(musics[zone-1]);
            minSpawnTime = 3;
            maxSpawnTime = 6;
            scrollSpeed = 0.5f;
            descentSpeed = 3.33f;
            limitManage = 1;
            shooting.recoveryBounce = 0.5f;
            break;
            case 200:
            textZone.SetText("Twilight Zone");
            speaker.Stop();
            speaker.PlayOneShot(musics[zone-1]);
            speaker.PlayOneShot(musics[16]);
            minSpawnTime = 2;
            maxSpawnTime = 5;
            limitManage = 2;
            zone = 2;
            scrollSpeed = 0.6f;
            descentSpeed = 6.66f;
            shooting.recoveryBounce = 0.6f;
            break;
            case 1000:
            speaker.Stop();
            speaker.PlayOneShot(musics[zone-1]);
            speaker.PlayOneShot(musics[17]);
            textZone.SetText("Midnight Zone");
            zone = 3;
            scrollSpeed = 0.8f;
            descentSpeed = 25f;
            limitManage = 3;
            shooting.recoveryBounce = 0.7f;
            break;
            case 4000:
            speaker.Stop();
            speaker.PlayOneShot(musics[zone-1]);
            speaker.PlayOneShot(musics[18]);
            textZone.SetText("Abyssal Zone");
            zone = 4;
            minSpawnTime = 2;
            maxSpawnTime = 4;
            scrollSpeed = 0.7f;
            descentSpeed = 16.66f;
            limitManage = 5;
            shooting.recoveryBounce = 0.8f;
            break;
            case 6000:
            speaker.Stop();
            speaker.PlayOneShot(musics[zone-1]);
            speaker.PlayOneShot(musics[19]);
            textZone.SetText("Hadal Zone");
            zone = 5;
            scrollSpeed = 0.9f;
            descentSpeed = 41.11f;
            shooting.recoveryBounce = 0.9f;
            break;
            case 10934:
            zone = 6;
            endBack.SetActive(true);
            textZone.SetText("CHALLENGER DEEP");
            StartCoroutine(boss());
            shooting.recoveryBounce = 0f;
           Clear();
            break;
        }
        }
        
    }

    IEnumerator boss()
    {
        minSpawnTime = 2;
        maxSpawnTime = 3;
        buble.Stop();
        speaker.Stop();
        spawning = false;
        shooting.control = false;
        yield return new WaitForSeconds(5);
        plasticBag.SetActive(true);
        speaker.PlayOneShot(musics[5]);
        yield return new WaitForSeconds(5);
        speaker.PlayOneShot(musics[5]);
        challengerDeepText.SetActive(true);
        yield return new WaitForSeconds(5);
         speaker.PlayOneShot(musics[9]);
        challengerDeepText.GetComponent<Rigidbody2D>().gravityScale = 500;
        shooting.control = true;
        shooting.shakeAmount = 0.15f;
        shooting.shakeDuration = 2;
        shooting.shaking = true;
        yield return new WaitForSeconds(2);
        speaker.Stop();
        buble.Play();
        speaker2.Play();
        var bubles = buble.emission;
        bubles.rateOverTime = 100;
        plasticBag.GetComponent<Animator>().SetBool("start", true);
        spawning = true;
        plasticHealthBar.SetActive(true);
    }

    IEnumerator victory()
    {
        Clear();
        buble.Stop();
        speaker.Stop();
        spawning = false;
        shooting.control = false;
        plasticHealthBar.GetComponent<Animator>().SetBool("done", true);
        yield return new WaitForSeconds(1);
        endText1.SetActive(true);
        yield return new WaitForSeconds(4);
        endText2.SetActive(true);
        yield return new WaitForSeconds(4);
        black.SetBool("trans", true);
         yield return new WaitForSeconds(1f);
         SceneManager.LoadScene(2);

    }

    void Clear()
    {
        int i = 0;
            GameObject[] allChildren = new GameObject[mommy.childCount];
            foreach(Transform child in mommy)
            {
                allChildren[i] = child.gameObject;
                i+= 1;
            }
            foreach(GameObject child in allChildren)
            {
                DestroyImmediate(child.gameObject);
            }
    }

    void UpdatePower()
    {
        float currentVelocity = 0;
        slider.value = Mathf.SmoothDamp(slider.value, currentPower, ref currentVelocity, powerLerp * powerDraw * Time.deltaTime);
        chipSlider.value = Mathf.SmoothDamp(chipSlider.value, maxPower, ref currentVelocity, powerLerp * Time.deltaTime);
    }

    public void hit()
    {
        speaker.PlayOneShot(musics[10]);
        shooting.hit = true;
        if(!isTutorial)
        {
            if (maxPower - 10 > 0)
            {
                maxPower -= 5;
                if(maxPower <= 30)
                {
                    if(!concern)
                    {
                        concern = true;;
                        speaker3.Play();
                        lowPower.SetActive(true);
                    }
                }
                else
                {
                    concern = false;
                }
            }
            else
            {
                if (canLose)
                {
                    shooting.out2 = true;
                    speaker.Stop();
                    speaker2.Stop();
                }
            }
        }
    }

    void Battery()
    {
            currentPower += 20;
        powerUpUsed = false;
    }

    void Lose()
    {
        if(canLose)
        {
            speaker.PlayOneShot(musics[6]);
            shooting.control  = false;
            maxPower = 0;
             spawning = false;
             realUi.alpha  = Mathf.Lerp(realUi.alpha, 0, Time.deltaTime * 5);
             GameOver.gameObject.SetActive(true);
            GameOver.alpha  = Mathf.Lerp(GameOver.alpha, 1, Time.deltaTime * 5);
        }
    }

    public void Reload()
    {
       StartCoroutine(reloadAnime());
    }

    IEnumerator reloadAnime()
    {
         black.SetBool("trans", true);
         yield return new WaitForSeconds(1f);
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public void PlasticBagHit()
    {
         speaker.PlayOneShot(musics[Random.Range(12,14)], 0.3f);
    }
}

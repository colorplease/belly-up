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
    [SerializeField]float currentSpawnTime;
    public GameObject egg;
    public Transform[] spawns;
    public Slider slider;
    public float maxPower;
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
    public TextMeshProUGUI totalfishmurderedondeath;
    public bool isTutorial;
    public GameObject lowPower;
    bool concern = false;
    [SerializeField]depthMeter DepthMeter;
    [SerializeField]GameObject rocksObjects;
    public bool canRegen;
    public int howManyAnglers;
    public int anglerSpawns;
    [Header("Difficulty Scaling")]
    int struggleMinSpawnTime;
    int struggleMaxSpawnTime;
    int currentMinSpawnTime;
    int currentMaxSpawnTime;
    [Header("Pause")]
    [SerializeField]GameObject pauseMenu;
    [SerializeField]GameObject pauseMenuMenu;
    [SerializeField]GameObject accessMenu;
    [SerializeField]GameObject controlMenu;
    [SerializeField]TextMeshProUGUI controlScheme;
    [SerializeField]TextMeshProUGUI controlReadOut;
    [SerializeField]GameObject secretMenu;
    [SerializeField]TextMeshProUGUI confirmation;
    public bool dylanMode;
    int scheme = 0;
    public bool isPaused;
    [SerializeField]GameObject PToPause;
    [Header("PowerAttributes")]
    public bool isEndless;
    public float maximumMaxPower = 150;
    public float maxPowerUpAmount = 15;
    public float powerUpAmount = 35;
    public float scrollSpeed;
    public float descentSpeed;
    public float powerRegenRate;
    public float damageMultiplier;
    public int minEnemyCount;
    public int enemyLimit;
    [Header("Player Stats")]
    public int kills;

    void Start()
    {
        minSpawnTime = 3;
        maxSpawnTime = 6;
        struggleMinSpawnTime = 3;
        struggleMaxSpawnTime = 6;
        currentSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        currentPower = maxPower;
         slider.maxValue = maxPower; 
         chipSlider.maxValue = maxPower;
         currentPowerCool = powerCool;
        UpdatePower();
        ogColor = powerFill.color;
    }

    void OnEnable()
    {
        StartCoroutine(PToPauseNotif());
    }
    
    IEnumerator PToPauseNotif()
    {
        PToPause.SetActive(true);
        yield return new WaitForSeconds(10f);
        PToPause.GetComponent<Animator>().SetBool("no more", true);
        PToPause.SetActive(false);
    }

    public IEnumerator outOfPowerGents()
    {
        shooting.shakeAmount = 0.05f;
        shooting.shakeDuration = 0.25f;
        shooting.shaking = true;
        Color iloveRed = new Color(1f, 70f/255f, 56f/255f);
        powerFill.color = iloveRed;
        yield return new WaitForSeconds(0.075f);
        powerFill.color = ogColor;
        yield return new WaitForSeconds(0.075f);
        powerFill.color = iloveRed;
        yield return new WaitForSeconds(0.075f);
        powerFill.color = ogColor;
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
       speaker.UnPause();
       speaker2.UnPause();
       speaker3.UnPause();
       isPaused = false;
    }
    public void DylanMode()
    {
        maximumMaxPower = 75;
        minEnemyCount = 10;
        shooting.maxDistanceTillLoss = 7.5f;
        maxPowerUpAmount = 7.5f;
        powerUpAmount = 10;
        dylanMode = true;
        confirmation.SetText("the ocean grows warmer...");
        speaker2.PlayOneShot(musics[5]);
    }
    public void OpenAcessibilityMenu()
    {
        pauseMenuMenu.SetActive(false);
        accessMenu.SetActive(true);
    }
    
    public void CloseAcessibilityMenu()
    {
        pauseMenuMenu.SetActive(true);
        accessMenu.SetActive(false);
    }
    public void OpenControlMenu()
    {
        controlMenu.SetActive(true);
        accessMenu.SetActive(false);
    }
    public void CloseControlMenu()
    {
        controlMenu.SetActive(false);
        accessMenu.SetActive(true);
    }
    public void hushhushsecret()
    {
        accessMenu.SetActive(false);
        secretMenu.SetActive(true);
    }
    public void byebyeSecret()
    {
        accessMenu.SetActive(true);
        secretMenu.SetActive(false);
        confirmation.SetText(" ");
    }
    public void ControlSwappers()
    {
        if(scheme == 0)
        {
            controlScheme.SetText("Current Controls: Trackpad");
            controlReadOut.SetText("CURRENT CONTROLS: \n\nDASH - UP ARROW\nBRAKE - RIGHT ARROW\nSHOOT - LEFT ARROW\nSWAP - DOWN ARROW\nAIM - TRACKPAD");
            shooting.ChromebookMode();
            scheme = 1;
        }
        else
        {
            controlScheme.SetText("Current Controls: Mouseful");
            shooting.RegularMode();
            controlReadOut.SetText("CURRENT CONTROLS: \n\nDASH - RIGHT CLICK\nBRAKE - SPACE\nSHOOT - LEFT CLICK\nSWAP - TAB\nAIM - MOUSE");
            scheme = 0;
        }
    }

    void Pause()
    {
        if(!isTutorial)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            speaker.Pause();
            speaker2.Pause();
            speaker3.Pause();
            isPaused = true;
        }
    }

    void Update()
    {
        if(!spawning)
        {
            Clear();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(!isPaused)
            {
                Pause();
            }
            else
            {
                UnPause();
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!PToPause.activeSelf)
            {
                StartCoroutine(PToPauseNotif());
            }
        }
        currentPower = Mathf.Clamp(currentPower, 0, maxPower);
        maxPower = Mathf.Clamp(maxPower, 0, maximumMaxPower);

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
        if (powerUpUsed)
        {
            
            switch(powerUpType)
            {
                case 0:
                speaker2.PlayOneShot(musics[7]);
                shooting.PowerUpCollect(0);
                Battery();
                break;

                case 1:
                speaker2.PlayOneShot(musics[8]);
                shooting.PowerUpCollect(1);
                if (maxPower < 150)
                {
                    maxPower += maxPowerUpAmount;
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
        if(shooting.usedPower && !isTutorial)
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
    IEnumerator songStart()
    {
        speaker.Stop();
        yield return new WaitForSeconds(1);
        speaker.Stop();
        speaker.PlayOneShot(musics[21]);
    }
    void FixedUpdate()
    {
        anglerfishai[] anglers = mommy.GetComponentsInChildren<anglerfishai>();
        howManyAnglers = anglers.Length;
        if (currentPower < maxPower)
        {
            if(canRegen)
            {
                currentPower += Time.deltaTime * powerRegenRate;
            }
        }
        UpdatePower();
        if (Time.time >= currentSpawnTime)
        {
            if (spawning && mommy.childCount <= enemyLimit)
            {
                GameObject spawn = Instantiate(egg, spawns[Random.Range(0, spawns.Length)].position, Quaternion.identity); 
                spawn.GetComponent<egg>().Spawn(limitManage);
                currentSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);
            }
        }
        if(!isTutorial)
        {
            if(mommy.childCount < minEnemyCount)
            {
                if(spawning)
                {
                    GameObject spawn = Instantiate(egg, spawns[Random.Range(0, spawns.Length)].position, Quaternion.identity); 
                    spawn.GetComponent<egg>().Spawn(limitManage);
                }
            }
            if(anglerSpawns < howManyAnglers)
            {
                if(spawning)
                {
                    GameObject spawn = Instantiate(egg, spawns[Random.Range(0, spawns.Length)].position, Quaternion.identity); 
                    spawn.GetComponent<egg>().AnglerSpawn(limitManage);
                    anglerSpawns++;
                }
            }
            switch(currentDepthRounded)
        {
            case 0:
            textZone.SetText("Sunlight Zone");
            zone = 1;
            speaker.Stop();
            StartCoroutine(songStart());
            minSpawnTime = 3;
            maxSpawnTime = 6;
            currentMinSpawnTime = 3;
            currentMaxSpawnTime = 6;
            scrollSpeed = 0.5f;
            descentSpeed = 3.33f;
            limitManage = 1;
            shooting.recoveryBounce = 0.5f;
            DepthMeter.UpdateDepth(0);
            break;
            case 200:
            textZone.SetText("Twilight Zone");
            speaker.PlayOneShot(musics[16]);
            minSpawnTime = 2;
            maxSpawnTime = 5;
            currentMinSpawnTime = 2;
            currentMaxSpawnTime = 5;
            limitManage = 2;
            zone = 2;
            scrollSpeed = 0.6f;
            descentSpeed = 6.66f;
            shooting.recoveryBounce = 0.6f;
            DepthMeter.UpdateDepth(1);
            break;
            case 1000:
            speaker.PlayOneShot(musics[17]);
            textZone.SetText("Midnight Zone");
            zone = 3;
            scrollSpeed = 0.8f;
            descentSpeed = 25f;
            limitManage = 3;
            shooting.recoveryBounce = 0.7f;
            DepthMeter.UpdateDepth(2);
            break;
            case 4000:
            speaker.PlayOneShot(musics[18]);
            textZone.SetText("Abyssal Zone");
            zone = 4;
            minSpawnTime = 2;
            maxSpawnTime = 4;
            currentMinSpawnTime = 2;
            currentMaxSpawnTime = 4;
            scrollSpeed = 0.7f;
            descentSpeed = 16.66f;
            limitManage = 5;
            shooting.recoveryBounce = 0.8f;
            DepthMeter.UpdateDepth(3);
            break;
            case 6000:
            rocksObjects.SetActive(true);
            speaker.PlayOneShot(musics[19]);
            textZone.SetText("Hadal Zone");
            zone = 5;
            scrollSpeed = 0.9f;
            descentSpeed = 41.11f;
            shooting.recoveryBounce = 0.9f;
            DepthMeter.UpdateDepth(4);
            break;
            case 10500:
            speaker.PlayOneShot(musics[20]);
            break;
            case 10934:
            zone = 6;
            endBack.SetActive(true);
            textZone.SetText("CHALLENGER DEEP");
            StartCoroutine(boss());
            shooting.recoveryBounce = 0f;
            spawning = false;
            Clear();
            DepthMeter.UpdateDepth(5);
            break;
        }
        if(currentDepthRounded >= 10936)
        {
            if(spawning == true)
            {
                zone = 6;
                endBack.SetActive(true);
                textZone.SetText("CHALLENGER DEEP");
                StartCoroutine(boss());
                shooting.recoveryBounce = 0f;
                spawning = false;
                Clear();
                DepthMeter.UpdateDepth(5);
            }
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
        if(dylanMode == true && PlayerPrefs.GetInt("murder") >= 650)
        {
            PlayerPrefs.SetInt("murder", 80085);
        }
        else if(dylanMode && PlayerPrefs.GetInt("murder") <= 700)
        {
            PlayerPrefs.SetInt("murder", 123456);
        }
        PlayerPrefs.SetInt("murder", kills);
        Clear();
        buble.Stop();
        speaker.Stop();
        speaker2.Stop();
        speaker3.Stop();
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
        if(mommy.childCount > 1)
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
    }

    void UpdatePower()
    {
        float currentVelocity = 0;
        slider.value = Mathf.SmoothDamp(slider.value, currentPower, ref currentVelocity, powerLerp * Time.deltaTime);
        chipSlider.value = Mathf.SmoothDamp(chipSlider.value, maxPower, ref currentVelocity, powerLerp * Time.deltaTime);
    }

    public void hit(int dmgMultiplier)
    {
        speaker2.PlayOneShot(musics[10]);
        shooting.hit = true;
        if(shooting.canHurt)
        {
            if (maxPower - (5 * dmgMultiplier * damageMultiplier) > 0)
            {
                maxPower -= (5 * dmgMultiplier * damageMultiplier);
                if(maxPower <= 30)
                {
                    if(!concern)
                    {
                        concern = true;;
                        speaker3.Play();
                        lowPower.SetActive(true);
                        minSpawnTime = struggleMinSpawnTime;
                        maxSpawnTime = struggleMaxSpawnTime;
                    }
                }
                else
                {
                    concern = false;
                    minSpawnTime = currentMinSpawnTime;
                    maxSpawnTime = currentMaxSpawnTime;
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
        currentPower += powerUpAmount;
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
            totalfishmurderedondeath.SetText("TOTAL FISH MURDERED: " + kills.ToString());
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
         PlayerPrefs.SetInt("murder", 0);
         kills = 0;
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public void PlasticBagHit()
    {
         speaker.PlayOneShot(musics[Random.Range(12,14)], 0.1f);
    }
}

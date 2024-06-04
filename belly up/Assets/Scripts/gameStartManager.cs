using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class gameStartManager : MonoBehaviour
{
   public GameObject UI;
   public shooting shooting;
   public GameObject GameManager;
   public GameObject backDrop;
   public GameObject sub;
   public GameObject glock;
   public GameObject dummy;
   public GameObject startStuff;
   public Rigidbody2D CameraFollow;
   public bool transistion;
   public bool transistionTwo;
   public CanvasGroup starting;
   public CanvasGroup UIOPA;
   public float transSpeed;
   public GameObject realBackDrop;
   public Animator black;
   public GameObject bubble;
   public AudioSource audioSource;
   public AudioClip drop;
   public AudioClip drop2;
   public AudioClip fishGuidePress;
   public GameObject seaAmbience;
   public Button start;
   public Button fishGuideButton;
   public Button credits;
   [Header("Tip Of The Day")]
   [SerializeField]string[] tips;
   [SerializeField]int currentTip;
   [SerializeField]TextMeshProUGUI tipText;
   [Header("Roadmap Menu")]
   [SerializeField]GameObject roadmapMenu;
   [Header("Fish Guide")]
   [SerializeField]Animator fishGuide;
   [SerializeField]GameObject fishGuideObject;
   [Header("Very Cool Opening Anim")]
   public GameObject[] titleObjects;
   public Button classicButton;
   public Button tutorialButton;
   public Button backButton;
   public Button endlessButton;
   [SerializeField]Button playButton;
   public GameObject classic;
   public GameObject tutorial;
   public GameObject back;
   public GameObject ENDLESS;
   public GameObject versionNumber;
   public AudioClip cardFlip;
   public AudioClip buttonSelect;
   public AudioClip buttonPress;
   [Header("ENDLESS WOOOOOO")]
   public bool isEndless;
   [SerializeField]float barSmoothTime;
   public Color[] upgradeColors;
   public Slider[] barUpgrades;
   public Image[] fills;
   public Image[] backgrounds;
   public int maxPowerUpgradeInt;
   void Start()
   {
    versionNumber.SetActive(false);
    StartCoroutine(titleOpening());    
   }

   IEnumerator titleOpening()
   {
        for(int i=0; i<titleObjects.Length; i++)
        {
            titleObjects[i].SetActive(false);
        }
        yield return new WaitForSeconds(0.7f);
        for(int i=0; i<titleObjects.Length; i++)
        {
            yield return new WaitForSeconds(0.15f);
            titleObjects[i].SetActive(true);
            audioSource.PlayOneShot(cardFlip);
        }
        versionNumber.SetActive(true);
        seaAmbience.SetActive(true);
   }

   public void OpenGameModeMenu()
   {
        tutorial.SetActive(false);
        classic.SetActive(false);
        back.SetActive(false);
        ENDLESS.SetActive(false);
        backButton.interactable = true;
        tutorialButton.interactable = true;
        classicButton.interactable = true;
        endlessButton.interactable = true;
        playButton.interactable = false;  
        playButton.animator.SetTrigger("Pressed");
        audioSource.PlayOneShot(buttonPress);
        StartCoroutine(gameMenuOpen());   
   }

   public void UpgradeSomething(int upgradeID)
    {
        switch(upgradeID)
        {
            case 0:
            maxPowerUpgradeInt += 1;
            fills[0].color = upgradeColors[maxPowerUpgradeInt];
            Color stupidDumbBackground = upgradeColors[maxPowerUpgradeInt];
            stupidDumbBackground = new Color(stupidDumbBackground.r, stupidDumbBackground.g, stupidDumbBackground.b, 0.220f);
            backgrounds[0].color = stupidDumbBackground;
            barUpgrades[0].value += 0.067f;
            break;
        }
    }

   public void CloseGameModeMenu()
   {
        titleObjects[1].SetActive(false);
        titleObjects[2].SetActive(false);
        titleObjects[3].SetActive(false);
        credits.interactable = true;
        playButton.interactable = true;
        fishGuideButton.interactable = true;
        backButton.interactable = false;
        audioSource.PlayOneShot(cardFlip); 
        StartCoroutine(gameMenuClose());
        audioSource.PlayOneShot(buttonPress);
   }

   IEnumerator gameMenuClose()
   {
        yield return new WaitForSeconds(0.15f);
        tutorialButton.interactable = false;
        audioSource.PlayOneShot(cardFlip);
        if(classic.activeSelf)
        { 
          yield return new WaitForSeconds(0.15f);
          classicButton.interactable = false;
          audioSource.PlayOneShot(cardFlip);
        }
        if(ENDLESS.activeSelf)
        {
          yield return new WaitForSeconds(0.15f);
          endlessButton.interactable = false;
          audioSource.PlayOneShot(cardFlip);
        } 
        yield return new WaitForSeconds(0.15f);
        titleObjects[3].SetActive(true);
        audioSource.PlayOneShot(cardFlip); 
        yield return new WaitForSeconds(0.15f);
        titleObjects[2].SetActive(true);
        audioSource.PlayOneShot(cardFlip); 
        yield return new WaitForSeconds(0.15f);
        titleObjects[1].SetActive(true);
        audioSource.PlayOneShot(cardFlip); 
   }

   IEnumerator gameMenuOpen()
   {
        yield return new WaitForSeconds(0.15f);
        fishGuideButton.interactable = false;
        audioSource.PlayOneShot(cardFlip); 
        yield return new WaitForSeconds(0.15f); 
        credits.interactable = false;
        audioSource.PlayOneShot(cardFlip);
        yield return new WaitForSeconds(0.15f); 
        back.SetActive(true);
        audioSource.PlayOneShot(cardFlip);
        yield return new WaitForSeconds(0.15f); 
        tutorial.SetActive(true);
        audioSource.PlayOneShot(cardFlip);
        if(PlayerPrefs.GetInt("progress") > 0)
        {
          yield return new WaitForSeconds(0.15f);
          classic.SetActive(true);
          audioSource.PlayOneShot(cardFlip); 
        }
        if(PlayerPrefs.GetInt("progress") > 1)
        {
          yield return new WaitForSeconds(0.15f);
          ENDLESS.SetActive(true);
          audioSource.PlayOneShot(cardFlip);
        }
   }

   public void FishGuide()
   {
        audioSource.PlayOneShot(buttonPress);
        fishGuideObject.SetActive(true);
        fishGuide.SetBool("fishGuide", false);
        
   }

   public void StartGame()
   {
     start.enabled = false;
     StartCoroutine(startIt());
     PlayerPrefs.SetInt("lastTip", currentTip);
     endlessButton.interactable = false;
     classicButton.interactable = false;
     backButton.interactable = false;
     tutorialButton.interactable = false;
   }

   void OnEnable()
   {
    TipOfTheDay();
   }

   void TipOfTheDay()
   {
    while(currentTip == PlayerPrefs.GetInt("lastTip"))
    {
        currentTip = Random.Range(0, tips.Length);
    }
    currentTip = Random.Range(0, tips.Length);
    tipText.SetText(tips[currentTip]);
   }

   void Update()
   {
        if (transistion)
        {
            starting.alpha = Mathf.Lerp(starting.alpha, 0, Time.deltaTime * transSpeed);
        }
        if(transistionTwo)
        {
            UIOPA.alpha = Mathf.Lerp(UIOPA.alpha, 1, Time.deltaTime * transSpeed);
        }
   }

   public void Tutorial()
   {
        StartCoroutine(tutorialIt("tutorial 1"));
        audioSource.PlayOneShot(buttonPress);
        endlessButton.interactable = false;
        classicButton.interactable = false;
        backButton.interactable = false;
        tutorialButton.interactable = false;
   }

   public void Credits()
   {
     StartCoroutine(tutorialIt("thank"));
     audioSource.PlayOneShot(buttonPress);
   }

   public void RoadmapMenu()
   {
        if(roadmapMenu.activeSelf)
        {
            roadmapMenu.SetActive(false);
        }
        else
        {
            roadmapMenu.SetActive(true);
        }
   }

   IEnumerator tutorialIt(string scene)
   {
     black.SetBool("trans", true);
         yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
   }

   IEnumerator startIt()
   {
    start.animator.SetTrigger("Pressed");
    seaAmbience.SetActive(false);
    audioSource.PlayOneShot(drop);
    audioSource.PlayOneShot(drop2);
    GameManager.GetComponent<gamemanager>().canLose = false;
    transistion = true;
    GameManager.GetComponent<gamemanager>().spawning = false;
   dummy.GetComponent<Rigidbody2D>().gravityScale = 1;
   GameManager.SetActive(true);
    CameraFollow.bodyType = RigidbodyType2D.Dynamic;
    yield return new WaitForSeconds(1.2f);
    transistionTwo = true;
    bubble.SetActive(true);
    GameManager.GetComponent<gamemanager>().spawning = true;
     CameraFollow.bodyType = RigidbodyType2D.Static;
    glock.transform.position = dummy.transform.position;
    glock.SetActive(true);
    sub.SetActive(true);
    shooting.player.AddForce(-transform.up * 1f,ForceMode2D.Impulse);
    UI.SetActive(true);
    GameManager.SetActive(true);
    shooting.control = true;
    yield return null;
    dummy.SetActive(false);
    backDrop.SetActive(true);
    realBackDrop.SetActive(false);
    yield return new WaitForSeconds(1f);
    startStuff.SetActive(false);
    yield return new WaitForSeconds(3f);
    GameManager.GetComponent<gamemanager>().canLose = true;
    audioSource.Stop();
    Destroy(gameObject);
   }
}

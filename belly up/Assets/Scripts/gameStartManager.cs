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
   public Button start;
   [Header("Tip Of The Day")]
   [SerializeField]string[] tips;
   [SerializeField]int currentTip;
   [SerializeField]TextMeshProUGUI tipText;
   [Header("Roadmap Menu")]
   [SerializeField]GameObject roadmapMenu;
   [Header("Tutorial Check")]
   [SerializeField]TextMeshProUGUI startText;
   [SerializeField]Button startButton;
   [Header("Fish Guide")]
   [SerializeField]Animator fishGuide;
   [SerializeField]GameObject fishGuideObject;

   void Start()
   {
    if(PlayerPrefs.GetInt("tutorial") == 0)
    {   
        startText.color = new Color(0.16f, 0.16f, 0.16f, 0.443f);
        startButton.interactable = false;
    }
    else
    {
        startText.color = new Color(0.16f, 0.16f, 0.16f, 1f);
        startButton.interactable = true;
    }    
   }

   public void FishGuide()
   {
        fishGuideObject.SetActive(true);
        fishGuide.SetBool("fishGuide", false);
        
   }

   public void StartGame()
   {
    start.enabled = false;
    StartCoroutine(startIt());
    PlayerPrefs.SetInt("lastTip", currentTip);
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
        StartCoroutine(tutorialIt());
        PlayerPrefs.SetInt("tutorial", 0);
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

   IEnumerator tutorialIt()
   {
     black.SetBool("trans", true);
         yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(3);
   }

   IEnumerator startIt()
   {
    audioSource.Stop();
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

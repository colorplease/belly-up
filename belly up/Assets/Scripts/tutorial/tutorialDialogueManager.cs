using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]
public class Dialogue
{
    public string dialogueText;
    public bool userInteractable;
    public bool notTest;
    public int id;
}

public class tutorialDialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public Dialogue[] lines;
    public Dialogue[] linesTrackpad;
    public float textSpeed;
    private int index;
    [SerializeField]shooting shoot;
    [SerializeField]Animator helpAnimator;
    [SerializeField]Animator skiptitle;
    [SerializeField]Animator skiptitleflavor;
    Coroutine helpmeTimer = null;
    [Header("General Tutorial Stuff")]
    public int tutorialScore;
    public GameObject[] checkmeboxes;
    bool cumplete;
    [SerializeField]Animator checkmeboxesParent;
    [SerializeField]Animator gunSwapUI;
    [SerializeField]Transform fishSpawn;
    [SerializeField]GameObject dummy;
    [SerializeField]Transform[] dummySpawnPoints;
    [SerializeField]Animator energyUI;
    [Header("Aim Tutorial Reqs")]
    float lastGlockRotation;
    //used for other reqs btw 
    public float currentAimScore;
    public float aimReq;
    [Header("Shoot Tutorial Basic Reqs")]
    public float shootBasicReq;
    [Header("Shoot Tutorial Dummy Single Reqs")]
    //also used for the shotgun tutorial
    public float shootDummySingleReq;
    [Header("Dash Tutorial")]
    public float dashTutorialReq;
    [Header("Brake Tutorial")]
    public float brakeTutorialReq;
    [Header("Power Intro")]
    public GameObject tutorialFish;
    [SerializeField]gamemanager gameManager;
    [Header("Power Up Tutorial")]
    public GameObject[] powerups;
    public float powerUpReq;
    [Header("Final Trial")]
    public int lastMurderCount;
    public float murderReq;
    public tutorialManager leavers;
    
    

    // Start is called before the first frame update
    void Start()
    {
        textComponent.SetText("");
        if(PlayerPrefs.GetInt("InputMode") == 1)
        {
            lines = linesTrackpad;
        }
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            skiptitle.SetBool("bad", true);
            skiptitleflavor.SetBool("bad", true);
            if(textComponent.text == lines[index].dialogueText)
            {
                NextLine();
            }
            else
            {
            StopAllCoroutines();
            textComponent.SetText("");
            textComponent.text = lines[index].dialogueText;
            if(!lines[index].userInteractable)
            {
                ExecuteAction(); 
                if(cumplete)
                {
                    NextLine();
                }
            }
            else
            {
                helpmeTimer = StartCoroutine(needHelpTimer());
            }
            }
        }
        if(!lines[index].userInteractable)
        {
            switch(lines[index].id)
            {
                case 1:
                AimTutorialCheck();
                break;
                case 2:
                ShootBasicTutorialCheck();
                break;
                case 3:
                ShootDummySingleTutorialCheck();
                break;
                case 4:
                ShootDummySingleTutorialCheck();
                break;
                case 5:
                DashTutorialCheck();
                break;
                case 6:
                BrakeTutorialCheck();
                break;
                case 7:
                ShootDummySingleTutorialCheck();
                break;
                case 10:
                PowerUpTutorial();
                break;
                case 11:
                FinalTrialTutorial();
                break;
            }
        }
    }

    void FinalTrialTutorial()
    {
        if(gameManager.kills > lastMurderCount)
        {
            currentAimScore++;
            lastMurderCount = gameManager.kills;
        }
        else
        {
            lastMurderCount = gameManager.kills;
        }
        if(currentAimScore >= murderReq)
        {
            if(tutorialScore < 2)
            {
                checkmeboxes[tutorialScore].GetComponent<Animator>().SetBool("done", true);
                tutorialScore++;
                currentAimScore = 0;
            }
            else
            {
                checkmeboxes[tutorialScore].GetComponent<Animator>().SetBool("done", true);
                StartCoroutine(Complete());
                tutorialScore = 0;
                gameManager.spawning = false;
                PlayerPrefs.SetInt("murder", 0);
            }
        }

    }

    void PowerUpTutorial()
    {
        if(fishSpawn.childCount == 0)
        {
            Instantiate(powerups[Random.Range(0, powerups.Length)], dummySpawnPoints[Random.Range(0, dummySpawnPoints.Length)].position, Quaternion.Euler(0f, 0f, Random.Range(0,360)),fishSpawn);
        }
        if(shoot.myFavPowerUp)
        {
            currentAimScore++;
            shoot.myFavPowerUp = false;
        }
        if(currentAimScore >= powerUpReq)
        {
            if(tutorialScore < 2)
            {
                checkmeboxes[tutorialScore].GetComponent<Animator>().SetBool("done", true);
                tutorialScore++;
                currentAimScore = 0;
            }
            else
            {
                checkmeboxes[tutorialScore].GetComponent<Animator>().SetBool("done", true);
                StartCoroutine(powerUpClean());
                StartCoroutine(Complete());
            }
        }

    }

    IEnumerator powerUpClean()
    {
        yield return new WaitForSeconds(0.5f);
        powerUp scam = fishSpawn.GetChild(0).gameObject.GetComponent<powerUp>();
        scam.Collected();
    }

    void AimTutorialCheck()
    {
        currentAimScore = Mathf.Abs(lastGlockRotation - shoot.transform.rotation.z) + currentAimScore;
        lastGlockRotation = shoot.transform.rotation.z;
        if(currentAimScore >= aimReq)
        {
            if(tutorialScore < 2)
            {
                checkmeboxes[tutorialScore].GetComponent<Animator>().SetBool("done", true);
                tutorialScore++;
                currentAimScore = 0;
            }
            else
            {
                checkmeboxes[tutorialScore].GetComponent<Animator>().SetBool("done", true);
                StartCoroutine(Complete());
                tutorialScore = 0;
            }
        }
        
    }

    void ShootBasicTutorialCheck()
    {
         if(Input.GetKeyDown(shoot.shoot))
         {
            currentAimScore++;
         }
          if(currentAimScore >= aimReq)
        {
            if(tutorialScore < 2)
            {
                checkmeboxes[tutorialScore].GetComponent<Animator>().SetBool("done", true);
                tutorialScore++;
                currentAimScore = 0;
            }
            else
            {
                checkmeboxes[tutorialScore].GetComponent<Animator>().SetBool("done", true);
                StartCoroutine(Complete());
            }
        }
    }

    void ShootDummySingleTutorialCheck()
    {
        if(fishSpawn.childCount == 0)
        {
            Instantiate(dummy, dummySpawnPoints[Random.Range(0, dummySpawnPoints.Length)].position, Quaternion.Euler(0f, 0f, Random.Range(0,360)),fishSpawn);
        }
        if(currentAimScore >= shootDummySingleReq)
        {
            if(tutorialScore < 2)
            {
                checkmeboxes[tutorialScore].GetComponent<Animator>().SetBool("done", true);
                tutorialScore++;
                currentAimScore = 0;
                
            }
            else
            {
                StartCoroutine(cleanUpDummy());
                checkmeboxes[tutorialScore].GetComponent<Animator>().SetBool("done", true);
                StartCoroutine(Complete());
            }
        }
    }

    IEnumerator cleanUpDummy()
    {
        yield return new WaitForSeconds(0.5f);
        dummyTarget scam = fishSpawn.GetChild(0).gameObject.GetComponent<dummyTarget>();
        scam.Hit();
    }

    void DashTutorialCheck()
    {
        if (Input.GetKeyDown(shoot.dash))
        {
            currentAimScore++;
        }
        if(currentAimScore >= dashTutorialReq)
        {
            if(tutorialScore < 2)
            {
                checkmeboxes[tutorialScore].GetComponent<Animator>().SetBool("done", true);
                tutorialScore++;
                currentAimScore = 0;
            }
            else
            {
                shoot.canDash = false;
                shoot.arrow.SetActive(false);
                checkmeboxes[tutorialScore].GetComponent<Animator>().SetBool("done", true);
                StartCoroutine(Complete());
            }
        }
    }

    void BrakeTutorialCheck()
    {
        if(Input.GetKeyDown(shoot.brake) && shoot.rb.velocity.sqrMagnitude > 5)
        {
            currentAimScore++;
        }
        if(currentAimScore >= brakeTutorialReq)
        {
            if(tutorialScore < 2)
            {
                checkmeboxes[tutorialScore].GetComponent<Animator>().SetBool("done", true);
                tutorialScore++;
                currentAimScore = 0;
            }
            else
            {
                checkmeboxes[tutorialScore].GetComponent<Animator>().SetBool("done", true);
                StartCoroutine(Complete());
            }
        }
    }

    public void ScoreBoostSingleTutorialCheck()
    {
        if(cumplete != true)
        {
            currentAimScore++;
        }
    }

    IEnumerator Complete()
    {
        tutorialScore = 0;
        yield return new WaitForSeconds(0.5f);
        checkmeboxesParent.SetBool("here", false);
        cumplete = true;
        currentAimScore = 0;
        for(int i = 0; i < checkmeboxes.Length; i++)
        {
            checkmeboxes[i].GetComponent<Animator>().SetBool("done", false);
        }
        shoot.control = false;
        shoot.isTutorialReal = true;
        NextLine();
    }
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        if (helpmeTimer != null)
        {
            StopCoroutine(helpmeTimer);
        }
        helpAnimator.SetBool("help", false);
        if(index < lines.Length - 1)
        {
            if(lines[index].userInteractable == true || cumplete == true)
            {
                index++;
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine());
                cumplete = false;
            }
        }
        else
        {
            //hi hi
        }
    }

    void ExecuteAction()
    {
        if (helpmeTimer != null)
        {
            StopCoroutine(helpmeTimer);
        }
        tutorialScore = 0;
        if(!lines[index].notTest)
        {
            checkmeboxesParent.SetBool("here", true);
            shoot.control = true;
        }
        switch(lines[index].id)
        {
            case 1:
            shoot.canAim = true;
            break;
            case 2:
            shoot.canShoot = true;
            shoot.canKB = false;
            break;
            case 4:
            shoot.canSwap = true;
            gunSwapUI.SetBool("canvasg", true);
            break;
            case 5:
            shoot.canDash = true;
            break;
            case 6:
            shoot.canDash = true;
            shoot.canBrake = true;
            shoot.isTutorialReal = false;
            break;
            case 7:
            shoot.canDash = true;
            shoot.isTutorialReal = false;
            shoot.canKB = true;
            break;
            case 8:
            StartCoroutine(powerIntro());
            break;
            case 9:
            shoot.canHurt = true;
            Instantiate(tutorialFish, gameManager.spawns[Random.Range(0, gameManager.spawns.Length)].position, Quaternion.identity);
            StartCoroutine(hitCheckTutorial());
            break;
            case 11:
            gameManager.spawning = true;
            shoot.isTutorialReal = false;
            PlayerPrefs.SetInt("murder", 0);
            lastMurderCount = 0;
            break;
            case 12:
            StartCoroutine(ends());
            break;
        }
    }

    IEnumerator ends()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(leavers.leave());
    }

    IEnumerator powerIntro()
    {
        gameManager.currentPower = 30;
        energyUI.SetBool("powerUp", true);
        yield return new WaitForSeconds(0.25f);
        shoot.control = false;
        shoot.isTutorialReal = false;
        lines[index].userInteractable = true;
        helpmeTimer = StartCoroutine(needHelpTimer());

    }

    IEnumerator hitCheckTutorial()
    {
        yield return new WaitForSeconds(5);
        shoot.control = false;
        shoot.isTutorialReal = false;
        lines[index].userInteractable = true;
        helpmeTimer = StartCoroutine(needHelpTimer());
    }

    IEnumerator TypeLine()
    {
        //types out each char one by one
        foreach(char c in lines[index].dialogueText.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        if(!lines[index].userInteractable)
        {
            ExecuteAction(); 
        }
        else
        {
            helpmeTimer = StartCoroutine(needHelpTimer());
        }
    }

    IEnumerator needHelpTimer()
    {
        yield return new WaitForSeconds(3);
        helpAnimator.SetBool("help", true);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]
public class Dialogue
{
    public string dialogueText;
    public bool userInteractable;
    public int id;
}

public class tutorialDialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public Dialogue[] lines;
    public float textSpeed;
    private int index;
    [SerializeField]shooting shoot;
    [SerializeField]Animator helpAnimator;
    [Header("General Tutorial Stuff")]
    public int tutorialScore;
    public GameObject[] checkmeboxes;
    bool cumplete;
    [SerializeField]Animator checkmeboxesParent;
    [SerializeField]Transform fishSpawn;
    [SerializeField]GameObject dummy;
    [SerializeField]Transform[] dummySpawnPoints;
    [Header("Aim Tutorial Reqs")]
    float lastGlockRotation;
    //used for other reqs btw 
    public float currentAimScore;
    public float aimReq;
    [Header("Shoot Tutorial Basic Reqs")]
    public float shootBasicReq;
    [Header("Shoot Tutorial Dummy Single Reqs")]
    public float shootDummySingleReq;
    

    // Start is called before the first frame update
    void Start()
    {
        textComponent.SetText("");
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
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
            }
            else
            {
                StartCoroutine(needHelpTimer());
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
            }
        }
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
         if(Input.GetButtonDown("Fire1"))
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
        NextLine();
    }
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        helpAnimator.SetBool("help", false);
        StopAllCoroutines();
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
        StopCoroutine(needHelpTimer());
        tutorialScore = 0;
        checkmeboxesParent.SetBool("here", true);
        switch(lines[index].id)
        {
            case 1:
            shoot.canAim = true;
            break;
            case 2:
            shoot.canShoot = true;
            shoot.canKB = false;
            break;
        }
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
            StartCoroutine(needHelpTimer());
        }
    }

    IEnumerator needHelpTimer()
    {
        yield return new WaitForSeconds(3);
        helpAnimator.SetBool("help", true);

    }
}

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
    [Header("General Tutorial Stuff")]
    public int tutorialScore;
    public GameObject[] checkmeboxes;
    bool cumplete;
    [SerializeField]Animator checkmeboxesParent;
    [Header("Aim Tutorial Reqs")]
    float lastGlockRotation;
    public float currentAimScore;
    public float aimReq;
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
            }
        }
        if(!lines[index].userInteractable)
        {
            switch(lines[index].id)
            {
                case 1:
                AimTutorialCheck();
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
            }
        }
        
    }

    IEnumerator Complete()
    {
        yield return new WaitForSeconds(0.5f);
        checkmeboxesParent.SetBool("here", false);
        cumplete = true;
        NextLine();
    }
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            if(lines[index].userInteractable == true || cumplete == true)
            {
                StopCoroutine(TypeLine());
                index++;
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine());
            }
        }
        else
        {
            //hi hi
        }
    }

    void ExecuteAction()
    {
        checkmeboxesParent.SetBool("here", true);
        switch(lines[index].id)
        {
            case 1:
            shoot.canAim = true;
            break;
        }
    }

    IEnumerator TypeLine()
    {;
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
    }
}

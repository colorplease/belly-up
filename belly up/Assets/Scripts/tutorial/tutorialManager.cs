using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class tutorialManager : MonoBehaviour
{
    public Animator black;
    public bool skippers;
    [SerializeField]TextMeshProUGUI murder;
    [SerializeField]GameObject[] creditThings;
    [SerializeField]GameObject currentCreditThing;
    [SerializeField]GameObject leftArrow, rightArrow;
    [SerializeField]int index;

    void Start()
    {
        if(!skippers)
        {
            index = creditThings.Length - 1;
            currentCreditThing = creditThings[creditThings.Length - 1];
            if(PlayerPrefs.GetInt("murder") == 80085)
            {
                murder.SetText("you truly have no enemies.");
            }
            if(PlayerPrefs.GetInt("murder") == 123456)
            {
                murder.SetText("ACTIVATE DYLAN MODE AT THE BEGINNING OR NOT AT ALL. WEAK.");
                murder.fontSize = 64.11f;
            }
            else
            {
                murder.SetText("TOTAL FISH MURDERED: " + PlayerPrefs.GetInt("murder").ToString());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if(Input.GetKey(KeyCode.K))
            {
                if(Input.GetKey(KeyCode.M))
                {
                    if(skippers)
                    {
                        StartCoroutine(leave());
                    }
                    else
                    {
                        StartCoroutine(tutorialIt());
                    }
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!skippers)
            {
                StartCoroutine(leave());
            }
        }
    }
     
     IEnumerator tutorialIt()
   {
     black.SetBool("trans", true);
         yield return new WaitForSeconds(1f);
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
   }

   public IEnumerator leave()
   {
        black.SetBool("trans", true);
        yield return new WaitForSeconds(1f);
        PlayerPrefs.SetInt("tutorial", 1);
        PlayerPrefs.SetInt("murder", 0);
        SceneManager.LoadScene("SampleScene");
   }

   public void DecreaseCredits()
   {
    if(index - 1 > 0)
    {
        index--;
        currentCreditThing.SetActive(false);
        currentCreditThing = creditThings[index];
        currentCreditThing.SetActive(true);
    }
    else
    {
        index--;
        currentCreditThing.SetActive(false);
        currentCreditThing = creditThings[index];
        currentCreditThing.SetActive(true);
        rightArrow.SetActive(false);
        leftArrow.SetActive(true);
    }
   }

   public void IncreaseCredits()
   {
    if(index + 1 < (creditThings.Length - 1))
    {
        index++;
        currentCreditThing.SetActive(false);
        currentCreditThing = creditThings[index];
        currentCreditThing.SetActive(true);
    }
    else
    {
        index++;
        currentCreditThing.SetActive(false);
        currentCreditThing = creditThings[index];
        currentCreditThing.SetActive(true);
        leftArrow.SetActive(false);
        rightArrow.SetActive(true);
    }
   }
}

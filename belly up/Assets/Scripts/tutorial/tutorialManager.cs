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

    void Start()
    {
        if(!skippers)
        {
            murder.SetText("TOTAL FISH MURDERED: " + PlayerPrefs.GetInt("murder").ToString());
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
        SceneManager.LoadScene(0);
   }
}

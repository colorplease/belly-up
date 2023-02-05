using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorialManager : MonoBehaviour
{
    public Animator black;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if(Input.GetKey(KeyCode.K))
            {
                if(Input.GetKey(KeyCode.M))
                {
                    StartCoroutine(tutorialIt());
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(leave());
        }
    }
     
     IEnumerator tutorialIt()
   {
     black.SetBool("trans", true);
         yield return new WaitForSeconds(1f);
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
   }

   IEnumerator leave()
   {
     black.SetBool("trans", true);
         yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
   }
}

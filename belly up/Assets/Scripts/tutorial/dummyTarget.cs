using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyTarget : MonoBehaviour
{
    [SerializeField]Rigidbody2D rb;
    tutorialDialogueManager tutorialmanager;
    public void Hit()
    {
        tutorialmanager = GameObject.FindWithTag("dialogueManager").GetComponent<tutorialDialogueManager>();
        tutorialmanager.ScoreBoostSingleTutorialCheck();
        StartCoroutine(FadeTo(0f, 0.2f));
        StartCoroutine(death());
    }

    IEnumerator death()
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    IEnumerator FadeTo(float aValue, float aTime)
     {
         float alpha = transform.GetComponent<SpriteRenderer>().color.a;
         for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
         {
             Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
             transform.GetComponent<SpriteRenderer>().color = newColor;
             yield return null;
         }
     }
}

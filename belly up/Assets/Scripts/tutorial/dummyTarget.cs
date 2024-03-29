using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyTarget : MonoBehaviour
{
    [SerializeField]Rigidbody2D rb;
    [SerializeField]Collider2D meCollider;
    tutorialDialogueManager tutorialmanager;
    bool tagged;
    bool dying;
    bool hitting;
    gamemanager gameManager;
    public void Hit()
    {
        tutorialmanager = GameObject.FindWithTag("dialogueManager").GetComponent<tutorialDialogueManager>();
        if(!tagged)
        {
            tutorialmanager.ScoreBoostSingleTutorialCheck();
        }
        tagged = true;
        StartCoroutine(FadeTo(0f, 0.2f));
        StartCoroutine(death());
        meCollider.enabled = false;
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

     void OnTriggerEnter2D(Collider2D other)
     {
        if (other.tag == "Player")
        {
            if (!hitting && !dying)
            {
                gameManager = GameObject.FindWithTag("GameManager").GetComponent<gamemanager>();
                hitting = true;
                gameManager.hit(0);
                StartCoroutine(FadeTo(0f, 1f));
                StartCoroutine(death());
            }
        }
     }
}

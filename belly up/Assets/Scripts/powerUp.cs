using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour
{
    public int type;

    void OnEnable()
    {
        shooting publicShooting = GameObject.FindWithTag("shoot").GetComponent<shooting>();
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.up * publicShooting .recoveryBounce * publicShooting.recoveryBounceMultiplier, ForceMode2D.Impulse);
        
    }
   void OnTriggerEnter2D(Collider2D other)
     {
        if (other.tag == "Player")
        {
            gamemanager gameManager = GameObject.FindWithTag("GameManager").GetComponent<gamemanager>();
            gameManager.powerUpType = type;
            gameManager.powerUpUsed = true;
            StartCoroutine(FadeTo(0f, 0.5f));
            StartCoroutine(death());
        }
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

     IEnumerator deathTimer()
     {
        yield return new WaitForSeconds(5);
        StartCoroutine(FadeTo(0f, 0.5f));
        StartCoroutine(death());

     }
    
     IEnumerator death()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyTarget : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    [SerializeField]Vector2 targetPos;
    SpriteRenderer spriteRenderer;
    bool doNotSpawnHere;
    [SerializeField]Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        //make dummy invisible while calculating position
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        //let's get our position jits, and brute force calculations for the best spawn
        targetPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);
        while(doNotSpawnHere == true)
        {
            targetPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);
        }
        //Set position and random rotation
        if(doNotSpawnHere == false)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Random.Range(0,360));
            spriteRenderer.enabled = true;
        }
        else
        {
            while(doNotSpawnHere == true)
            {
                targetPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);
            }
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Random.Range(0,360));
            spriteRenderer.enabled = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "DummyExclude")
        {
            doNotSpawnHere = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "DummyExclude")
        {
            doNotSpawnHere = false;
        }
    }

    public void Hit()
    {
        StartCoroutine(FadeTo(0f, 0.5f));
        StartCoroutine(death());
    }

    IEnumerator death()
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1);
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

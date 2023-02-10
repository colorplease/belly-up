using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blobfishai : MonoBehaviour
{
    [SerializeField]Transform amongUs;
    [SerializeField]Rigidbody2D rb;
    public float speed;
    public float HP;
    gamemanager gameManager;
    public GameObject underlings;
    public int dupeNumber;
    bool dying;
    bool hitting;
    public GameObject[] powerUps;
    public GameObject splitParticle;
    [SerializeField]float time;

    void Start()
    {
        amongUs = GameObject.FindWithTag("Player").transform;
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<gamemanager>();
    }
    void FixedUpdate()
    {
        blobFish();
    }

    void blobFish()
    {
        rb.AddRelativeForce(Vector2.right* speed, ForceMode2D.Force);
        Vector3 dir = amongUs.position - transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void hit(float dmg)
    {
        if(!dying)
        {
            HP -= dmg;
            StartCoroutine(flash());
            rb.velocity = Vector2.zero;
            if (HP <= 0)
            {
                die();
                PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
                collider.enabled = false;
                dying = true;
            }
        }
    }
    void Generate()
   {
    var chance = Random.Range(0, 4);
    if (chance < 3)
    {
        Instantiate(powerUps[chance], transform.position, Quaternion.identity);
    }
   }
    

    void die()
    {
        if (!dying)
        {
            Generate();
            if (dupeNumber < 1)
        {
            for(int i = 0; i<Random.Range(2,4);i++)
         {
            StartCoroutine(summon());
         }
        }
        else
        {
            StartCoroutine(FadeTo(0f, 1f));
            StartCoroutine(death());
        }

        }
    }
   

    IEnumerator summon()
    {
        StartCoroutine(split());
        GameObject splitEffect = Instantiate(splitParticle, transform.position,Quaternion.identity);
        Destroy(splitEffect, 5f);
        yield return new WaitForSeconds(0.25f);
        GameObject fish = Instantiate(underlings, new Vector2(transform.position.x + Random.Range(-0.3f, 0.3f), transform.position.y + Random.Range(-0.1f, 0.1f)), Quaternion.identity);
        fish.GetComponent<SpriteRenderer>().enabled = true;
        fish.transform.localScale = new Vector2(transform.localScale.x * 0.75f, transform.localScale.y  * 0.75f);
        Color transparencyFix = new Color(1f, 1f, 1f, 1f);
        fish.GetComponent<SpriteRenderer>().enabled = true;
        fish.GetComponent<PolygonCollider2D>().enabled = true;
        fish.GetComponent<SpriteRenderer>().color = transparencyFix;
        fish.GetComponent<blobfishai>().HP = 2;
        fish.GetComponent<blobfishai>().speed = speed * Random.Range(2,4);
        fish.GetComponent<blobfishai>().dupeNumber += 1;
        Destroy(gameObject, 0.1f);
    }

    IEnumerator split()
    {
        yield return new WaitForSeconds(time);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

     IEnumerator flash()
    {
        SpriteRenderer colorMe = gameObject.GetComponent<SpriteRenderer>();
        colorMe.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        colorMe.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        colorMe.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        colorMe.color = Color.white;
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

     void OnTriggerEnter2D(Collider2D other)
     {
        if (other.tag == "Player")
        {
            if (!hitting && !dying)
            {
                hitting = true;
                gameManager.hit();
                StartCoroutine(FadeTo(0f, 1f));
                StartCoroutine(death());
            }
            
        }
     }
}

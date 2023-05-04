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
    [SerializeField]float maxSpeed;
    bool hitting;
    public GameObject[] powerUps;
    public GameObject splitParticle;
    [SerializeField]float time;
    [SerializeField]float minChance;
    [SerializeField]float maxChance = 150;
    [SerializeField]float realChance;
    [SerializeField]float deathSpinSpeed;
    float dropPowerChance;

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
        if(!dying)
        {
            if(rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
            }
            rb.AddRelativeForce(Vector2.right* speed, ForceMode2D.Force);
            Vector3 dir = amongUs.position - transform.position;
            float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            float angle = transform.rotation.z + 50;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * deathSpinSpeed);
        }
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
                PolygonCollider2D collider = GetComponentInChildren<PolygonCollider2D>();
                collider.enabled = false;
                dying = true;
            }
        }
    }
    void Generate()
   {
    if(!gameManager.dylanMode)
    {
        realChance = gameManager.maxPower - 50;
        dropPowerChance = Random.Range(minChance, maxChance);
    }
    else
    {
        realChance = 125;
        dropPowerChance = Random.Range(minChance, maxChance);
    }
    if (dropPowerChance >= realChance)
    {
        var chance = Random.Range(0, 4);
        Instantiate(powerUps[chance], transform.position, Quaternion.identity);
    }
   }
    

    void die()
    {
        gameManager.kills += 1;
        if (!dying)
        {
            Generate();
            PolygonCollider2D collider = GetComponentInChildren<PolygonCollider2D>();
            collider.enabled = false;
            if (dupeNumber < 1)
        {
            for(int i = 0; i<Random.Range(2,4);i++)
         {
            StartCoroutine(summon());
         }
        }
        else
        {
            StartCoroutine(FadeTo(0f, 0.5f));
            StartCoroutine(death());
        }

        }
    }
   

    IEnumerator summon()
    {
        StartCoroutine(split());
        GameObject splitEffect = Instantiate(splitParticle, transform.position,Quaternion.identity);
        Destroy(splitEffect, 5f);
        StartCoroutine(FadeTo(0f, 1.25f));
        yield return new WaitForSeconds(0.25f);
        GameObject fish = Instantiate(underlings, new Vector2(transform.position.x + Random.Range(-0.3f, 0.3f), transform.position.y + Random.Range(-0.1f, 0.1f)), Quaternion.identity);
        fish.GetComponent<SpriteRenderer>().enabled = true;
        fish.transform.localScale = new Vector2(transform.localScale.x * 0.75f, transform.localScale.y  * 0.75f);
        Color transparencyFix = new Color(1f, 1f, 1f, 1f);
        fish.GetComponent<SpriteRenderer>().enabled = true;
        fish.GetComponentInChildren<PolygonCollider2D>().enabled = true;
        fish.GetComponent<SpriteRenderer>().color = transparencyFix;
        fish.GetComponent<blobfishai>().HP = Random.Range(1, 3);
        fish.GetComponent<blobfishai>().speed = speed * Random.Range(2,4);
        fish.GetComponent<blobfishai>().dupeNumber += 1;
        Destroy(gameObject, 1f);
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

     void OnCollisionEnter2D(Collision2D other)
     {
        if (other.collider.tag == "Player")
        {
            if (!hitting &&  !dying)
            {
                hitting  = true;
                gameManager.hit(1);
                StartCoroutine(FadeTo(0f, 0.5f));
                StartCoroutine(death());
            }
        }
     }

     void OnTriggerExit2D(Collider2D other)
     {
        if(other.tag == "bluepikmin")
        {
                StartCoroutine(FadeTo(0f, 0.5f));
                StartCoroutine(death());
        }
     }
}

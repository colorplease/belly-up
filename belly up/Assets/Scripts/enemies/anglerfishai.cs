using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class anglerfishai : MonoBehaviour
{
     [SerializeField]Transform amongUs;
    [SerializeField]Rigidbody2D rb;
    public float speed;
    public float HP;
    gamemanager gameManager;
    [SerializeField]UnityEngine.Rendering.Universal.Light2D light; 
    bool hitting;
    public GameObject[] powerUps;
    bool dying;
    [SerializeField]float maxSpeed;
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
        anglerFish();
    }

    void anglerFish()
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

    public void hit(float dmg)
    {
        if(!dying)
        {
            HP -= dmg;
            StartCoroutine(flash());
            rb.velocity = Vector2.zero;
            if (HP <= 0)
            {
                PolygonCollider2D collider = GetComponentInChildren<PolygonCollider2D>();
                collider.enabled = false;
                die();
                dying = true;
            }
        }
    }

    void die()
    {
        PlayerPrefs.SetInt("murder", PlayerPrefs.GetInt("murder") + 1);
        dying = true;
        Generate();
        StartCoroutine(FadeTo(0f, 0.5f));
        StartCoroutine(death());
        StartCoroutine(FadeLight(0f, 0.5f));
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

     IEnumerator FadeLight(float aValue, float aTime)
     {
        float intensity = light.intensity;
        for (float t= 0.0f; t<1.0f;t+= Time.deltaTime / aTime)
        {
            light.intensity = Mathf.Lerp(intensity, aValue, t);
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
                BoxCollider2D collider = GetComponentInParent<BoxCollider2D>();
                collider.enabled = false;
                rb.velocity = Vector2.zero;
                StartCoroutine(FadeTo(0f, 0.5f));
                StartCoroutine(FadeLight(0f, 0.6f));
                StartCoroutine(death());
            }
        }
     }

     void OnTriggerExit2D(Collider2D other)
     {
        if(other.tag == "bluepikmin")
        {
                StartCoroutine(FadeTo(0f, 0.5f));
                StartCoroutine(FadeLight(0f, 0.6f));
                StartCoroutine(death());
        }
     }
}

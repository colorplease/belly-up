using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordfishai : MonoBehaviour
{
    [SerializeField]Transform amongUs;
    [SerializeField]Rigidbody2D rb;
    public float speed;
    public float force;
    public float HP;
    gamemanager gameManager;
    bool ready;
    bool launch;
    public GameObject laser;
    [SerializeField]Color preFire;
    [SerializeField]Color afterFire;
    bool hitting;
    public GameObject[] powerUps;
    bool dying;
     [SerializeField]float minChance;
    [SerializeField]float maxChance = 150;
    [SerializeField]float realChance;
    [Header("RePos Speed")]
    [SerializeField]bool rePos;
    [SerializeField]float rePosSpeed;


    

    void Start()
    {
        amongUs = GameObject.FindWithTag("Player").transform;
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<gamemanager>();

    }
    void FixedUpdate()
    {
        swordFish();
    }

    void swordFish()
    {
        if(!dying)
        {
            if(rePos)
            {
                Vector3 dir = amongUs.position - transform.position;
                float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * rePosSpeed);
            }
            if (ready)
            {
                Vector3 dir = amongUs.position - transform.position;
                float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, Mathf.SmoothStep(transform.rotation.z, angle, speed * Time.deltaTime ));
            }
            else
            {
                if (!launch)
                {
                    StartCoroutine(charge());
                }
            }
        }
        else
        {
            float angle = transform.rotation.z + 50;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * rePosSpeed);
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

    IEnumerator charge()
    {
        rb.velocity = Vector2.zero;
        launch = true;
        rePos = true;
        yield return new WaitForSeconds(1f);
        rePos = false;
        laser.SetActive(true);
        laser.GetComponent<SpriteRenderer>().color = preFire;
        ready = true;
        yield return new WaitForSeconds(2);
        ready = false;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        laser.GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.075f);
        laser.GetComponent<SpriteRenderer>().color = afterFire;
        yield return new WaitForSeconds(1);
        laser.SetActive(false);
        rb.AddForce(transform.right * force * Vector2.Distance(transform.position, amongUs.position), ForceMode2D.Impulse);
        yield return new WaitForSeconds(2);
        launch = false;
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

    void die()
    {
        PlayerPrefs.SetInt("murder", PlayerPrefs.GetInt("murder") + 1);
        dying = true;
        Generate();
        StopAllCoroutines();
        laser.SetActive(false);
        StartCoroutine(FadeTo(0f, 0.5f));
        StartCoroutine(death());
    }
    void Generate()
   {
    realChance = gameManager.maxPower - 50;
    var dropPowerChance = Random.Range(minChance, maxChance);
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

     void OnTriggerEnter2D(Collider2D other)
     {
        if (other.tag == "Player")
        {
            if (!hitting && !dying)
            {
                hitting = true;
                gameManager.hit();
                StartCoroutine(FadeTo(0f, 0.5f));
                StartCoroutine(death());
            }
        }
     }
}

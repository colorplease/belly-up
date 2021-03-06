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
    bool hitting;
    public GameObject[] powerUps;
    bool dying;
    

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
        if (ready)
        {
            Vector3 dir = amongUs.position - transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg ;
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
        laser.SetActive(true);
        ready = true;
        yield return new WaitForSeconds(2);
        ready = false;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(1);
        laser.SetActive(false);
        rb.AddForce(transform.right * force * Vector2.Distance(transform.position, amongUs.position), ForceMode2D.Impulse);
        yield return new WaitForSeconds(2);
        launch = false;
    }

    public void hit()
    {
        HP--;
        StartCoroutine(flash());
        rb.velocity = Vector2.zero;
        if (HP == 0)
        {
            die();
        }
    }

    void die()
    {
        dying = true;
        Generate();
        StopAllCoroutines();
        laser.SetActive(false);
        StartCoroutine(FadeTo(0f, 1f));
        StartCoroutine(death());
    }
    void Generate()
   {
    var chance = Random.Range(0, 4);
    if (chance < 3)
    {
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
                StartCoroutine(FadeTo(0f, 1f));
                StartCoroutine(death());
            }
        }
     }
}

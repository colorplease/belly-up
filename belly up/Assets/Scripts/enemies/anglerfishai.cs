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
        rb.AddRelativeForce(Vector2.right* speed, ForceMode2D.Force);
        Vector3 dir = amongUs.position - transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
                die();
                PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
                collider.enabled = false;
                dying = true;
            }
        }
    }

    void die()
    {
        dying = true;
        Generate();
        StartCoroutine(FadeTo(0f, 1f));
        StartCoroutine(death());
        StartCoroutine(FadeLight(0f, 1f));
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

     IEnumerator FadeLight(float aValue, float aTime)
     {
        float intensity = light.intensity;
        for (float t= 0.0f; t<1.0f;t+= Time.deltaTime / aTime)
        {
            light.intensity = Mathf.Lerp(intensity, aValue, t);
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

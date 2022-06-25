using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.Experimental.Rendering.Universal;

public class anglerfishai : MonoBehaviour
{
     [SerializeField]Transform amongUs;
    [SerializeField]Rigidbody2D rb;
    public float speed;
    public float HP;
    gamemanager gameManager;
    [SerializeField]Light2D light; 


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

    public void hit()
    {
        HP--;
        rb.velocity = Vector2.zero;
        if (HP == 0)
        {
            die();
        }
    }

    void die()
    {
        StartCoroutine(FadeTo(0f, 1f));
        StartCoroutine(death());
        StartCoroutine(FadeLight(0f, 1f));
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
            gameManager.hit();
            StartCoroutine(FadeTo(0f, 1f));
            StartCoroutine(death());
        }
     }
}

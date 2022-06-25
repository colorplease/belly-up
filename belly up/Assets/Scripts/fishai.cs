using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishai : MonoBehaviour
{
    [SerializeField]Transform amongUs;
    [SerializeField]Rigidbody2D rb;
    public float speed;
    public float HP;

    void Start()
    {
        amongUs = GameObject.FindWithTag("Player").transform;
    }
    void FixedUpdate()
    {
       Vector3 dir = amongUs.position - transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        rb.AddRelativeForce(Vector2.right* speed, ForceMode2D.Force);
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

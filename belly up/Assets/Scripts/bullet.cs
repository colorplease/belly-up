using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
   [SerializeField]fishai fish;
   swordfishai fish2;
   anglerfishai fish3;

    void OnEnable()
    {
        Destroy(gameObject, 2f);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "fish")
        {
            fish = collision.gameObject.GetComponent<fishai>();
            fish.hit();
        Destroy(gameObject);
        }
        if(collision.collider.tag == "swordfish")
        {
            fish2= collision.gameObject.GetComponent<swordfishai>();
            fish2.hit();
        Destroy(gameObject);
        }
        if(collision.collider.tag == "angler")
        {
            fish3= collision.gameObject.GetComponent<anglerfishai>();
            fish3.hit();
        Destroy(gameObject);
        }
    }
}

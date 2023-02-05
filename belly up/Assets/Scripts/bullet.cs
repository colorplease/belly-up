using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
   [SerializeField]fishai fish;
   swordfishai fish2;
   anglerfishai fish3;
   blobfishai fish4;
   plasticbag bag;

   tutorialBox tutorialbox;


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
        if(collision.collider.tag == "blob")
        {
            fish4 = collision.gameObject.GetComponent<blobfishai>();
            fish4.hit();
            Destroy(gameObject);
        }
        if (collision.collider.tag == "bag")
        {
            bag = collision.gameObject.GetComponent<plasticbag>();
            bag.hit();
            Destroy(gameObject);
        }
        if(collision.collider.tag == "tutorialBox")
        {
            tutorialbox = collision.gameObject.GetComponent<tutorialBox>();
            tutorialbox.Hit();
        Destroy(gameObject, 0.025f);
        }
    }
}

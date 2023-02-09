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
   [SerializeField]float minBulletFallOff;
   [SerializeField]float maxBulletFallOff;
   [SerializeField]float killDrag;
   [SerializeField]bool hasFallOff;


    void OnEnable()
    {
        if(hasFallOff)
        {
            StartCoroutine(bulletFalloff());
        }
        else
        {
            Destroy(gameObject, 5f);
        }
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

    IEnumerator bulletFalloff()
    {
        yield return new WaitForSeconds(Random.Range(minBulletFallOff, maxBulletFallOff));
        gameObject.GetComponent<Rigidbody2D>().drag = killDrag;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject, 1f);
    }
}

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
   dummyTarget dummytarget;

   tutorialBox tutorialbox;
   [SerializeField]float minBulletFallOff;
   [SerializeField]float maxBulletFallOff;
   [SerializeField]float killDrag;
   [SerializeField]bool hasFallOff;
   [SerializeField]float damage;


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
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "fish")
        {
            fish = other.gameObject.GetComponentInParent<fishai>();
            fish.hit(damage);
        Destroy(gameObject);
        }
        if(other.tag == "swordfish")
        {
            fish2= other.gameObject.GetComponentInParent<swordfishai>();
            fish2.hit(damage);
        Destroy(gameObject);
        }
        if(other.tag == "angler")
        {
            fish3= other.gameObject.GetComponentInParent<anglerfishai>();
            fish3.hit(damage);
        Destroy(gameObject);
        }
        if(other.tag == "blob")
        {
            fish4 = other.gameObject.GetComponentInParent<blobfishai>();
            fish4.hit(damage);
            Destroy(gameObject);
        }
        if (other.tag == "bag")
        {
            bag = other.gameObject.GetComponentInParent<plasticbag>();
            bag.hit(damage);
            Destroy(gameObject);
        }
        if(other.tag == "tutorialBox")
        {
            tutorialbox = other.gameObject.GetComponent<tutorialBox>();
            tutorialbox.Hit();
        Destroy(gameObject, 0.025f);
        }
        if(other.tag == "Dummy")
        {
            dummytarget = other.gameObject.GetComponent<dummyTarget>();
            dummytarget.Hit();
            Destroy(gameObject);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
   [SerializeField]fishai fish;

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
    }
}

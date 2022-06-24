using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
   [SerializeField]Explodable _explodable;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "fish")
        {
             _explodable = collision.collider.gameObject.GetComponent<Explodable>();
        _explodable.explode();
		ExplosionForce ef = GameObject.FindObjectOfType<ExplosionForce>();
        ef.doExplosion(transform.position);
        Destroy(gameObject);
		
        }
    }
}

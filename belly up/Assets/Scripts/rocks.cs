using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocks : MonoBehaviour
{
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Boundary")
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - 21.7f);
    }
  }
}

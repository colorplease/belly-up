using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocks : MonoBehaviour
{
  public float speed;
  [SerializeField]float currentPos;
  [SerializeField]bool left;
  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Boundary")
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - 21.7f * 2);
    }
  }

  void FixedUpdate()
  {
      transform.position = new Vector2(Mathf.Lerp(transform.position.x, currentPos, speed), transform.position.y);
  }
}

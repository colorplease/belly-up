using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aim : MonoBehaviour
{
    Vector2 mousePos;
    public Rigidbody2D rb;
    public float difference;

    public Transform notDummyArm;

    // Update is called once per frame
    void Start()
    {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg  - difference;
            rb.rotation = angle;
            notDummyArm.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Update is called once per frame
    void Update()
    {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg  - difference;
            rb.rotation = angle;
            notDummyArm.rotation = Quaternion.Euler(0, 0, angle);
    }
}

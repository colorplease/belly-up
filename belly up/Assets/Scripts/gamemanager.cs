using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamemanager : MonoBehaviour
{
    public float scrollSpeed;
    public Transform cameraTransform;

    // Update is called once per frame
    void FixedUpdate()
    {
        cameraTransform.Translate(-Vector2.up * Time.deltaTime * scrollSpeed);
    }
}

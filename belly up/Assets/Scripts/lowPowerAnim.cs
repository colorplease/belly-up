using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class lowPowerAnim : MonoBehaviour
{
    public Color red;
    public Color yellow;
    public float time = 0.69f;
    TextMeshProUGUI image;

    void Start()
    {
        image = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Flash());
    }

    void OnEnable()
    {
        StartCoroutine(Flash());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }
    
    IEnumerator Flash()
    {
        image.color = red;
        yield return new WaitForSeconds(time * 0.5f);
        image.color = yellow;
        yield return new WaitForSeconds(time * 0.5f);
        StartCoroutine(Flash());
    }
}

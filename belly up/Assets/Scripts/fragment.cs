using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fragment : MonoBehaviour
{
  void OnEnable()
   {
    StartCoroutine(FadeTo(0f, 1f));
    StartCoroutine(death());
   }

   IEnumerator FadeTo(float aValue, float aTime)
     {
         float alpha = transform.GetComponent<MeshRenderer>().material.color.a;
         Debug.Log(alpha);
         for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
         {
             Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
             transform.GetComponent<MeshRenderer>().material.color = newColor;
             yield return null;
         }
     }

     IEnumerator death()
     {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class depthMeter : MonoBehaviour
{

    public GameObject[] deeps;
    [SerializeField]RectTransform pointerTransform;
    int index;
    public void UpdateDepth(int type)
    {
        index = type;
        StartCoroutine(depthAnim());
        switch(type)
        {
            case 0:
            deeps[index].SetActive(true);
            pointerTransform.position = new Vector2(pointerTransform.position.x, deeps[index].transform.position.y);
            break;

            case 1:
            deeps[index - 1].SetActive(false);
            deeps[index].SetActive(true);
            pointerTransform.position = new Vector2(pointerTransform.position.x, deeps[index].transform.position.y);
            break;

            case 2:
            deeps[index - 1].SetActive(false);
            deeps[index].SetActive(true);
            pointerTransform.position = new Vector2(pointerTransform.position.x, deeps[index].transform.position.y);
            break;

            case 3:
            deeps[index - 1].SetActive(false);
            deeps[index].SetActive(true);
            pointerTransform.position = new Vector2(pointerTransform.position.x, deeps[index].transform.position.y);
            break;

            case 4:
            deeps[index - 1].SetActive(false);
            deeps[index].SetActive(true);
            pointerTransform.position = new Vector2(pointerTransform.position.x, deeps[index].transform.position.y);
            break;

            case 5:
            deeps[index - 1].SetActive(false);
            deeps[index].SetActive(true);
            pointerTransform.position = new Vector2(pointerTransform.position.x, deeps[index].transform.position.y);
            break;
        }
    }

    IEnumerator depthAnim()
    {
        StartCoroutine(FadeTo(1f, 0.5f));
        yield return new WaitForSeconds(5f);
        StartCoroutine(FadeTo(-0.5f, 0.5f));
    }

    IEnumerator FadeTo(float aValue, float aTime)
     {
         float alpha = transform.GetComponent<CanvasGroup>().alpha;
         for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
         {
            transform.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(alpha, aValue, t);
            yield return null;
         }
     }
}

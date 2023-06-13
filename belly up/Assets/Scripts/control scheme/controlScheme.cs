using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controlScheme : MonoBehaviour
{
    [SerializeField]GameObject pickControlText;
    [SerializeField]GameObject trackButton;
    [SerializeField]GameObject mouseButton;
    public void TrackPadInput()
    {
        PlayerPrefs.SetInt("InputMode", 1);
        StartCoroutine(fadeOutEnd());
    }

    public void MouseInput()
    {
        PlayerPrefs.SetInt("InputMode", 0);
        StartCoroutine(fadeOutEnd());
    }

    void Start()
    {
        StartCoroutine(fadeInBegin());
    }

    IEnumerator fadeInBegin()
    {
        yield return new WaitForSeconds(0.1f);
        pickControlText.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        mouseButton.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        trackButton.SetActive(true);  
    }

    IEnumerator fadeOutEnd()
    {
        yield return new WaitForSeconds(0.1f);
        pickControlText.GetComponent<Animator>().SetBool("byebye", true);
        yield return new WaitForSeconds(0.1f);
        mouseButton.GetComponent<Animator>().SetBool("byebye", true);
        yield return new WaitForSeconds(0.1f);
        trackButton.GetComponent<Animator>().SetBool("byebye", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("SampleScene");
    }
    
}

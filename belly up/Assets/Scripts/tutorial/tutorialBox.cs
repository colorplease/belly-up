using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialBox : MonoBehaviour
{
    public int boxType;
    public GameObject tutorialText;
    public GameObject fishBoxes;
    public GameObject lightFish;
    public GameObject angler;
    public GameObject sword;
    public GameObject blob;
    public SpriteRenderer spriteRenderer;
    public Transform spawn;

    public void Hit()
    {
        switch(boxType)
        {
            case 0:
            StartCoroutine(flash());
            if(tutorialText.activeSelf)
            {
                tutorialText.SetActive(false);
                fishBoxes.SetActive(true);
            }
            else
            {
                tutorialText.SetActive(true);
                fishBoxes.SetActive(false);
            }
            break;

            case 1:
            StartCoroutine(flash());
            GameObject spawnedFish = Instantiate(lightFish, spawn.position, Quaternion.identity);
            spawnedFish.GetComponent<SpriteRenderer>().flipY = true;
            break;

            case 2:
            StartCoroutine(flash());
            GameObject spawnedFish2 = Instantiate(sword, spawn.position, Quaternion.identity);
            spawnedFish2.GetComponent<SpriteRenderer>().flipY = true;
            break;

            case 3:
            StartCoroutine(flash());
            GameObject spawnedFish3 = Instantiate(angler, spawn.position, Quaternion.identity);
            break;

            case 4:
            StartCoroutine(flash());
            GameObject spawnedFish4 = Instantiate(blob, spawn.position, Quaternion.identity);
            break;
        }

        
    }
     IEnumerator flash()
    {
        SpriteRenderer colorMe = gameObject.GetComponent<SpriteRenderer>();
        colorMe.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        colorMe.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        colorMe.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        colorMe.color = Color.white;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ohthefishguide : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI title;
    [SerializeField]TextMeshProUGUI type;
    [SerializeField]TextMeshProUGUI occupation;
    [SerializeField]TextMeshProUGUI education;
    [SerializeField]TextMeshProUGUI hobbies;
    [SerializeField]TextMeshProUGUI tagline;
    [SerializeField]GameObject[] data;
    [SerializeField]GameObject[] fish3D;
    [SerializeField]Animator fishGuide;
   [SerializeField]GameObject fishGuideObject;
   [SerializeField]gameStartManager startManager;
    public void fishguidefadeout()
    {
        startManager.audioSource.PlayOneShot(startManager.buttonPress);
        fishGuide.SetBool("fishGuide", true);
        StartCoroutine(fadeouttimer());
    }
    IEnumerator fadeouttimer()
    {
        yield return new WaitForSeconds(1);
        fishGuideObject.SetActive(false);
    }
    public void cod()
    {
        startManager.audioSource.PlayOneShot(startManager.fishGuidePress);
        title.SetText("Cod");
        type.SetText("Type: fish");
        occupation.SetText("Occupation: fish");
        education.SetText("Education: Business Major at Standford");
        hobbies.SetText("Hobbies: fish");
        tagline.SetText("i have no adversaries");
        for(int i = 0; i < data.Length; i++)
        {
            data[i].SetActive(false);
        }
        data[0].SetActive(true);
        for(int i = 0; i < fish3D.Length; i++)
        {
            fish3D[i].SetActive(false);
        }
        fish3D[0].SetActive(true);
    }

    public void sword()
    {
        startManager.audioSource.PlayOneShot(startManager.fishGuidePress);
        title.SetText("Sword Fish");
        type.SetText("Type: fish");
        occupation.SetText("Occupation: Data Science Analyst");
        education.SetText("Education: fish");
        hobbies.SetText("Hobbies: fish");
        tagline.SetText("i want a girlfriend");
        for(int i = 0; i < data.Length; i++)
        {
            data[i].SetActive(false);
        }
        data[1].SetActive(true);
        for(int i = 0; i < fish3D.Length; i++)
        {
            fish3D[i].SetActive(false);
        }
        fish3D[1].SetActive(true);
    }

    public void angler()
    {
        startManager.audioSource.PlayOneShot(startManager.fishGuidePress);
        title.SetText("Angler Fish");
        type.SetText("Type: fish");
        occupation.SetText("Occupation: fish");
        education.SetText("Education: fish");
        hobbies.SetText("Hobbies: reading and dabbles in writing");
        tagline.SetText("perpendicularity of the bisector");
        for(int i = 0; i < data.Length; i++)
        {
            data[i].SetActive(false);
        }
        data[2].SetActive(true);
        for(int i = 0; i < fish3D.Length; i++)
        {
            fish3D[i].SetActive(false);
        }
        fish3D[2].SetActive(true);
    }

    public void blobfish()
    {
        startManager.audioSource.PlayOneShot(startManager.fishGuidePress);
        title.SetText("Blobfish");
        type.SetText("Type: fish");
        occupation.SetText("Occupation: Finance Consultant");
        education.SetText("Education: fish");
        hobbies.SetText("Hobbies: fish");
        tagline.SetText("hello francis");
        for(int i = 0; i < data.Length; i++)
        {
            data[i].SetActive(false);
        }
        data[3].SetActive(true);
        for(int i = 0; i < fish3D.Length; i++)
        {
            fish3D[i].SetActive(false);
        }
        fish3D[3].SetActive(true);
    }

    public void plasticBag()
    {
        startManager.audioSource.PlayOneShot(startManager.fishGuidePress);
        title.SetText("a literal plastic bag");
        type.SetText("Type: Creature");
        occupation.SetText("Occupation: plastic bag");
        education.SetText("Education: plastic bag");
        hobbies.SetText("Hobbies: plastic bag");
        tagline.SetText("colorplease cant 3d model a real bag");
        for(int i = 0; i < data.Length; i++)
        {
            data[i].SetActive(false);
        }
        data[4].SetActive(true);
        for(int i = 0; i < fish3D.Length; i++)
        {
            fish3D[i].SetActive(false);
        }
        fish3D[4].SetActive(true);
    }

    public void seaBirdMKII()
    {
        startManager.audioSource.PlayOneShot(startManager.fishGuidePress);
        title.SetText("FishCorp “SeaBird” MK II");
        type.SetText("Type: Machine");
        occupation.SetText("Occupation: Consumer Submarine");
        education.SetText("Education: ??");
        hobbies.SetText("Hobbies: ??");
        tagline.SetText("i haven't eaten lunch yet");
        for(int i = 0; i < data.Length; i++)
        {
            data[i].SetActive(false);
        }
        data[5].SetActive(true);
        for(int i = 0; i < fish3D.Length; i++)
        {
            fish3D[i].SetActive(false);
        }
        fish3D[5].SetActive(true);
    }

    public void FishCorp()
    {
        startManager.audioSource.PlayOneShot(startManager.fishGuidePress);
        title.SetText("FishCorp");
        type.SetText("Type: Tech-Conglomerate");
        occupation.SetText("Occupation: Universal Domination");
        education.SetText("Education: High-School Diploma");
        hobbies.SetText("Hobbies: DDR");
        tagline.SetText("go play FinuxOS on colorplease . itch . io");
        for(int i = 0; i < data.Length; i++)
        {
            data[i].SetActive(false);
        }
        data[6].SetActive(true);
        for(int i = 0; i < fish3D.Length; i++)
        {
            fish3D[i].SetActive(false);
        }
        fish3D[6].SetActive(true);

    }
}

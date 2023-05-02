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
    public void fishguidefadeout()
    {
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
}

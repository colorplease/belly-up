using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuButton : MonoBehaviour
{
    public gameStartManager gamestart;
    [SerializeField]bool soundPlayed;

    void Update()
    {
        if(IsPointerOverUIElement(GetEventSystemRaycastResults()) == true)
        {
            if(!soundPlayed)
            {   
                gamestart.audioSource.PlayOneShot(gamestart.buttonSelect);
                soundPlayed = true;
            }
            
        }
        else
        {
            soundPlayed = false;
        }
    }

    //Returns 'true' if we touched or hovering on Unity UI element.
    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.tag == "buttonClickNoise" && curRaysastResult.gameObject == this.gameObject)
            {
                return true;
            }
        }
        return false;
    }

     //Gets all event system raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
}

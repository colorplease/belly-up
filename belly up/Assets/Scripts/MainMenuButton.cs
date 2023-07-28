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
        if(EventSystem.current.IsPointerOverGameObject())
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
}

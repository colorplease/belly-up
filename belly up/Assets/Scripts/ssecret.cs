using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ssecret : MonoBehaviour
{
    [SerializeField]gamemanager gameManager;
    public TMP_InputField input;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            switch(input.text)
            {
                case "dylan":
                gameManager.DylanMode();
                break;
            }
        }
    }
}

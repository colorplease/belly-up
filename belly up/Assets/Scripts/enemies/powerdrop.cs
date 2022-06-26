using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerdrop : MonoBehaviour
{
    public GameObject[] powerUps;
   public void Generate()
   {
    var chance = Random.Range(0, 3);
    Debug.Log(chance);
    if (chance < 2)
    {
        Instantiate(powerUps[chance], transform.position, Quaternion.identity);
    }
   }
}

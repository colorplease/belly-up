using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class egg : MonoBehaviour
{
    public GameObject[] enemies;
   public void Spawn(int limit)
    {
        GameObject fish = Instantiate(enemies[Random.Range(0, limit)], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

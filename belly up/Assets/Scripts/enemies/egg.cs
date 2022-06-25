using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class egg : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform mommy;
   public void Spawn(int limit)
    {
        mommy = GameObject.FindWithTag("mommy").GetComponent<Transform>();
        GameObject fish = Instantiate(enemies[Random.Range(0, limit)], transform.position, Quaternion.identity);
        fish.transform.parent = mommy;
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class egg : MonoBehaviour
{
    public GameObject[] enemies;
    public Transform mommy;
    [SerializeField]int[] validChoices;
    public void Spawn(int limit)
    {
        mommy = GameObject.FindWithTag("mommy").GetComponent<Transform>();
        GameObject fish = Instantiate(enemies[Random.Range(0, limit - 1)], transform.position, Quaternion.identity);
        fish.transform.parent = mommy;
        Destroy(gameObject);
    }
    public void AnglerSpawn(int limit)
    {
        mommy = GameObject.FindWithTag("mommy").GetComponent<Transform>();
        //[0,1,3,4];
        GameObject fish = Instantiate(enemies[validChoices[Random.Range(0, limit - 1)]], transform.position, Quaternion.identity);
        fish.transform.parent = mommy;
        Destroy(gameObject);
    }
}

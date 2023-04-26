using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blobfishai : MonoBehaviour
{
    [SerializeField]Transform amongUs;
    [SerializeField]Rigidbody2D rb;
    public float speed;
    public float HP;
    gamemanager gameManager;
    public GameObject underlings;
    public int dupeNumber;
    bool dying;
    [SerializeField]float maxSpeed;
    bool hitting;
    public GameObject[] powerUps;
    public GameObject splitParticle;
    [SerializeField]float time;
    [SerializeField]float minChance;
    [SerializeField]float maxChance = 150;
    [SerializeField]float realChance;
    [SerializeField]float deathSpinSpeed;
    [SerializeField]int difficultyNum;
    [SerializeField]int[] difficultyTierPotentials;
    [SerializeField]int tier;
    [SerializeField]int[] weightTable;
    [SerializeField]int calcVar;
    [SerializeField]int iterationGenNum;

    void Start()
    {
        amongUs = GameObject.FindWithTag("Player").transform;
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<gamemanager>();
        if(dupeNumber == 0)
        {
            DifficultySet();
            TierCalculation();
        }
    }
    void FixedUpdate()
    {
        blobFish();
    }

    void TierCalculation()
    {
        if(calcVar <= weightTable[iterationGenNum])
        {
            tier = difficultyTierPotentials[iterationGenNum];
            float buff = 1+(tier * 0.075f);
            transform.localScale = new Vector3(transform.localScale.x * buff, transform.localScale.y * buff, transform.localScale.z);
            HP += tier * 0.4f;
            speed += (tier * 0.075f);
        }
        else
        {
            iterationGenNum++;
            TierCalculation();
        }
    }

    void DifficultySet()
    {
        difficultyNum = gameManager.difficultNumber;
        calcVar = Random.Range(0, 100);
        switch(difficultyNum)
        {
            case 1:
            for(int i = 0; i < difficultyTierPotentials.Length; i++)
            {
                difficultyTierPotentials[i] = 1;
            }
            break;
            case 2:
            for(int i = 0; i < difficultyTierPotentials.Length; i++)
            {
                difficultyTierPotentials[i] = 1;
            }
            difficultyTierPotentials[0] = 2;
            break;
            case 3:
            for(int i = 0; i < difficultyTierPotentials.Length; i++)
            {
                difficultyTierPotentials[i] = 1;
            }
            difficultyTierPotentials[0] = 3;
            difficultyTierPotentials[1] = 2;
            break;
            case 4:
            for(int i = 0; i < difficultyTierPotentials.Length; i++)
            {
                difficultyTierPotentials[i] = 1;
            }
            difficultyTierPotentials[0] = 4;
            difficultyTierPotentials[1] = 3;
            difficultyTierPotentials[2] = 2;
            break;
        }
        if(difficultyNum >= 5)
        {
            for(int i = 0; i < difficultyTierPotentials.Length; i++)
            {
                difficultyTierPotentials[i] += (difficultyNum - 4);
            }
        }
    }

    void blobFish()
    {
        if(!dying)
        {
            if(rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
            }
            rb.AddRelativeForce(Vector2.right* speed, ForceMode2D.Force);
            Vector3 dir = amongUs.position - transform.position;
            float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            float angle = transform.rotation.z + 50;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * deathSpinSpeed);
        }
    }

    public void hit(float dmg)
    {
        if(!dying)
        {
            HP -= dmg;
            StartCoroutine(flash());
            rb.velocity = Vector2.zero;
            if (HP <= 0)
            {
                die();
                PolygonCollider2D collider = GetComponentInChildren<PolygonCollider2D>();
                collider.enabled = false;
                dying = true;
            }
        }
    }
    void Generate()
   {
    realChance = gameManager.maxPower - 50;
    var dropPowerChance = Random.Range(minChance, maxChance);
    if (dropPowerChance >= realChance)
    {
        var chance = Random.Range(0, 4);
        Instantiate(powerUps[chance], transform.position, Quaternion.identity);
    }
   }
    

    void die()
    {
        PlayerPrefs.SetInt("murder", PlayerPrefs.GetInt("murder") + 1);
        if (!dying)
        {
            Generate();
            PolygonCollider2D collider = GetComponentInChildren<PolygonCollider2D>();
            collider.enabled = false;
            if (dupeNumber < 1)
        {
            for(int i = 0; i<Random.Range(2,4);i++)
         {
            StartCoroutine(summon());
         }
        }
        else
        {
            StartCoroutine(FadeTo(0f, 0.5f));
            StartCoroutine(death());
        }

        }
    }
   

    IEnumerator summon()
    {
        StartCoroutine(split());
        GameObject splitEffect = Instantiate(splitParticle, transform.position,Quaternion.identity);
        Destroy(splitEffect, 5f);
        StartCoroutine(FadeTo(0f, 1.25f));
        yield return new WaitForSeconds(0.25f);
        GameObject fish = Instantiate(underlings, new Vector2(transform.position.x + Random.Range(-0.3f, 0.3f), transform.position.y + Random.Range(-0.1f, 0.1f)), Quaternion.identity);
        fish.GetComponent<SpriteRenderer>().enabled = true;
        float buff = Random.Range(1,(1+(difficultyNum * 0.35f)));
        fish.transform.localScale = new Vector2(transform.localScale.x * 0.75f, transform.localScale.y  * 0.75f);
        Color transparencyFix = new Color(1f, 1f, 1f, 1f);
        fish.GetComponent<SpriteRenderer>().enabled = true;
        fish.GetComponentInChildren<PolygonCollider2D>().enabled = true;
        fish.GetComponent<SpriteRenderer>().color = transparencyFix;
        fish.GetComponent<blobfishai>().HP = (2 + difficultyNum);
        fish.GetComponent<blobfishai>().speed = speed * Random.Range(2,4);
        fish.GetComponent<blobfishai>().dupeNumber += 1;
        Destroy(gameObject, 1f);
    }

    IEnumerator split()
    {
        yield return new WaitForSeconds(time);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
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

    IEnumerator death()
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    IEnumerator FadeTo(float aValue, float aTime)
     {
         float alpha = transform.GetComponent<SpriteRenderer>().color.a;
         for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
         {
             Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
             transform.GetComponent<SpriteRenderer>().color = newColor;
             yield return null;
         }
     }

     void OnCollisionEnter2D(Collision2D other)
     {
        if (other.collider.tag == "Player")
        {
            if (!hitting &&  !dying)
            {
                hitting  = true;
                if(tier != 1)
                {
                    float dmgMultiplier = (float)tier;
                    gameManager.hit((int)Mathf.Round(dmgMultiplier / 2f));
                }
                else
                {
                    gameManager.hit(1);
                }
                StartCoroutine(FadeTo(0f, 0.5f));
                StartCoroutine(death());
            }
        }
     }
}

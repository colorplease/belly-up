using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordfishai : MonoBehaviour
{
    [SerializeField]Transform amongUs;
    [SerializeField]Rigidbody2D rb;
    public float speed;
    public float force;
    public float HP;
    gamemanager gameManager;
    bool ready;
    bool launch;
    public GameObject laser;
    [SerializeField]Color preFire;
    [SerializeField]Color afterFire;
    bool hitting;
    public GameObject[] powerUps;
    bool dying;
     [SerializeField]float minChance;
    [SerializeField]float maxChance = 150;
    [SerializeField]float realChance;
    [SerializeField]int difficultyNum;
    [SerializeField]int[] difficultyTierPotentials;
    [SerializeField]int tier;
    [SerializeField]int[] weightTable;
    [SerializeField]int calcVar;
    [SerializeField]int iterationGenNum;
    [SerializeField]float fireTime = 2;
    [Header("RePos Speed")]
    [SerializeField]bool rePos;
    [SerializeField]float rePosSpeed;
    


    

    void Start()
    {
        amongUs = GameObject.FindWithTag("Player").transform;
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<gamemanager>();
        DifficultySet();
        TierCalculation();
    }
    void TierCalculation()
    {
        if(calcVar <= weightTable[iterationGenNum])
        {
            tier = difficultyTierPotentials[iterationGenNum];
            float buff = 1+(tier * 0.075f);
            transform.localScale = new Vector3(transform.localScale.x * buff, transform.localScale.y * buff, transform.localScale.z);
            HP += tier * 0.4f;
            if(fireTime - (tier * 0.075f) > 0.5f)
            {
                fireTime -= (tier * 0.075f);
                force += (tier * 0.075f);
            } 
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
    void FixedUpdate()
    {
        swordFish();
    }

    void swordFish()
    {
        if(!dying)
        {
            if(rePos)
            {
                Vector3 dir = amongUs.position - transform.position;
                float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * rePosSpeed);
            }
            if (ready)
            {
                Vector3 dir = amongUs.position - transform.position;
                float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, Mathf.SmoothStep(transform.rotation.z, angle, speed * Time.deltaTime ));
            }
            else
            {
                if (!launch)
                {
                    StartCoroutine(charge());
                }
            }
        }
        else
        {
            float angle = transform.rotation.z + 50;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * rePosSpeed);
        }
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

    IEnumerator charge()
    {
        rb.velocity = Vector2.zero;
        launch = true;
        rePos = true;
        yield return new WaitForSeconds(1f);
        rePos = false;
        laser.SetActive(true);
        laser.GetComponent<SpriteRenderer>().color = preFire;
        ready = true;
        yield return new WaitForSeconds(fireTime);
        ready = false;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        laser.GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.075f);
        laser.GetComponent<SpriteRenderer>().color = afterFire;
        yield return new WaitForSeconds(1);
        laser.SetActive(false);
        rb.AddForce(transform.right * force * Vector2.Distance(transform.position, amongUs.position), ForceMode2D.Impulse);
        yield return new WaitForSeconds(2);
        launch = false;
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

    void die()
    {
        PlayerPrefs.SetInt("murder", PlayerPrefs.GetInt("murder") + 1);
        dying = true;
        Generate();
        StopAllCoroutines();
        laser.SetActive(false);
        StartCoroutine(FadeTo(0f, 0.5f));
        StartCoroutine(death());
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

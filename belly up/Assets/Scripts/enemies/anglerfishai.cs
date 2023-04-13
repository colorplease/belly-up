using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class anglerfishai : MonoBehaviour
{
     [SerializeField]Transform amongUs;
    [SerializeField]Rigidbody2D rb;
    public float speed;
    public float HP;
    gamemanager gameManager;
    [SerializeField]UnityEngine.Rendering.Universal.Light2D light; 
    bool hitting;
    public GameObject[] powerUps;
    bool dying;
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
    void FixedUpdate()
    {
        anglerFish();
    }

    void anglerFish()
    {
        if(!dying)
        {
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

    public void hit(float dmg)
    {
        if(!dying)
        {
            HP -= dmg;
            StartCoroutine(flash());
            rb.velocity = Vector2.zero;
            if (HP <= 0)
            {
                PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
                collider.enabled = false;
                die();
                dying = true;
            }
        }
    }

    void die()
    {
        PlayerPrefs.SetInt("murder", PlayerPrefs.GetInt("murder") + 1);
        dying = true;
        Generate();
        StartCoroutine(FadeTo(0f, 0.5f));
        StartCoroutine(death());
        StartCoroutine(FadeLight(0f, 0.5f));
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

     IEnumerator FadeLight(float aValue, float aTime)
     {
        float intensity = light.intensity;
        for (float t= 0.0f; t<1.0f;t+= Time.deltaTime / aTime)
        {
            light.intensity = Mathf.Lerp(intensity, aValue, t);
            yield return null;
        }
     }

     void OnTriggerEnter2D(Collider2D other)
     {
        if (other.tag == "Player")
        {
            if (!hitting && !dying)
            {
                hitting = true;
                if(tier != 1)
                {
                    float dmgMultiplier = (float)tier;
                    gameManager.hit((int)Mathf.Round(dmgMultiplier / 2f));
                }
                else
                {
                    gameManager.hit(1);
                }
                PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
                collider.enabled = false;
                rb.velocity = Vector2.zero;
                StartCoroutine(FadeTo(0f, 0.5f));
                StartCoroutine(FadeLight(0f, 0.6f));
                StartCoroutine(death());
            }
        }
     }
}

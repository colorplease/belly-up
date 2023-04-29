using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class plasticbag : MonoBehaviour
{
    public float HP = 250;
    public Slider healthBar;
    float currentVelocity;
    public float smooth;
    public Rigidbody2D rb;
    public bool death;
    public gamemanager gameManager;

    void Start()
    {
        healthBar.value = HP;
        healthBar.maxValue = HP;
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<gamemanager>();
    }

    void OnEnable()
    {
        if(gameManager.dylanMode)
        {
            HP = 1000;
        }
    }
   
   public void hit(float dmg)
    {
        gameManager.PlasticBagHit();
        HP -= dmg;
        StartCoroutine(flash());
        if (HP <= 0)
        {
            die();
        }
    }

    void Update()
    {
        float currentHealth = Mathf.SmoothDamp(healthBar.value, HP,  ref currentVelocity, smooth * Time.deltaTime);
        healthBar.value = currentHealth;
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

    void die()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        death = true;
    }

}

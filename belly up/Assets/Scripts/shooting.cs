using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class shooting : MonoBehaviour
{
    public Transform firePoint;
   public GameObject bulletPrefab;

   public float bulletForce = 20f;
   public float playerKnockbackForce;
    public float fireRate;
    public float switchRate;
    public float dashKB;
    float nextTimeToFire;
    float nextTimeToSwitch;
    public float recoveryBounce;
    public float recoveryBounceMultiplier;
    int gunType;
    public bool readyToDash;
    public GameObject arrow;

   [SerializeField]Rigidbody2D player;

   Vector2 mousePos;
    public Rigidbody2D rb;
    public float difference;
    public float shotgunDifference;
    Vector2 lookDir;
    public bool usedPower;
    public int powerType;
    public bool outOfPower;
    public Transform play;
    public Transform cameraTransform;
    public Vector3 originalPos;
    public float shakeDuration;
    public float decreaseFactor;
    public float shakeAmount;
    public bool shaking;
    public bool shakeEnabled;
    public bool control;
    public Animator weaponText;
    public TextMeshProUGUI weaponTextText;

    void Start()
    {
      
    }

   void Update()
   {
    if (control)
    {
      play.position = transform.position;
      if (Input.GetKeyDown(KeyCode.Tab) && Time.time >= switchRate)
        {
          switch(gunType)
          {
            case 0:
            gunType = 1;
            StartCoroutine(pulse());
            weaponTextText.SetText("Shotgun");
            break;
            case 1:
            gunType = 0;
            StartCoroutine(pulse());
            weaponTextText.SetText("Single-Shot");
            break;
          }
          nextTimeToSwitch = Time.time + switchRate;
        }
        if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            Shoot();
            nextTimeToFire = Time.time + 1f/fireRate;
        }
        if(Input.GetMouseButtonDown(1))
        {
          powerType = 1;
          usedPower = true;
          if (!outOfPower)
          {
            readyToDash = true;
          arrow.SetActive(true);
          }
        }
        if (Input.GetMouseButtonUp(1))
        {
          arrow.SetActive(false);
          if (readyToDash == true)
          {
            player.AddForce(lookDir * dashKB, ForceMode2D.Impulse);
            readyToDash = false;
          } 
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
          Brake();
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
          player.drag = 0.1f;
        }
        if (shaking && shakeEnabled)
    {
      if(shakeDuration > 0)
    {
      cameraTransform.localPosition = originalPos + UnityEngine.Random.insideUnitSphere * shakeAmount;
    shakeDuration -= Time.deltaTime * decreaseFactor;
    }
    else
    {
      shakeDuration = 0;
      cameraTransform.localPosition = originalPos;
      shaking = false;
    }
    }
        
   }
   else
   {
    player.velocity = Vector2.zero;
    rb.velocity = Vector2.zero;
   }
   }

   void FixedUpdate()
   {
    if (control)
    {
      Aim();
    }
   }

   void Shoot()
   {
    if (gunType == 0)
    {
      powerType = 2;
      usedPower = true;
      if (!outOfPower)
      {
        playerKnockbackForce = 0.075f;
      fireRate = 5;
      shakeAmount = 0.025f;
      shakeDuration = 0.25f;
      Shake();
      GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right* bulletForce, ForceMode2D.Impulse);
        player.AddForce(-lookDir * playerKnockbackForce, ForceMode2D.Impulse);
      }
    }
    else
    {
      for(int i = 0; i<Random.Range(10,15);i++)
      {
        powerType = 3;
     usedPower = true;
     if (!outOfPower)
     {
      playerKnockbackForce = 0.05f;
           fireRate = 1.5f;
           bulletForce = 15;
           shakeAmount = 0.075f;
      shakeDuration = 0.25f;
      Shake();
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        float variation = Random.Range(-20,20);
        var x = firePoint.transform.position.x - player.transform.position.x;
        var y = firePoint.transform.position.y - player.transform.position.y;
        float rotateAngle = variation + (Mathf.Atan2(y,x) * Mathf.Rad2Deg - shotgunDifference);
        var MovementDirection = new Vector2(Mathf.Cos(rotateAngle * Mathf.Deg2Rad), Mathf.Sin(rotateAngle*Mathf.Deg2Rad)).normalized;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(MovementDirection* bulletForce, ForceMode2D.Impulse);
        player.AddForce(-lookDir * playerKnockbackForce, ForceMode2D.Impulse);
     }
      }
     
    }
    }

  IEnumerator pulse()
  {
    weaponText.SetBool("goTime", true);
    yield return new WaitForSeconds(0.1f);
     weaponText.SetBool("goTime", false);
  }
  

   void Aim()
   {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
             lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg  - difference;
           transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
   }

   void Brake()
   {
    player.AddForce(-player.transform.up * recoveryBounce * recoveryBounceMultiplier, ForceMode2D.Impulse);
     player.drag = 10;
     powerType = 0;
     usedPower = true;
   }

   void Shake()
   {
    originalPos = cameraTransform.position;
      shaking = true;
   }

   
   
}

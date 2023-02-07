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

   public Rigidbody2D player;

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
    public bool hit;
    public bool out2;
    public Transform outtaHere;
    public ParticleSystem bubbles;
    public AudioSource speaker;
    public AudioClip[] sounds;
    Renderer rd;
    [SerializeField]GameObject indicator;
    [SerializeField]Animator indicatorAnimator;
    [SerializeField]GameObject indicatorText;
    [SerializeField]Animator indicatorTextAnimator;
    [SerializeField]float maxDistanceTillLoss;
    [SerializeField]bool canShoot = true;
    Animator animator;
    [SerializeField]Color energyUp;
    [SerializeField]Color maxEnergyUp;

    void Start()
    {
      rd = play.gameObject.GetComponent<Renderer>();
      animator = play.gameObject.GetComponent<Animator>();
    }

    IEnumerator hitAnim()
    {
      play.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
      yield return new WaitForSeconds(0.1f);
      play.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
      yield return new WaitForSeconds(0.1f);
      play.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
      yield return new WaitForSeconds(0.1f);
      play.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    IEnumerator quickFadeOut()
    {
      indicatorAnimator.SetBool("in", false);
      indicatorTextAnimator.SetBool("in", false);
      yield return new WaitForSeconds(0.01f);
      indicator.SetActive(false);

    }

   void Update()
   {
    if(!rd.isVisible)
    {
        if(!indicator.activeSelf)
        {
          indicatorText.SetActive(true);
          indicator.SetActive(true);
          indicatorAnimator.SetBool("in", true);
          indicatorTextAnimator.SetBool("in", true);
        }
      Vector3 dir = indicator.transform.position - transform.position;
      float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
      indicator.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }
    else
    {
      if(indicator.activeSelf)
      {
        indicatorText.SetActive(false);
        StartCoroutine(quickFadeOut());
      }
    }
    if (hit)
    {
      StartCoroutine(hitAnim());
      shakeAmount = 0.08f;
      shakeDuration = 0.25f;
      Shake();
      hit = false;
    }
    if (Vector2.Distance(transform.position, outtaHere.position) > maxDistanceTillLoss)
    {
      out2 = true;
    }

    if (control)
    {
      if (Input.GetKeyDown(KeyCode.Tab) && Time.time >= switchRate)
        {

          switch(gunType)
          {
            case 0:
            gunType = 1;
            StartCoroutine(pulse());
            weaponTextText.SetText("Shotgun");
            speaker.PlayOneShot(sounds[0]);
            break;
            case 1:
            gunType = 0;
            StartCoroutine(pulse());
            weaponTextText.SetText("Single-Shot");
            speaker.PlayOneShot(sounds[1]);
            break;
          }
          nextTimeToSwitch = Time.time + switchRate;
        }
        if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && canShoot)
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
          StopCoroutine(canShootTimer());
          canShoot = true;
          animator.SetBool("isBraking", false);
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
      play.position = transform.position;
      Aim();
    }
   }

   void Shoot()
   {
     speaker.pitch = Random.Range(0.8f, 1.2f);
    speaker.PlayOneShot(sounds[2]);
    speaker.PlayOneShot(sounds[3]);
    if (gunType == 0)
    {
      powerType = 2;
      usedPower = true;
      if (!outOfPower)
      {
        bubbles.Play();
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
      bubbles.Play();
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
    StartCoroutine(canShootTimer());
    player.AddForce(-player.transform.up * recoveryBounce * recoveryBounceMultiplier, ForceMode2D.Impulse);
     player.drag = 8;
     powerType = 0;
     usedPower = true;
   }

   void Shake()
   {
    originalPos = cameraTransform.position;
      shaking = true;
   }

   public void PowerUpCollect(int powerType)
   {
    Color currentColor = new Color(1, 1, 1, 1);
    switch(powerType)
    {
      case 0:
      currentColor = energyUp;
      break;
      case 1:
      currentColor = maxEnergyUp;
      break;
    }
    StartCoroutine(powerCollected(currentColor));
   }

   IEnumerator canShootTimer()
   {
    animator.SetBool("isBraking", true);
    yield return new WaitForSeconds(0.5f);
    if(animator.GetBool("isBraking") == true)
    {
      canShoot = false;
    }
   }

   IEnumerator powerCollected(Color currentColor)
   {
    play.GetComponent<SpriteRenderer>().color = currentColor;
    animator.SetBool("collected", true);
    yield return new WaitForSeconds(0.2f);
    animator.SetBool("collected", false);
    play.GetComponent<SpriteRenderer>().color = Color.white;
   }

   
   
}

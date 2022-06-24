using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public Transform firePoint;
   public GameObject bulletPrefab;

   public float bulletForce = 20f;
   public float playerKnockbackForce;
    public float fireRate;
    float nextTimeToFire;

   [SerializeField]Rigidbody2D player;

   Vector2 mousePos;
    public Rigidbody2D rb;
    public float difference;
    Vector2 lookDir;

   void Update()
   {
        if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            Shoot();
            nextTimeToFire = Time.time + 1f/fireRate;
            
        }
   }

   void FixedUpdate()
   {
    Aim();
   }

   void Shoot()
   {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right* bulletForce, ForceMode2D.Impulse);
        player.AddForce(-lookDir * playerKnockbackForce, ForceMode2D.Impulse);

   }

   void Aim()
   {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
             lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg  - difference;
           transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
   }
}

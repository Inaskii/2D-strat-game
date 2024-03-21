using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float timeBetweenShots;
    private float nextTimeToShoot;
    public GameObject target;
    public float range;
    public int damage;
    public float rotationSpeed;
    private float angle;
    public string focusOn;
    TargetFinder targetFinder;
    private float distance;
    public GameObject turretNose;
    private ParticleSystem shootParticle;
    public GameObject bulletPrefab;
    Vector2 direction;
    public float bulletSpeed;
    public float spread;
    public int burst;
    private void Start()
    {
        if (burst == 0)
        {
            burst = 1;
        }

        shootParticle = GetComponent<ParticleSystem>();
        turretNose = transform.GetChild(0).gameObject;
        gameObject.tag = transform.parent.tag;
        nextTimeToShoot = Time.time;
        targetFinder = gameObject.AddComponent<TargetFinder>();
        targetFinder.range = range;
        targetFinder.focusOn = focusOn;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            distance = Vector2.Distance(target.transform.position, transform.position);
            if (distance <= range)
            {
                Aim();
                if (transform.rotation == Quaternion.Euler(0, 0, angle) && Time.time >= nextTimeToShoot)
                {
                    Shoot();
                }
                return;
            }
        }
       
            target = targetFinder.FindTarget();
        
    }

    private void Aim()
    {
        direction = target.transform.position - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle),rotationSpeed);
    }
    private void Shoot()
    {
        for(int k=0; k < burst; k++)
        {
            Vector2 dir;
            shootParticle.Play();
            //Debug.DrawRay(transform.position, target.transform.position - transform.position, Color.red, .3f);
            GameObject bullet = Instantiate(bulletPrefab, turretNose.transform.position, Quaternion.Euler(0, 0, angle + 90));
            Rigidbody2D bulletrb = bullet.GetComponent<Rigidbody2D>();
            Debug.DrawRay(transform.position, direction, Color.green,.5f);
            dir = Quaternion.Euler(0, 0, Random.Range(spread / 2, -spread / 2)) * direction;
            Debug.DrawRay(transform.position, dir, Color.red,.5f);


            bulletrb.AddForce(dir.normalized * bulletSpeed, ForceMode2D.Impulse);

            bullet.tag = tag;
            BulletDie bulletdie = bullet.GetComponent<BulletDie>();
            bulletdie.damage = damage;
            bulletdie.target = target;
            nextTimeToShoot = Time.time + timeBetweenShots;


        }
    }
   

}

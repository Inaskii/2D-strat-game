using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    Health targethealth;
    public float timeBetweenShots;
    public GameObject hitPrefab;
    private float nextTimeToShoot;
    public GameObject target;
    public float range;
    public int damage;
    public float rotationSpeed;
    public float angle;
    public string focusOn;
    TargetFinder targetFinder;
    public float distance;
    public GameObject turretNose;
    private void Start()
    {
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
            if (Vector2.Distance(target.transform.position, transform.position) <= range)
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
        Vector2 direction = target.transform.position - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle),rotationSpeed);
    }
    private void Shoot()
    {

        Debug.DrawRay(transform.position, target.transform.position - transform.position, Color.red, .3f);
        //Instantiate(bulletPrefab,)
        Instantiate(hitPrefab,target.transform.position- new Vector3(Random.Range(-.3f, .3f), Random.Range(-.3f, .3f), 2), target.transform.rotation);
        targethealth = target.GetComponent<Health>();
        targethealth.health = targethealth.health - damage;
        
        
        nextTimeToShoot = Time.time + timeBetweenShots;
    }
    
   

}

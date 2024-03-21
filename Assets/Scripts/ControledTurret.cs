using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControledTurret : MonoBehaviour
{

    public float timeBetweenShots;
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
    public ParticleSystem shootParticle;
    public GameObject bulletPrefab;
    Vector2 direction;
    public int x;
    public Vector2 mousePos;
    public Vector3 mouse3Pos;

    private void Start()
    {

        shootParticle = GetComponent<ParticleSystem>();
        turretNose = transform.GetChild(0).gameObject;
        gameObject.tag = transform.parent.tag;
        nextTimeToShoot = Time.time;
        targetFinder = gameObject.AddComponent<TargetFinder>();
        targetFinder.range = range;
        targetFinder.focusOn = focusOn;
    }


    private void Update()
    {
        Aim();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse3Pos = new Vector3(mousePos.x,mousePos.y,0);
        Vector2 mouseDir = mouse3Pos - transform.position;

        if (Input.GetMouseButton(0) && Time.time >= nextTimeToShoot)
        {
            Shoot();
            
        }

    }


    private void Aim()
    {
        direction = mouse3Pos - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed);
    }
    private void Shoot()
    {
        shootParticle.Play();
        GameObject bullet = Instantiate(bulletPrefab, turretNose.transform.position, Quaternion.Euler(0, 0, angle + 90));
        Rigidbody2D bulletrb = bullet.GetComponent<Rigidbody2D>();
        bulletrb.AddForce(direction.normalized * 15, ForceMode2D.Impulse);
        bullet.tag = tag;
        bullet.GetComponent<BulletDie>().damage = damage;





        nextTimeToShoot = Time.time + timeBetweenShots;
    }


}

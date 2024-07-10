using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float timeBetweenShots;
    public bool canShoot;
    public GameObject target;
    public float range;
    public int damage;
    public float rotationSpeed;
    private float angle;
    private string focusOn;
    TargetFinder targetFinder;
    private float distance;
    public List<Nose> turretNoses = new List<Nose>();
    public GameObject bulletPrefab;
    Vector2 direction;
    public float bulletSpeed;
    public float spread;
    public int burst;
    private int noseCount;
    private int actualNose;
    public float burstDelay;
    AudioSource audio;
    
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        canShoot = true;

        if (burst == 0)
        {
            burst = 1;
        }

        noseCount = transform.childCount;
        for(int i=0;i<noseCount;i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            Nose nose = new Nose(child,child.GetComponent<ParticleSystem>());
            turretNoses.Add(nose);
        }



        if (transform.parent != null)
        {
            gameObject.tag = transform.parent.tag;
            if (gameObject.tag == "Ally")
            {
                focusOn = "Enemy";
            }
            else
            {
                focusOn = "Ally";
            }
        }
        else
        {
            
            if (gameObject.tag == "Ally")
            {
                focusOn = "Enemy";
            }
            else
            {
                focusOn = "Ally";
            }

        }
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
                if (transform.rotation == Quaternion.Euler(0, 0, angle) && canShoot)
                {
                    StartCoroutine(Shoot());
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
    private IEnumerator Shoot()
    {
        canShoot = false;
        for(int k=0; k < burst; k++)
        {
            audio.Play();
            Vector2 dir;
            turretNoses[actualNose].noseParticle.Play();
            //Debug.DrawRay(transform.position, target.transform.position - transform.position, Color.red, .3f);
            GameObject bullet = Instantiate(bulletPrefab, turretNoses[actualNose].noseObject.transform.position, Quaternion.Euler(0, 0, angle + 90));
            Rigidbody2D bulletrb = bullet.GetComponent<Rigidbody2D>();
            Debug.DrawRay(transform.position, direction, Color.green,.5f);
            dir = Quaternion.Euler(0, 0, Random.Range(spread / 2, -spread / 2)) * turretNoses[actualNose].noseObject.transform.up ;
            Debug.DrawRay(transform.position, dir, Color.red,.5f);


            bulletrb.AddForce(dir.normalized * bulletSpeed, ForceMode2D.Impulse);

            bullet.tag = tag;
            BulletDie bulletdie = bullet.GetComponent<BulletDie>();
            bulletdie.damage = damage;
            bulletdie.target = target;

            actualNose += 1;
            if (actualNose >= noseCount)
            {
                actualNose = 0;
            }
            yield return new WaitForSeconds(burstDelay);

        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }
   

}
public class Nose
{
    public GameObject noseObject;
    public ParticleSystem noseParticle;

    public Nose(GameObject _noseObject, ParticleSystem _noseParticle)
    {
        noseObject = _noseObject;
        noseParticle = _noseParticle;

    }
}

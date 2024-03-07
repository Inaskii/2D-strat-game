using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    Health targethealth;

    UnitMovement movement;
    public Collider2D[] targetC;
    public GameObject target;
    float targdist;
    public int range;
    private float nextTimeToShoot;
    public int damage;
    public int timeBetweenShots;
    public string focusOn;
    TargetFinder targetFinder;

    void Start()
    {
        nextTimeToShoot = Time.time;
        movement = gameObject.GetComponent<UnitMovement>();
        targetFinder = gameObject.AddComponent<TargetFinder>();
        targetFinder.range = range;
        targetFinder.focusOn = focusOn;

    }

    void Update()
    {
        if (target != null && Vector2.Distance(target.transform.position,transform.position) <= range)
        {
            if (target.GetComponent<Collider2D>().IsTouching(gameObject.GetComponent<Collider2D>()))
            {
                if (Time.time >= nextTimeToShoot)
                {
                    Attack();
                }
            }
            else
            {
                Debug.DrawLine(transform.position, target.transform.position);
                movement.enabled = true;
                movement.Walkto(target.transform.position, 1, false);
            }
            return;
        }
        movement.enabled = false;
        target = targetFinder.FindTarget();
    }
    void Attack()
    {
        targethealth = target.GetComponent<Health>();
        targethealth.health = targethealth.health - damage;
        nextTimeToShoot = Time.time + timeBetweenShots;
        movement.enabled = false;

    }




}

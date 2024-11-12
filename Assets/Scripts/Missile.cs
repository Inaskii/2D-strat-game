using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject target;
    public Bomb bomb;
    public Rigidbody2D rb;
    Vector3 forward;
    public float rotationSpeed;
    public Vector2 direction;
    //public float Fspeed;
    public float speed;
    private Vector3 previousLoc;
    void Start()
    {
        bomb = GetComponent<Bomb>();
        target = GetComponent<BulletDie>().target;
        rb = GetComponent<Rigidbody2D>();
        //speed = Fspeed;
        if (target == null)
        {
            bomb.Explode();
            return;
        }
        previousLoc = target.transform.position;

    }

    void FixedUpdate()
    {
        if (Vector2.Distance(gameObject.transform.position, previousLoc) <= bomb.radius/2)
        {
            bomb.Explode();

        }

        if (target != null)
        {
            previousLoc = target.transform.position;

        }
        direction = previousLoc - transform.position;

        //speed += Fspeed / 100;
        forward = transform.right.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg ;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed);
        //transform.position = transform.position + forward * speed;
        rb.AddForce(forward * speed,ForceMode2D.Impulse);
    }
}

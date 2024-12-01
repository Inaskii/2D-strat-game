using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float radius;
    private int damage;
    BulletDie bulletDie;
    public void Start()
    {
        bulletDie = GetComponent<BulletDie>();
        damage = bulletDie.damage;
    }

    public void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        
        foreach(Collider2D collider in colliders)
        {
            if(collider.TryGetComponent<Health>(out Health health))
            {
                health.health -= damage;
            }
        }
        bulletDie.Die();
        
    }


}

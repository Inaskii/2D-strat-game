using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDie : MonoBehaviour
{
    public ParticleSystem hitPrefab;
    public int damage;
    public GameObject target;
    public bool die;
    public float dieTime;

    void Start()
    {
        if (die)
        {
            Destroy(gameObject, dieTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Health>(out Health health) && !CompareTag(collision.gameObject.tag))
        {
            health.health -= damage;

            if (TryGetComponent(out Bomb bomb))
            {
                bomb.Explode();
            }
            else
            {

                Die();
            }
        }

    }
    public void Die()
    {
        Instantiate(hitPrefab, transform.position - new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), 2), Quaternion.identity);

        Destroy(gameObject);

    }



}

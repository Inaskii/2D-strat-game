using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using UnityEngine;

public class BulletDie : MonoBehaviour
{
    public ParticleSystem hitPrefab;
    public int damage;
    public GameObject target;
    public bool die;
    public float dieTime;

    public enum DamageType
    {
        physical,
        bomb,
        fire
    }

    public DamageType damagetype;

    void Start()
    {
        damagetype = DamageType.physical;
        if (TryGetComponent(out Bomb bomb))
        {
            damagetype = DamageType.bomb;
        }
        if (TryGetComponent(out Fire fire))
        {
            damagetype = DamageType.fire;
        }
        

        if (die)
        {
            Destroy(gameObject, dieTime);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Health>(out Health health) && !CompareTag(collision.gameObject.tag))
        {
            switch (damagetype)
            {
                case DamageType.bomb:
                    GetComponent<Bomb>().Explode();
                    break;
                case DamageType.physical:
                    health.health -= damage;
                    Die();
                    break;
                case DamageType.fire:
                    health.health -= damage;
                    break;
            }
        }
    }

    public void Die()
    {
        if (hitPrefab)
        {
            Instantiate(hitPrefab,
                transform.position - new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), 2),
                Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
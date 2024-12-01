using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public float distanceToDetonate;
    public float actualdistance;
    public Vector2 Fposition;
    Bomb bomb;

    void Start()
    {
        bomb = GetComponent<Bomb>();
        Fposition = transform.position;
        distanceToDetonate = Vector2.Distance(Fposition, GetComponent<BulletDie>().target.transform.position);

    }

    void Update()
    {
        actualdistance = Vector2.Distance(Fposition, transform.position);
        if (actualdistance >= distanceToDetonate)
        {
            bomb.Explode();
            gameObject.GetComponent<BulletDie>().Die();
        }
    }


}

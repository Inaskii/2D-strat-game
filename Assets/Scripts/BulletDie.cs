using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDie : MonoBehaviour
{
    public ParticleSystem bullets;
    void Start()
    {
        Destroy(gameObject, 1);
        
    }
    private void Update()
    {
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    Health health;
    public GameObject spawn;
    public int amount;
    void Start()
    {
        health = GetComponent<Health>();
        health.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(health.health<=0)
        {
            hatch();
        }
    }
    void hatch()
    {
        for (int x = 0; x < amount; x++)
        {
            Instantiate(spawn, transform.position + new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), 0), Quaternion.Euler(0, 0, Random.Range(-180, 180)));
        }
        health.die();
    }
}

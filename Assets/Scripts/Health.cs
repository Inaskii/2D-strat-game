using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    GameObject me;
    public int health;
    public int Maxhealth;
    void Start()
    {
        health = Maxhealth;
        me = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            die();
        }
    }
    public void die()
    {
        Destroy(me);

    }
}

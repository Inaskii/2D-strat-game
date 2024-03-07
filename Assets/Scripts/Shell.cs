using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public float distanceToDetonate;
    Vector2 Fposition;
    // Start is called before the first frame update
    void Start()
    {
        Fposition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(Fposition, transform.position) >= distanceToDetonate)
        {
            //Explode();
        }
    }
    public Shell(float _distance) 
    {
        distanceToDetonate = _distance;
    }
    

}

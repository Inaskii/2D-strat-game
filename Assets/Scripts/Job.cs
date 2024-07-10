using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Job
{
    public GameObject target;
    public int workerAmount;
    public string type;
    private bool done;
    Pathfinder pathfinder = Camera.main.GetComponent<Pathfinder>();
    GameObject Nest = GameObject.FindObjectOfType<Nest>().gameObject;
    public Job(GameObject _target, string _type)
    {
        target = _target;
        type = _type;


    }
}
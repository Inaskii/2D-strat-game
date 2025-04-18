 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntBehaviour : MonoBehaviour
{
    private UnitMovement movement;
    public int order;
    public string state;
    public GameObject target;
    public float collectRange;
    public Inventory inventory;
    public int maxInv;
    private GameObject nestObject;
    public Nest nest;
    public float collectTime;
    public float nextTimetoCollect;
    public GameObject player;
    public Job job;
    public Building actualBuilding;
    public Item nextItem;
    public string jobtype;
    void Start()
    {
        nest = FindAnyObjectByType<Nest>();
        nestObject = nest.gameObject;
        state = "idle";
        movement = GetComponent<UnitMovement>();
        target = null;
        inventory = gameObject.AddComponent<Inventory>();
        nextTimetoCollect = 0;
        player = Camera.main.gameObject;
        jobtype = null;
        
    }
    void Update()
    {
        if (job != null)
        {
            if (job.target != null)
            {
                jobtype = job.type;
                if (job.type == "collect")
                {
                    Collect();
                    return;
                }
                if (job.type == "build")
                {
                    Build();
                    return;
                }
            }
        }
        if (state == "walking")
        {
            Walk();
            return;
        }
        if(state == "collecting")
        {
            Collect();
            return;
        }
        if(job==null)
        {
            target = nestObject;
            //if not walk, ou seja, entra no if se ele ja tiver chegado
            if (!Walk())
            {
                job = nest.GetJob(jobtype);
                
            }

            return;
        }
        if (job.target==null)
        {
            target = nestObject;
            //if not walk, ou seja, entra no if se ele ja tiver chegado
            if (!Walk())
            {
                job = nest.GetJob(jobtype);
            }

        }


    }
    void Collect()
    {

        string resourcename = job.target.GetComponent<Resource>().resourcename;
        if (inventory.itemindex(resourcename) != -1)
        {
            if (inventory.items[inventory.itemindex(resourcename)].amount >= maxInv)
            {
                target = nestObject;
                if (Vector2.Distance(transform.position, target.transform.position) < collectRange)
                {
                    inventory.empty(nestObject);
                }
                Walk();
                return;
            }
        }

        target = job.target;


        if (Vector2.Distance(transform.position, target.transform.position) <= collectRange)
        {
            if (state == "collecting")
            {
                if (Time.time >= nextTimetoCollect)
                {
                    inventory.Add(resourcename, 1);
                    state = "idle";
                }

                return;
            }

            nextTimetoCollect = Time.time + collectTime;
            state = "collecting";
            

        }
        else
        {
            Walk();
        }


    }
    /*
    void TargetClosestResource(string resourcename) 
    {

        resource[] resources = FindObjectsOfType<resource>();
        float smallestDist = 9999;
        GameObject chosen = null;
        foreach (resource resource in resources)
        {
            if (resource.resourcename == resourcename)
            {
                float distance = Vector2.Distance(gameObject.transform.position, resource.transform.position);
                if (distance < smallestDist)
                {
                    chosen = resource.gameObject;
                    smallestDist = distance;
                }
            }
        }
        if (chosen == null)
        {
            return;
        }


        target = chosen;

    }
    */
    bool Walk()
    {
        if(target==null)
        {
            state = "idle";
            return false;
         
        }
        if(Vector2.Distance(transform.position,target.transform.position) <= collectRange)
        {
            state = "idle";
            movement.enabled = false;
           // print("1");
            return false;
        }
        if(movement.enabled == false)
        {
            state = "walking";

            //print("2");
            movement.Walkto(target.transform.position);
            return true;
        }
       // print("3");
        return false;

        
    }
    void Build()
    {


        actualBuilding = job.target.GetComponent<Building>();
        
        foreach (Item item in inventory.items)
        {
            if (item.itemname == nextItem.itemname || item.amount >= nextItem.amount)
            {
                target = job.target;
                //print("walktobuilding");
                Walk();
                if (Vector2.Distance(transform.position, job.target.transform.position) <= collectRange)
                {
                    inventory.empty(job.target);
                    //print("deliver");
                }
                return;


            }

        }



        foreach (Item item in nestObject.GetComponent<Inventory>().items)
        {
            
            if (actualBuilding.cost[0].itemname == item.itemname && actualBuilding.cost[0].amount > 0)
            {
                nextItem = item;
         
                if(nextItem.amount > maxInv)
                {
                    nextItem.amount = maxInv;
                }
                target = nestObject;
                //print("gotonest");
                Walk();
                

            }
        }
        if (target == nestObject)
        {
            if (Vector2.Distance(nest.transform.position, gameObject.transform.position) <= collectRange)
            {
                inventory.get(nextItem.itemname, nestObject);
                //print("getitem");
                return;
            }
        }


    }

}


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
    public GameObject container;
    public GameObject closestResource;
    //STATES: 1 = doing, 0 = done
    void Start()
    {
        state = "idle";
        //idle
        movement = GetComponent<UnitMovement>();
        target = null;
        inventory = gameObject.AddComponent<Inventory>();
    }

    void Update()
    {
      
        if (state == "idle")
        {
            if (order == 1)
            {
                Collect("food");
                return;
            }
        }
        if (state == "walking")
        {
            Walk();
            return;
        }


        
    }
    void Collect(resource resource)
    {
        if (resource == null)
        {

        }

        movement.Walkto(resource.transform.position, 0, false);


    }
    void Collect(string resourcename)
    {
        if (inventory.itemindex(resourcename) != -1)
        {
            if (inventory.items[inventory.itemindex(resourcename)].amount >= maxInv)
            {
                target = container;
                if (Vector2.Distance(transform.position, target.transform.position) < collectRange)
                {
                    inventory.empty(container);
                }
                Walk();
                return;
            }
        }
        if(closestResource == null)
        {
            TargetClosestResource(resourcename);
        }
        else if (closestResource.GetComponent<resource>().resourcename != resourcename)
        {
            TargetClosestResource(resourcename);
        }
        else 
        {
            target = closestResource;
        }


        Walk();

        if (Vector2.Distance(transform.position, target.transform.position) < collectRange)
        {
            Item item = new Item(resourcename, 1);
            inventory.Add(item);

        }


    }
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
    bool Walk()
    {
        if(Vector2.Distance(transform.position,target.transform.position) < collectRange)
        {
            state = "idle";
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
    void Deliver()
    {

    }
    void Build()
    {

    }

}


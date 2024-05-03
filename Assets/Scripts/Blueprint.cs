using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint : MonoBehaviour
{

    public Building building;
    public Building me;
    public Inventory inventory;
    public void Start()
    {

        me = gameObject.AddComponent<Building>();

        me.copyB(building);
        
        inventory = gameObject.AddComponent<Inventory>();
        //cost = gameObject.AddComponent<Cost>();
        //cost.materials = new List<Item>();


    }

    public void Update()
    {
        bool b = true;
        foreach(Item item in building.cost)
        {
            if(item.amount > inventory.items[inventory.itemindex(item.itemname)].amount)
            {
                b = false;
            }
        }



        if(b)
        {
            Instantiate(building.Prefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}

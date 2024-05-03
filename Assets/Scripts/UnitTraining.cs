using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTraining : MonoBehaviour
{
    public Queue<UnitTrain> queue;
    public float nextUnitTime;
    public UnitTrain actualUnit;
    private Inventory NestBuildingContainer;
    public Nest nest;

    public void Start()
    {
        nest = GameObject.FindObjectOfType<Nest>();
        queue = new Queue<UnitTrain>();
        NestBuildingContainer = GameObject.FindObjectOfType<Nest>().GetComponent<Inventory>();
    }
    private void Update()
    {
        if (actualUnit != null)
        {
            if (Time.time >= nextUnitTime)
            {
                GameObject unit = Instantiate(actualUnit.unitPrefab, gameObject.transform.position + new Vector3(0, 1, 0.1f), transform.rotation);
                if(unit.TryGetComponent<AntBehaviour>(out AntBehaviour antBehaviour))
                {
                    nest.workers.Add(antBehaviour);
                }
                if (queue.Count > 0)
                {
                    actualUnit = queue.Dequeue();
                    nextUnitTime = Time.time + actualUnit.trainTime;
                }
                else
                {
                    actualUnit = null;
                }
            }
        }
        else if(queue.Count>0)
        {
            actualUnit = queue.Dequeue();
            nextUnitTime = Time.time + actualUnit.trainTime;

        }

    }
    public void Train(UnitTrain unit)
    {
        
        if(!testPrice(unit))
        {
            print("Not enough resources");
            return;
        }

        foreach (Item price in unit.price)
        {
            NestBuildingContainer.spend(price.itemname,price.amount);
        }
        queue.Enqueue(unit);

    }

    public bool testPrice(UnitTrain unit)
    {
        foreach (Item price in unit.price)
        {
            foreach (Item containerItem in NestBuildingContainer.items)
            {
                if (containerItem.itemname == price.itemname)
                {
                    if (!(containerItem.amount >= price.amount))
                    {
                        return false;

                    }
                }
            }
        }
        return true;
    }

}


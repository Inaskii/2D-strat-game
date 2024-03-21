using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public List<Item> items;
    private void Start()
    {
        items = new List<Item>();
    }
    public void Add(Item item)
    {
        int index = 0;
        if (items.Count == 0)
        {
            items.Add(item);
            return;
        }
        foreach (Item _item in items)
        {

            if (_item.itemname == item.itemname)
            {
                items[index].amount += item.amount;
                return;

            }
            index += 1;
        }

        items.Add(item);
    }
    public void empty(GameObject container)
    {
        Inventory containerINV = container.GetComponent<Inventory>();
        foreach (Item item in items)
        {
            containerINV.Add(item);
        }
        items = new List<Item>();
    }
    public int itemindex(string itemname)
    {
        int index = 0;
        foreach(Item _item in items)
        {
            if (_item.itemname == itemname)
            {
                return index;
            }
        }
        return -1;
    }


}


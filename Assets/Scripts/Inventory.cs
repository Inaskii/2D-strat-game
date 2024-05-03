using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Inventory : MonoBehaviour
{

    public List<Item> items;
    private void Start()
    {
        items = new List<Item>();
        Item food;
        food.amount = 0;
        food.itemname = "food";
        items.Add(food);
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
                Item i = items[index];
                i.amount += item.amount;
                items[index] = i;
                return;

            }
            index += 1;
        }

        items.Add(item);
    }
    public void Add(string itemName, int amount)
    {
        Item item;
        item.amount = amount;
        item.itemname = itemName;
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
                Item i = items[index];
                i.amount += item.amount;
                items[index] = i;
                return;

            }
            index += 1;
        }

        items.Add(item);

    }
    public void Add(string itemName)
    {
        Item item;
        item.amount = 1;
        item.itemname = itemName;
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
                Item i = items[index];
                i.amount += item.amount;
                items[index] = i;
                return;

            }
            index += 1;
        }

        items.Add(item);


    }
    public void get(string itemName, GameObject container)
    {
        Item tempItem;
        int itemamount = -1;
        Inventory containerINV = container.GetComponent<Inventory>();
        foreach (Item _item in containerINV.items)
        {
            if (_item.itemname == itemName)
            {

                itemamount = containerINV.items[containerINV.itemindex(itemName)].amount;
                itemamount -= 1;

            }
        }
        if (itemamount == -1)
        {
            return;
        }
        tempItem.amount = itemamount;
        tempItem.itemname = itemName;

        containerINV.items[containerINV.itemindex(itemName)] = tempItem;
        Add(itemName);

    }
    public void get(string itemName, GameObject container,int amount)
    {
        Item tempItem;
        int itemamount = -1;
        Inventory containerINV = container.GetComponent<Inventory>();
        foreach (Item _item in containerINV.items)
        {
            if (_item.itemname == itemName)
            {

                itemamount = containerINV.items[containerINV.itemindex(itemName)].amount;
                itemamount -= amount;

            }
        }
        if (itemamount == -1)
        {
            return;
        }
        tempItem.amount = itemamount;
        tempItem.itemname = itemName;

        containerINV.items[containerINV.itemindex(itemName)] = tempItem;
        Add(itemName,amount);

    }
    public bool spend(string itemName, int amount)
    {
        if(items[itemindex(itemName)].amount <amount)
        {
            return false;
        }

        Item item;
        item.itemname = itemName;
        item.amount = items[itemindex(itemName)].amount - amount;
        items[itemindex(itemName)] = item;
        return true;



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
        if (items.Count > 0)
        {
            foreach (Item _item in items)
            {
                if (_item.itemname == itemname)
                {
                    return index;
                }
            }
        }
        return -1;
    }


}


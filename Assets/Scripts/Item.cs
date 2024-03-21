using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string itemname;
    public int amount;
    public Item(string name, int num)
    {
        itemname = name;
        amount = num;
    }
}
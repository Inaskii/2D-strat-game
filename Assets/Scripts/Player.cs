using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public int coin;
    GameObject mainBuilding;
    Inventory mainInv;

    void Start()
    {
        coin = 0;
        mainBuilding = GameObject.FindObjectOfType<Nest>().gameObject;
        mainInv = mainBuilding.GetComponent<Inventory>();
    }

    void Update()
    {

        coin = mainInv.items[mainInv.itemindex("food")].amount;
        coinText.text = coin.ToString();

    }
}

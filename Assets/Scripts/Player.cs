using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public int coin;
    public GameObject mainBuilding;
    private Inventory mainInv;

    void Start()
    {
        coin = 0;
        mainInv = mainBuilding.GetComponent<Inventory>();
    }

    void Update()
    {

        coin = mainInv.items[mainInv.itemindex("food")].amount;
        coinText.text = coin.ToString();

    }
}

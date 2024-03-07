using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public int coin;
    


    void Start()
    {
        
    }

    void Update()
    {
        coin++;
        coinText.text = coin.ToString();

    }
}

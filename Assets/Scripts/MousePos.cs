using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePos : MonoBehaviour
{
    Vector3 realPos, gridPos,HexPos;
    int x, y;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        realPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        gridPos = new Vector3Int(Mathf.RoundToInt(realPos.x), Mathf.RoundToInt(realPos.y), 0);

       // HexPos = new Vector3(Redondo(realPos.x, Mathf.Cos(30 * Mathf.Deg2Rad)),Redondo(realPos.y, Mathf.Sin(30 * Mathf.Deg2Rad)), 0);
        
        
        transform.position = gridPos;
        
    }
    /*float Redondo(float n, float v)
    {
        
        n = Mathf.RoundToInt(n / v);
        n = n*v;
        return n;
    }*/
}

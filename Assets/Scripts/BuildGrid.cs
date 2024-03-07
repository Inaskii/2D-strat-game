using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGrid : MonoBehaviour
{
    DragSelect dragSelect;
    public GameObject[] buildingGhost; 
    public GameObject[] building;
    public GameObject mouse;
    public bool b = false;
    public int[] size;
    public float distance;
    
    Transform mouseT;
    public int k;
    void Start()
    {
        dragSelect = GetComponent<DragSelect>();
        mouseT = mouse.GetComponent<Transform>();
        
    }

    void Update()
    {
        dragSelect.enabled = !b;

        
        if (Input.GetKeyDown("q"))
        {
            if (mouseT.childCount > 0)
            {
                DestroyImmediate(mouseT.GetChild(0).gameObject);
            }
            k = 1;
            Instantiate(buildingGhost[k-1],mouse.transform);
            b = true;
        }
        if (Input.GetKeyDown("e"))
        {
            if (mouseT.childCount > 0)
            {
                DestroyImmediate(mouseT.GetChild(0).gameObject);
            }
            k = 2;
            Instantiate(buildingGhost[k - 1], mouse.transform);
            b = true;

        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            
            b = false;
            k = 0;
            if (mouseT.childCount > 0)
            {
                DestroyImmediate(mouseT.GetChild(0).gameObject);
            }

        }

        if (Input.GetMouseButton(0) && b == true)
        {
            Collider2D[] collider;
            
            distance = size[k - 1]*Mathf.Cos(Mathf.Deg2Rad * 30)/2;
            collider = Physics2D.OverlapCircleAll(mouse.transform.position,distance);

            if (collider.Length == 0) 
            {
                Instantiate(building[k - 1], mouse.transform.position, Quaternion.Euler(0, 0, 0));
            } 
        }
    }
    public void Ghost(int n) 
    {
        if (mouseT.childCount > 0)
        {
            DestroyImmediate(mouseT.GetChild(0).gameObject);
        }
        Instantiate(buildingGhost[n - 1], mouse.transform);
        k = n;
        b = true;
    }
    


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public List<Item> cost;
    public GameObject Prefab;
    public GameObject GhostPrefab;
    public int BuildTime;
    public Vector3 size;
    public void copyB(Building B)
    {
        cost = B.cost;
        Prefab = B.Prefab;
        GhostPrefab = B.GhostPrefab;
        BuildTime = B.BuildTime;
        
    }
    
}

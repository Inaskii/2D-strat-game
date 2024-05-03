using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growth : MonoBehaviour
{
    public List<Vector2> neighbour;
    public int MaxNCount;
    public bool active;
    public LayerMask layerMask;
    public int k;
    public int N;
    public int count;
    public Vector2 selected;
    public float size;
    public List<GameObject> buildingPrefabs;
    public GameObject sporePrefab;
    public List<GameObject> spores;
    public int SlowBubbleFactor;
    int i;
    public int maxSpore;
    public int maxMain;
    public int SpawnIndex;
    private void Start()
    {
        i = 0;
        spores.Add(gameObject);
        if(N==0)
        {
            N = 30;
        }
        k = -1;
    }
    private void Update()
    {

        k += 1;
        if(k>=N)
        {
            UpdateN();
            k = 0;

        }
    }
    void UpdateN()
    {
        SpawnIndex = 0;
        bool b = false;
        if(spores.Count==0)
        {
            enabled = false;
        }
        int n = Random.Range(0, spores.Count - 1);
        b = false;
        if (spores[n] == null)
        {
            b = true;
            spores.Remove(spores[n]);
            return;
        }
        if(maxSpore<=0)
        {
            if(maxMain<=0)
            {
                return;
            }
            SpawnIndex = 1;

        }

        selected = spores[n].transform.position;


        neighbour = RandomizeNeighbour(FindNeighbour(selected));

        Vector2 Tselected = new Vector2(0,0);
        count = 9;
        int Tcount;
        foreach(Vector2 vector2 in neighbour)
        {
            if(Physics2D.OverlapPoint(vector2,layerMask))
            {
                continue;
            }
            Tcount = Countneighbour(vector2);

            if(Tcount < count)
            {
                Tselected = vector2;
                count = Tcount;
                
            }

        }
        if (Tselected == new Vector2(0, 0))
        {
            return;
        }
        InsertSpore(Tselected);

    }
    List<Vector2> FindNeighbour(Vector2 vector)
    {
        List<Vector2> nebas = new List<Vector2>();
        nebas.Add(vector + new Vector2(0, size));
        nebas.Add(vector + new Vector2(size, size));
        nebas.Add(vector + new Vector2(size, 0));
        nebas.Add(vector + new Vector2(size, -size));
        nebas.Add(vector + new Vector2(0, -size));
        nebas.Add(vector + new Vector2(-size, -size));
        nebas.Add(vector + new Vector2(-size, 0));
        nebas.Add(vector + new Vector2(-size, size));
        /*foreach (node V in neighbour)
            {
                Debug.DrawLine(vector, V.pos, Color.black, 1);
            }*/
        
        return nebas;
    }
    public int Countneighbour(Vector2 ng)
    {
        
        return Physics2D.OverlapCircleAll(ng, size, layerMask).Length;
    
    }
    public List<Vector2> RandomizeNeighbour(List<Vector2> ng)
    {
        for (int i = ng.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Vector2 temp = ng[i];
            ng[i] = ng[j];
            ng[j] = temp;
        }
        return ng;
    }
    public void InsertSpore(Vector2 position)
    {
        GameObject spore = Instantiate(buildingPrefabs[SpawnIndex], position, transform.rotation);
        spores.Add(spore);
        if (SpawnIndex == 0)
        {
            maxSpore -= 1;
        }
        if(SpawnIndex==1)
        {
            maxMain -= 1;
        }
        return;

    }

}

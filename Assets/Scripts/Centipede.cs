using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centipede : MonoBehaviour
{
    public float radius;
    public float speed;
    public List<GameObject> parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int x = 1; x < parent.Count; x++)
        {
            Vector2 direction = parent[x].transform.position - parent[x-1].transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            parent[x].transform.rotation = Quaternion.Lerp(parent[x].transform.rotation, Quaternion.Euler(0, 0, angle), .1f);
            if (Vector2.Distance(parent[x].transform.position, parent[x-1].transform.position) > radius)
            {
                //parent[x].transform.position += (parent[x].transform.up * speed);
                parent[x].transform.position = Vector2.Lerp(parent[x].transform.position, parent[x - 1].transform.position,speed);
            }
        }
    }
}

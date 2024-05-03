using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGrid : MonoBehaviour
{
    DragSelect dragSelect;
    public List<Building> buildings;

    public GameObject mouse;
    public Vector2 buildPos;
    public bool b = false;
    public int[] size;
    public float distance;
    public List<Job> jobs = new List<Job>();
    public int k;
    GameObject ghost;
    public GameObject highlight;
    public bool buildable;

    public Nest nest;
    void Start()
    {
        nest = GameObject.FindObjectOfType<Nest>();
        dragSelect = GetComponent<DragSelect>();
        
    }

    void Update()
    {

        List<Job> jobes = jobs;

        int index = 0;
        foreach(Job job in jobs)
        {
            if(job.target == null && job.type=="build")
            {
                jobes.Remove(job);
            }
            index++;
        }
        jobs = jobes;

        dragSelect.enabled = !b;

        
        if (Input.GetKeyDown("q"))
        {
            Ghost(1);
        }
        if (Input.GetKeyDown("e"))
        {
            Ghost(2);

        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            
            b = false;
            k = 0;
            if (mouse.transform.childCount > 0)
            {
                DestroyImmediate(mouse.transform.GetChild(0).gameObject);
            }
        }
        GameObject resource = null;
        GameObject building = null;

        if (b)
        {
            ghost.GetComponent<Collider2D>().enabled = false;
            if (!Physics2D.OverlapArea(ghost.transform.position - buildings[k].size / 2, ghost.transform.position + buildings[k].size / 2))
            {
                buildable = true;
                highlight.GetComponent<SpriteRenderer>().color = Color.blue;
                highlight.GetComponent<SpriteRenderer>().size = buildings[k].size;

            }
            else
            {
                buildable = false;
                highlight.GetComponent<SpriteRenderer>().color = Color.red;
                highlight.GetComponent<SpriteRenderer>().size = buildings[k].size;
            }

            if (Input.GetMouseButton(0))
            {
                planBuild(k);

            }

        }
        else
        {
            Collider2D[] colliders = Physics2D.OverlapAreaAll(highlight.transform.position - (Vector3.one / 2.5f), highlight.transform.position + (Vector3.one / 2.5f));
            foreach(Collider2D collider in colliders)
            {
                if (collider.GetComponent<Resource>())
                {
                    resource = collider.gameObject;
                }
                if(collider.GetComponent<Blueprint>())
                {
                    building = collider.gameObject;
                }
            }
            highlight.GetComponent<SpriteRenderer>().size = new Vector2(1, 1);
            highlight.transform.position = mouse.transform.position;

            if (resource !=null)
            {
                highlight.GetComponent<SpriteRenderer>().color = Color.blue;
            }
            else if(building!=null)
            {
                highlight.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                highlight.GetComponent<SpriteRenderer>().color = Color.white;

            }

        }
        if (Input.GetMouseButtonDown(1))
        {
            Job job = null;
            if (resource!=null)
            {
                job = new Job(resource, "collect");
                nest.collectJobs.Add(new Job(resource, "collect"));

            }
            if (building!=null)
            {
                job = new Job(building, "build");
            }
            if(job!=null)
            foreach (GameObject selected in dragSelect.selected)
            {
                if (selected.TryGetComponent<AntBehaviour>(out AntBehaviour behaviour))
                {
                    behaviour.job = job;
                }
            }
        }


    }
    public void Ghost(int n) 
    {
        if (mouse.transform.childCount > 0)
        {
            DestroyImmediate(mouse.transform.GetChild(0).gameObject);
        }
        highlight.transform.position = mouse.transform.position;

        ghost = Instantiate(buildings[n].GhostPrefab, mouse.transform);
        ghost.transform.position += ((buildings[n].size - new Vector3(1, 1, 0)) / 2);
        highlight.transform.position += ((buildings[n].size - new Vector3(1, 1, 0)) / 2);
        k = n;
        b = true;
    }

    public void planBuild(int n)
    {
        if(!buildable)
        {
            return;
        }


        GameObject bluePrint = Instantiate(buildings[n].GhostPrefab, ghost.transform.position, Quaternion.Euler(0, 0, 0));
        Blueprint bp = bluePrint.AddComponent<Blueprint>();
        bp.building = buildings[n];
       
        Job job1 = new Job(bluePrint,"build");
        jobs.Add(job1);


    }
    


}

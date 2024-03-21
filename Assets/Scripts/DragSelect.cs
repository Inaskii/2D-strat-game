using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragSelect : MonoBehaviour
{
    UnitMovement movement;
    public Vector2 FP;
    public Vector2 SP;
    public Collider2D[] units;
    public int n = 0;
    public List<GameObject> selected;
    public List<GameObject> particles;
    public GameObject square;
    Vector2 SQ;
    public List<Vector2> path;
    public bool walkAttack;
    public GameObject particle;
    public LayerMask layerMask;
    // first mouse click position, second mouse click position
    void Start()
    {
        selected = new List<GameObject>();

    }

    void Update()
    {
        

        if (Input.GetKeyDown("r"))
        {
            walkAttack = true;
        }
        if(Input.GetMouseButtonDown(0))
        {
            walkAttack = false;

            FP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
          //  Debug.Log(FP);
        }
        
        if (Input.GetMouseButton(0))
        {
            SQ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            square.SetActive(true);
            Vector3 vector3;
            vector3 = new Vector3(FP.x, FP.y, -0.5f);
            square.transform.position = vector3;
            square.transform.localScale = SQ-FP;
        }

        if (Input.GetMouseButtonUp(0))
        {
            foreach(GameObject particle in particles)
            {
                Destroy(particle);
            }
            particles = new List<GameObject>();
            square.SetActive(false);
            n = 0;
            SP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //   Debug.Log(SP);
            units =  Physics2D.OverlapAreaAll(FP,SP,layerMask);
            selected = new List<GameObject>();
                foreach (Collider2D collider2D in units)
                {

                    string x = collider2D.tag;
                    if (x == "Ally")
                    {
                        selected.Add(collider2D.gameObject);
                        //Debug.Log(selected[n].name);
                        n++;
                        particles.Add(Instantiate(particle,collider2D.gameObject.transform));
                        



                }
            }
        }
        if (Input.GetMouseButtonUp(1))
        {

            foreach (GameObject GO in selected)  
            {

                if (GO != null)
                {
                    movement = GO.GetComponent<UnitMovement>();
                    if (movement != null)
                    {
                        movement.enabled = true;
                        movement.Walkto(Camera.main.ScreenToWorldPoint(Input.mousePosition),n,walkAttack);
                    }
                }
            }
            walkAttack = false;
        }

    }
    
}

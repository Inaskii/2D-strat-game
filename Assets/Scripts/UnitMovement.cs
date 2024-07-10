using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{

    float rotation;
    Vector2 direction;
    public float turnSpeed;
    Quaternion rotquat;
    public Vector2 walkpos;
    public float speed;
    int num;
    Collider2D[] radius;
    float dist;
    Vector2 direc;
    Vector2 position;
    public int size;
    public List<Vector2> path;
    public int j;
    public bool avoid;
    public bool walkAttack;
    public Turret turret;
    Pathfinder pathfinder;
    Coroutine coroutine;
    Vector2 currentPath;
    public void Awake()
    {
        pathfinder = Camera.main.GetComponent<Pathfinder>();
        turret = GetComponentInChildren<Turret>();   
        position = transform.position;
        this.enabled = false;

    }
    
    private void FixedUpdate()
    {
        if (walkAttack == true && turret.target!=null)
        {
            return;
        }
        if ( (j < path.Count-1) || (path.Count == 1 && j<1) )
        {
            direction = path[j] - position;
            currentPath = path[j];
        }
        else 
        {
            enabled = false;
            
        }
        position = transform.position;
        //Debug.DrawLine(position, path[j]);
        if(avoid == true)
        {
            Avoid();
        }

        
        rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 270;
        rotquat = Quaternion.Euler(0, 0, rotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotquat, turnSpeed);
        
        if (Vector2.Distance(currentPath, position) > Mathf.Sqrt(num)/4)
        {
            transform.position = transform.position + transform.up * speed;
        }
        else
        {
            j++;
            transform.position = transform.position + transform.up * speed;
        }
       
    }
    public void Walkto(Vector2 wkp, int n = 1, bool walkAttack_ = false)
    {
        enabled = false;
        position = transform.position;
        num = n;
        walkpos = wkp;
        //path = Camera.main.GetComponent<DragSelect>().path;
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
  
        coroutine = StartCoroutine(pathfinder.FindPath(position, walkpos,Walk));
        j = 0;
        walkAttack = walkAttack_;
    }
    public void Walk(List<Vector2> _path) 
    {
        if(_path == null)
        {
            enabled = false;
            return;
        }
        enabled = true;
        path = _path;

    }
    public void FollowPath(List<Vector2> _path)
    {
        enabled = true;
        num = 1;
        path = _path;

        j = 0;
        walkAttack = false;

    }

    void Avoid()
        {
            radius = Physics2D.OverlapCircleAll(position, size);
            foreach (Collider2D collider in radius)
            {
                //Debug.Log(collider.gameObject.name);
                dist = Vector2.Distance(collider.gameObject.transform.position, transform.position);
                direc = (position - new Vector2(collider.gameObject.transform.position.x, collider.gameObject.transform.position.y)).normalized;
                if (gameObject.layer == collider.gameObject.layer)
                {

                    direction = (direction + size * direc / (dist*dist)).normalized;
                    Debug.DrawRay(position, direc, Color.yellow);
                    Debug.DrawRay(position, direction, Color.red);
                }
            }
        }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    public Collider2D[] targetC;
    public List<Collider2D> colliders;
    public int n;
    public float range;
    public GameObject target;
    public string focusOn;
    int frames = 0;
    public float distance;
    private void Awake()
    {
        
        targetC = Physics2D.OverlapCircleAll(transform.position, range);
        n = targetC.Length;
        colliders = new List<Collider2D>();
        foreach (Collider2D collider2d in targetC)
        {
            if (collider2d.gameObject.tag == focusOn)
            {
                colliders.Add(collider2d);
            }

        }
    }
    private void FixedUpdate()
    {
        frames++;
        if (frames == 10)
        {
            frames = 0;
            Fixed10Update();
        }
    }
    private void Fixed10Update()
    {
        targetC = Physics2D.OverlapCircleAll(transform.position, range);

        if (targetC.Length != n)
        {
            colliders = new List<Collider2D>();
            foreach (Collider2D collider2d in targetC)
            {
                if (collider2d.gameObject.tag == focusOn)
                {
                    colliders.Add(collider2d);
                }

            }
            n = targetC.Length;
        }
    }
    public GameObject FindTarget()
    {
        target = null;
        float targdist;
        targdist = Mathf.Infinity;
        foreach (Collider2D collider2d in colliders)
        {
            if (collider2d != null)
            {
                
                float dist = Vector2.Distance(collider2d.transform.position, transform.position);
                if (dist <= targdist && dist <= range)
                {
                    target = collider2d.gameObject;
                    targdist = Vector2.Distance(target.transform.position, transform.position);
                }
            }
        }

        return target;
    }
    
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    public LayerMask layerMask;
    private void Awake()
    {
        layerMask = LayerMask.GetMask("Unit", "Building");
        
        targetC = Physics2D.OverlapCircleAll(point:transform.position,radius: range, layerMask:layerMask);
        n = targetC.Length;
        colliders = new List<Collider2D>();
        foreach (Collider2D collider2d in targetC)
        {
            //print(collider2d.gameObject.name);
            if (collider2d.gameObject.tag == focusOn)
            {
                colliders.Add(collider2d);
            }

        }
    }
 
    

    public IEnumerator FindTarget(Action<GameObject> onTargetFound)
    {
        int t;
        while (true)
        {
            target = null;
            float targdist;
            targdist = Mathf.Infinity;
            targetC = Physics2D.OverlapCircleAll(point: transform.position, radius: range, layerMask: layerMask);

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

            t = 15;
            while (t-- != 0) 
            {
                yield return new WaitForEndOfFrame();
            }

            foreach (Collider2D collider2d in colliders)
            {
                if (collider2d == null)
                {
                    continue;
                }
                float dist = Vector2.Distance(collider2d.transform.position, transform.position);
                if (dist <= targdist && dist <= range)
                {

                    target = collider2d.gameObject;
                    targdist = Vector2.Distance(target.transform.position, transform.position);
                }

            }

            onTargetFound(target);

            t = 15;
            while (t-- != 0) 
            {
                yield return new WaitForEndOfFrame();
            }


        }


    }
    private void OnDrawGizmosSelected()
    {
        // Visualize the range in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}


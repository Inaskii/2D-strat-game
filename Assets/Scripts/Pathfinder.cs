using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public List<node> open = new List<node>();
    public List<node> closed = new List<node>();
    public List<Vector2> _closed = new List<Vector2>();
    public List<Vector2> _open = new List<Vector2>();
    public List<Vector2> path = new List<Vector2>();
    node[] neighbour = new node[8];
    public LayerMask layerMask;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public List<Vector2> FindPath(Vector2 from, Vector2 to)
    {
       
        

        from = new Vector2(Mathf.RoundToInt(from.x), Mathf.RoundToInt(from.y));
        to = new Vector2(Mathf.RoundToInt(to.x), Mathf.RoundToInt(to.y));
        open = new List<node>();
        _open = new List<Vector2>();
        path = new List<Vector2>();
        closed = new List<node>();
        _closed = new List<Vector2>();
        node current;
        current = new node(from);
        open.Add(current);
        _open.Add(current.pos);
        for (int k = 0; k < 1000; k++)
        {
          //  Debug.DrawLine(from, current.pos, Color.white, 1);
            current = open[0];
            foreach (node VT in open)
            {

                if (Vector2.Distance(current.pos, to) >= Vector2.Distance(VT.pos, to))
                {
                    current = VT;
                }

                // pega menor f cost
            }
            open.Remove(current);
            _open.Remove(current.pos);

            closed.Add(current);
            _closed.Add(current.pos);
            if (current.pos == to)
            {
                RetracePath();
                break;
            }
            Cneighbour(current.pos,current);
            //pega os vizinho do current
            foreach (node vector in neighbour)
            {
                if (Physics2D.OverlapCircle(vector.pos,.4f,layerMask) || _closed.Contains(vector.pos))
                {
                    //Debug.Log("continue");
                    continue;
                    //ve se o vizinho é invalido
                }
                if (Vector2.Distance(current.pos, from) + Vector2.Distance(current.pos, vector.pos) < Vector2.Distance(vector.pos, from) || !_open.Contains(vector.pos))
                {
                    if (!_open.Contains(vector.pos))
                    {
                        open.Add(vector);
                        _open.Add(vector.pos);
                    }

                }

            }

        }
        void RetracePath()
        {

            node CurrentNode = closed[closed.Count - 1];
            while(CurrentNode.pos != from)
            {
                Debug.DrawLine(CurrentNode.pos, CurrentNode.parent.pos,Color.black,1);
                path.Add(CurrentNode.pos);
                CurrentNode = CurrentNode.parent;
            }
            path.Reverse();
        }
       
        return path;


    }



void Cneighbour(Vector2 vector,node current)
        {
            neighbour[0] = new node((vector + new Vector2(0, 1)),current);
            neighbour[1] = new node((vector + new Vector2(1, 1)), current);
            neighbour[2] = new node((vector + new Vector2(1, 0)), current);
            neighbour[3] = new node((vector + new Vector2(1, -1)), current);
            neighbour[4] = new node((vector + new Vector2(0, -1)), current);
            neighbour[5] = new node((vector + new Vector2(-1, -1)), current);
            neighbour[6] = new node((vector + new Vector2(-1, 0)), current);
            neighbour[7] = new node((vector + new Vector2(-1, 1)), current);
        /*foreach (node V in neighbour)
            {
                Debug.DrawLine(vector, V.pos, Color.black, 1);
            }*/
        
        }


    }



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
    public node current;
    node[] neighbour = new node[8];
    public LayerMask layerMask;
    int i;
    public List<Path> paths = new List<Path>();

    // Start is called before the first frame update
    void Start()
    {
        i = 0;

    }

    // Update is called once per frame
    void Update()
    {
    }
    public List<Vector2> FindPath(Vector2 from, Vector2 to)
    {

        while(Physics2D.OverlapPoint(to,layerMask))
        {
            Vector2 direction = (from - to).normalized;
            to = to - direction;
        }



        from = new Vector2(Mathf.RoundToInt(from.x), Mathf.RoundToInt(from.y));
        to = new Vector2(Mathf.RoundToInt(to.x), Mathf.RoundToInt(to.y));

        foreach (Path path in paths)
        {
            if ((path.from == from) && (path.to == to))
            {
                if (checkpath(path.path))
                {
                    return path.path;
                }
                else
                {
                    paths.Remove(path);
                    break;
                }
            }
        }

        open = new List<node>();
        _open = new List<Vector2>();
        path = new List<Vector2>();
        closed = new List<node>();
        _closed = new List<Vector2>();
        current = new node(from);
        open.Add(current);
        _open.Add(current.pos);
        for (int k = 0; k < 3000; k++)
        {
          //  Debug.DrawLine(from, current.pos, Color.white, 1);
            current = open[0];
            foreach (node VT in open)
            {

                if (Vector2.Distance(current.pos, to) > Vector2.Distance(VT.pos, to))
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
                paths.Add(new Path(from, to, path));
                return path;
            }
            Cneighbour(current.pos,current);
            //pega os vizinho do current
            foreach (node vector in neighbour)
            {
                if (Physics2D.OverlapPoint(vector.pos,layerMask) || _closed.Contains(vector.pos))
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
        
        float currentDist = Mathf.Infinity;
        foreach(node node in closed)
        {
            float dist = Vector2.Distance(to, node.pos);
            print(dist);
            if (dist < currentDist)
            {
                currentDist = dist;
                current = node;


            }
            print(currentDist);
            
        }
        RetracePath();
        


        void RetracePath()
        {

            while(current.pos != from)
            {
                
                Debug.DrawLine(current.pos, current.parent.pos,Color.black,1);
                path.Add(current.pos);
                current = current.parent;
            }
            path.Reverse();
        }
        paths.Add(new Path(from, to, path));
        return path;


    }
    bool checkpath(List<Vector2> path)
    {
        foreach(Vector2 vector in path)
        {
            if(Physics2D.OverlapPoint(vector, layerMask))
            {
                return false;
            }

        }

        return true;
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
        foreach (node V in neighbour)
            {
                Debug.DrawLine(vector, V.pos, Color.black, 1);
            }
        
        }


    }


[System.Serializable]
public class Path
{
    public Vector2 from;
    public Vector2 to;
    public List<Vector2> path;

    public Path(Vector2 _from, Vector2 _to, List<Vector2> _path)
    {
        from = _from;
        to = _to;
        path = _path;

    }
}



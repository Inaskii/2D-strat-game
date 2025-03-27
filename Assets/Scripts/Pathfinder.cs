using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Threading;
using UnityEngine.UIElements;
using Unity.Mathematics;
using System.Linq;

public class Pathfinder : MonoBehaviour
{
    Heap<(Node,double)> open = new Heap<(Node,double)>();
    public List<Node> closed = new List<Node>();
    public List<Vector2> _closed = new List<Vector2>();
    public List<Vector2> _open = new List<Vector2>();
    public List<Vector2> path = new List<Vector2>();
    public Node current;
    Node[] neighbour = new Node[8];
    public LayerMask layerMask;
    //float i;
    public List<Path> paths = new List<Path>();
    public Queue<Path> pathQueue;
    Path currentPath;
    
    Thread thread;
    void Start()
    {
        pathQueue = new Queue<Path>();

    }

    void Update()
    {
    }

    public IEnumerator FindPath(Vector2 from, Vector2 to, Action< List<Vector2> > onpathFound)
    {
        
        Path newPath = new Path(from, to, null);
        pathQueue.Enqueue(newPath);


        while (Physics2D.OverlapPoint(to, layerMask))
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
                    onpathFound(path.path);
                    yield break;
                }
                else
                {
                    paths.Remove(path);
                    break;
                }
            }
        }

        open = new Heap<(Node, double)>();
        //_open = new List<Vector2>();
        path = new List<Vector2>();
        closed = new List<Node>();
        //_closed = new List<Vector2>();
        current = new Node(from);
        open.Enqueue((current,0));
        //_open.Add(current.pos);
        for (int k = 0; k < 200; k++)
        {
            for (int i = 0; i < 50; i++)
            {
                if (open.Count == 0)
                {
                    yield break;
                }
                var cur = open.Dequeue();

                /*

                foreach (Node VT in open)
                {

                    if ((current.gcost + current.Calchcost(to)) > (VT.gcost + VT.Calchcost(to)))
                    {
                        current = VT;
                    }

                }
                */

                if (Vector2.Distance(current.pos, to) <= 2)
                {
                    current = new Node(to, current);
                    RetracePath();
                    paths.Add(new Path(from, to, path));
                    onpathFound(path);
                    yield break;
                }



                
                closed.Add(current);
                

                Cneighbour(current.pos, current);
                //pega os vizinho do current
                foreach (Node vector in neighbour)
                {
                    if (Physics2D.OverlapPoint(vector.pos, layerMask) || closed.Contains(vector))
                    {
                        //Debug.Log("continue");
                        continue;
                        //ve se o vizinho � invalido
                    }
                    if (Vector2.Distance(current.pos, from) + Vector2.Distance(current.pos, vector.pos) < Vector2.Distance(vector.pos, from) || !open.Contains(vector))
                    {
                        if (!open.Contains(vector))
                        {
                            open.Add(vector);
                            //_open.Add(vector.pos);
                        }

                    }

                }
            }
            yield return new WaitForEndOfFrame();

        }

        float currentDist = Mathf.Infinity;
        foreach (Node node in closed)
        {
            float dist = Vector2.Distance(to, node.pos);
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

            while (current.pos != from)
            {

                Debug.DrawLine(current.pos, current.parent.pos, Color.black, 1);
                path.Add(current.pos);
                current = current.parent;
            }
            path.Reverse();

        }
        paths.Add(new Path(from, to, path));
        onpathFound(path);
        yield break;



    }
    bool checkpath(List<Vector2> path)
    {
        foreach (Vector2 vector in path)
        {
            if (Physics2D.OverlapPoint(vector, layerMask))
            {
                return false;
            }

        }

        return true;
    }

    void Cneighbour(Vector2 vector, Node current)
    {
        neighbour[0] = new Node((vector + new Vector2(0, 1)), current);
        neighbour[1] = new Node((vector + new Vector2(1, 1)), current);
        neighbour[2] = new Node((vector + new Vector2(1, 0)), current);
        neighbour[3] = new Node((vector + new Vector2(1, -1)), current);
        neighbour[4] = new Node((vector + new Vector2(0, -1)), current);
        neighbour[5] = new Node((vector + new Vector2(-1, -1)), current);
        neighbour[6] = new Node((vector + new Vector2(-1, 0)), current);
        neighbour[7] = new Node((vector + new Vector2(-1, 1)), current);

        foreach (Node V in neighbour)
        {
            Debug.DrawLine(vector, V.pos, Color.white, .4f);
        }

    }
            void Enqueue(Node item, int priority)
        {
            if (!open.ContainsKey(priority))
                open[priority] = new Queue<string>();

            open[priority].Enqueue(item);
        }

        string Dequeue()
        {
            if (open.Count == 0) throw new InvalidOperationException("Fila vazia");

            var firstKey = open.Keys.Min(); // O(1) para SortedDictionary
            var queue = open[firstKey];
            var item = queue.Dequeue();

            if (queue.Count == 0) open.Remove(firstKey); // Remove a fila vazia

            return item;
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



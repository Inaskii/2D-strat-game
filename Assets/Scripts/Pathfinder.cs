using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Pathfinder : MonoBehaviour
{
    public BinaryHeap<Node> binaryHeap;
    public List<Node> open = new List<Node>();
    public List<Node> closed = new List<Node>();
    public List<Vector2> closedPositions = new();
    public List<Vector2> openPositions = new();
    public List<Vector2> path = new List<Vector2>();
    public Node current;
    Node[] neighbour = new Node[8];
    public LayerMask layerMask;
    //float i;
    public List<Path> paths = new List<Path>();
    public Queue<Path> pathQueue;
    Path currentPath;

    void Start()
    {
        pathQueue = new Queue<Path>();
        binaryHeap = new BinaryHeap<Node>(); // Initialize BinaryHeap
    }

    void Update()
    {
    }

    public IEnumerator FindPath(Vector2 from, Vector2 to, Action<List<Vector2>> onpathFound)
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
                if (path.path != null && checkpath(path.path))
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

        open = new List<Node>();
        binaryHeap = new BinaryHeap<Node>(); // Initialize BinaryHeap
        path = new List<Vector2>();
        closed = new List<Node>();
        current = new Node(from);
        open.Add(current);
        binaryHeap.Enqueue(current, 0); // Enqueue with priority 0
        for (int k = 0; k < 200; k++)
        {
            for (int i = 0; i < 50; i++)
            {
                if (open.Count == 0)
                {
                    yield break;
                }

                current = binaryHeap.Dequeue(); // Dequeue from BinaryHeap

                if (Vector2.Distance(current.pos, to) <= 2)
                {
                    current = new Node(to, current);
                    RetracePath();
                    paths.Add(new Path(from, to, path));
                    onpathFound(path);
                    yield break;
                }



                open.Remove(current);

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
                    float currentToFromDistance = Vector2.Distance(current.pos, from);
                    float currentToNeighborDistance = Vector2.Distance(current.pos, vector.pos);
                    float neighborToFromDistance = Vector2.Distance(vector.pos, from);
                    bool isShorterPath = currentToFromDistance + currentToNeighborDistance < neighborToFromDistance;
                    bool isNotInOpenList = !open.Contains(vector);

                    if (isShorterPath || isNotInOpenList)
                    {
                        if (!open.Contains(vector))
                        {
                            binaryHeap.Enqueue(vector, vector.Calchcost(to)); // Enqueue with calculated priority
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
}








[System.Serializable]
public class Path
{
    public Vector2 from;
    public Vector2 to;
    public List<Vector2> path;

    public Path(Vector2 from, Vector2 to, List<Vector2> path)
    {
        this.from = from;
        this.to = to;
        this.path = path;
    }
}



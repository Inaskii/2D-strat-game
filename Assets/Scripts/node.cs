using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node
{
    [SerializeField]
    public Vector2 pos;
    public node parent;
    public int gcost;
    public int hcost;
    public node(Vector2 _pos, node _parent)
    {
        pos = _pos;
        parent = _parent;
        gcost = parent.gcost + 1;
    }
    public node(Vector2 _pos)
    {
        pos = _pos;
    }
    public bool nodeinlist(List<node> nodes)
    {
        foreach(node node in nodes)
        {
            if(node.pos == pos && node.parent == parent)
            {
                return true;
            }
        }
        return false;
    }
    public int Calchcost(Vector2 b)
    {
        int dx = Mathf.Abs(Mathf.RoundToInt(pos.x) - Mathf.RoundToInt(b.x));
        int dy = Mathf.Abs(Mathf.RoundToInt(pos.y) - Mathf.RoundToInt(b.y));
        int cost = dx + dy;
        return cost;
    }

}

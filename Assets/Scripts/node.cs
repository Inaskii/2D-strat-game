using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    [SerializeField]
    public Vector2 pos;
    public Node parent;
    public int gcost;
    public int hcost;
    public Node(Vector2 _pos, Node _parent)
    {
        pos = _pos;
        parent = _parent;
        gcost = parent.gcost + 1;
    }
    public Node(Vector2 _pos)
    {
        pos = _pos;
    }
    public bool nodeinlist(List<Node> nodes)
    {
        foreach(Node node in nodes)
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

        public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Node other = (Node)obj;
        return pos == other.pos;
    }

    // Sobrescrevendo GetHashCode
    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 31 + pos.GetHashCode();
        hash = hash * 31 + (parent != null ? parent.GetHashCode() : 0);
        return hash;
    }
    /*
    public static bool operator == (Node a, Node b)
    {
        if((a.pos == b.pos) && (a.parent.pos == b.parent.pos))
        {
            return true;
        }
        return false;
    }
    public static bool operator !=(Node a, Node b)
    {
        if ((a.pos == b.pos) && (a.parent.pos == b.parent.pos))
        {
            return false;
        }
        return true;
    }
    */

}


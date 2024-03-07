using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node
{
    [SerializeField]
    public Vector2 pos;
    public node parent;
    public node(Vector2 _pos, node _parent)
    {
        pos = _pos;
        parent = _parent;
    }
    public node(Vector2 _pos)
    {
        pos = _pos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool w,a,s,d = false;
    public float x,y = 0;
    float z = -9;
    public float angle;
    public GameObject Player,mark;
    public float speed;
    void Start()
    {
        
    }

    void Update()
    {


        a = Input.GetKey("a");
        d = Input.GetKey("d");
        w = Input.GetKey("w");
        s = Input.GetKey("s");
        x = 0;
        y = 0;
        if (a == true)
        {
            x--;
        }
        if (d == true)
        {
            x++;
        }
        if (w == true)
        {
            y++;

        }
        if (s == true)
        {
            y--;

        }
        Vector3 NextPos = new Vector3(x, y, z);
        Camera.main.gameObject.transform.position = Vector2.Lerp(transform.position, Camera.main.gameObject.transform.position, .1f);
        Vector3 direction = NextPos.normalized;
        if (w || a || s || d)
        {
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 270;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), .1f);
            transform.position = transform.position + transform.up * speed;

        }
    }
   

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public bool a = false;
    public float x = 0;
    public float y = 0;
    float z = -10;
    public bool w = false;
    public bool s = false;
    public bool d = false;
    public float speed;
    float dt;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;

        a = Input.GetKey("a");
        d = Input.GetKey("d");
        w = Input.GetKey("w");
        s = Input.GetKey("s");

        if (a == true)
        {
            x-=speed*Camera.main.orthographicSize*dt;
        }
        if (d == true)
        {
            x+=speed*Camera.main.orthographicSize*dt;
        }
        if (w == true)
        {
            y+=speed*Camera.main.orthographicSize*dt;

        }
        if (s == true)
        {
            y-=speed*Camera.main.orthographicSize*dt;

        }
        transform.position = new Vector3 (x/10,y/10,z);

        if (Input.mouseScrollDelta != new Vector2(0,0))
        {
            if (Camera.main.orthographicSize - Input.mouseScrollDelta.y >= 2 && Camera.main.orthographicSize - Input.mouseScrollDelta.y <= 20)

            Camera.main.orthographicSize = Camera.main.orthographicSize - Input.mouseScrollDelta.y*.5f;
        }

    }
}

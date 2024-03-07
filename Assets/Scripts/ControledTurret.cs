using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControledTurret : MonoBehaviour
{

    Health targethealth;
    public float timeBetweenShots;
    public GameObject hitPrefab;
    private float nextTimeToShoot;
    public float range;
    public int damage;
    public int Adamage;
    public bool areaDamage;
    public float radius;
    public float roationSpeed;
    public float angle;
    public string focusOn;
    public float distance;
    public GameObject mark;
    public Vector2 mousePos;
    public Vector3 mouse3Pos;
    GameObject target;
    private void Start()
    {
        nextTimeToShoot = Time.time;
    }

    private void FixedUpdate()
    {
        Aim();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse3Pos = new Vector3(mousePos.x,mousePos.y,0);
        Vector2 mouseDir = mouse3Pos - transform.position;

        if (Input.GetMouseButton(0))
        {
            Debug.DrawRay(transform.position, mouseDir);
            if (Physics2D.Raycast(transform.position, mouseDir, 10).collider.gameObject)
            {
                target = Physics2D.Raycast(transform.position, mouseDir, 10).collider.gameObject;
                Shoot();
            }
        }

    }


    private void Aim()
    {
        Vector2 direction = mouse3Pos - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), roationSpeed);
    }
    private void Shoot()
    {
       

        Debug.DrawRay(transform.position, target.transform.position - transform.position, Color.red, .3f);
        Instantiate(hitPrefab, target.transform.position - new Vector3(Random.Range(-.3f, .3f), Random.Range(-.3f, .3f), 2), target.transform.rotation);
        targethealth = target.GetComponent<Health>();
        targethealth.health =- damage;
        if (areaDamage == true)
        {
            foreach (Collider2D collider in Physics2D.OverlapCircleAll(target.transform.position, radius))
            {
                if (collider.gameObject.tag != gameObject.tag)
                {
                    targethealth = collider.GetComponent<Health>();
                    targethealth.health = targethealth.health - Adamage;
                    Instantiate(hitPrefab, collider.transform.position - new Vector3(Random.Range(-.3f, .3f), Random.Range(-.3f, .3f), 2), collider.transform.rotation);

                }
            }
        }

        nextTimeToShoot = Time.time + timeBetweenShots;
    }


}

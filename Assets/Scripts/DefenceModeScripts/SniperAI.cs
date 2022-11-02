using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SniperAI : MonoBehaviour
{
    public float checkRadius, lookSpeed, fireRate, speed;
    public GameObject sniperBullet, ejectionPoint;
    private float timeTillNextAttack;
    private Rigidbody2D rb;
    public LayerMask checkLayers;
    public Transform sniperPivot, gunRotation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Shoot();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }

    private void Shoot()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, checkRadius, checkLayers);
        Array.Sort(colliders, new DistanceComparer(transform));

        foreach( Collider2D item in colliders)
        {
            Debug.Log(item.name);
        }

        if (colliders.Length == 0)
        {
            Move();
            sniperPivot.localEulerAngles = new Vector3(0,0,-180);
            Debug.Log("No Colliders");
            return;
        }

        Vector2 direction = sniperPivot.position - colliders[0].transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        sniperPivot.rotation = Quaternion.Slerp(sniperPivot.rotation, rotation, lookSpeed * Time.deltaTime);
        Debug.Log(sniperPivot.eulerAngles);

        if(Time.time -  timeTillNextAttack >= 1/fireRate)
        {
            Instantiate(sniperBullet, ejectionPoint.transform.position, sniperPivot.rotation);
            timeTillNextAttack = Time.time;
        }
    }

    private void Move()
    {
        Vector2 direction = Vector2.right;
        rb.transform.position = new Vector2(rb.transform.position.x + direction.x * Time.deltaTime * speed, rb.transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}

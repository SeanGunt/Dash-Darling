using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlockerAI : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 0.5f;
    public float health = 100, checkRadius;
    public Transform checkRadiusObj;
    public LayerMask checkLayers;
    [SerializeField] public RectTransform healthBar;
    private Animator animator;
    private void Awake()
    {
        health = 100;
        healthBar.sizeDelta = new Vector2(100,20);
        speed = 0.5f;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(checkRadiusObj.position, checkRadius, checkLayers);
        Array.Sort(colliders, new DistanceComparer(transform));

        Vector2 direction = Vector2.right;
        rb.transform.position = new Vector2(rb.transform.position.x + direction.x * Time.deltaTime * speed, rb.transform.position.y);

        if (colliders.Length == 0)
        {
            animator.SetBool("NearEnemy", false);
            return;
        }

        float distanceToEnemy = Vector2.Distance(this.transform.position, colliders[0].transform.position);
        if (distanceToEnemy < checkRadius)
        {
            speed = 0;
            animator.SetBool("NearEnemy", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            health -= 33.4f;
            healthBar.sizeDelta = healthBar.sizeDelta -  new Vector2(33.4f,0);

            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        speed = 0.5f;
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkRadiusObj.position, checkRadius);
    }
}

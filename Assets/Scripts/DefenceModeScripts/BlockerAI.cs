using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlockerAI : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float health = 100, checkRadius;
    public Transform checkRadiusObj;
    public LayerMask checkLayers;
    [SerializeField] public RectTransform healthBar;
    private Animator animator;
    private AudioSource audioSource;
    [SerializeField] private AudioClip shieldSlamSound;
    private float usedSpeed;
    private void Awake()
    {
        health = 100;
        healthBar.sizeDelta = new Vector2(100,20);
        usedSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
        rb.transform.position = new Vector2(rb.transform.position.x + direction.x * Time.deltaTime * usedSpeed, rb.transform.position.y);

        if (colliders.Length == 0)
        {
            usedSpeed = speed;
            animator.SetBool("NearEnemy", false);
            return;
        }

        float distanceToEnemy = Vector2.Distance(this.transform.position, colliders[0].transform.position);
        if (distanceToEnemy < checkRadius)
        {
            usedSpeed = 0;
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

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkRadiusObj.position, checkRadius);
    }

    public void PlayBlockSound()
    {
        audioSource.PlayOneShot(shieldSlamSound);
    }
}

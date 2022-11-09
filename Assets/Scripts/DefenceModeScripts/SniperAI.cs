using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SniperAI : MonoBehaviour
{
    public float checkRadius, lookSpeed, fireRate, speed;
    public GameObject sniperBullet, ejectionPoint, boxObj;
    private float timeTillNextAttack;
    private Rigidbody2D rb;
    public LayerMask checkLayers;
    public Transform sniperPivot;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool canAttack;
    private AudioSource audioSource;
    [SerializeField] private AudioClip sniperShotSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxObj.SetActive(false);
        animator = GetComponent<Animator>();
        animator.SetBool("isAttacking", false);
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

        if (colliders.Length == 0)
        {
            animator.SetBool("isAttacking", false);
            boxObj.SetActive(false);
            spriteRenderer.sortingOrder = 2;
            canAttack = false;
            Move();
            sniperPivot.localEulerAngles = new Vector3(0,0,-180);
            return;
        }

        animator.SetBool("isAttacking", true);
        Vector2 direction = sniperPivot.position - colliders[0].transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        sniperPivot.rotation = Quaternion.Slerp(sniperPivot.rotation, rotation, lookSpeed * Time.deltaTime);

        if(Time.time -  timeTillNextAttack >= 1/fireRate && canAttack)
        {
            audioSource.PlayOneShot(sniperShotSound);
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

    public void EnableBox()
    {
        boxObj.SetActive(true);
        spriteRenderer.sortingOrder = -8;
        StartCoroutine("animAttackDelay");
    }

    private IEnumerator animAttackDelay()
    {
        yield return new WaitForSeconds(0.5f);
        canAttack = true;
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerAI : MonoBehaviour
{
    private GameObject[] shieldAllies;
    private float healSpeed = 0f;
    public float speed;
    private bool isMoving;
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;
    [SerializeField] private AudioClip healSound;

    private void Awake()
    {
        isMoving = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isMoving)
        {
            Move();
        }
        else
        {
            Heal();
        }
    }

    private void Move()
    {
        Vector2 direction = Vector2.right;
        rb.transform.position = new Vector2(rb.transform.position.x + direction.x * Time.deltaTime * speed, rb.transform.position.y);

        healSpeed -= Time.deltaTime;
        if (healSpeed <= 0f)
        {
            isMoving = false;
        }
    }
    public void Heal()
    {
        animator.SetBool("isHealing", true);
        shieldAllies = GameObject.FindGameObjectsWithTag("Shield");
        healSpeed = 6f;

        foreach(GameObject shieldAlly in shieldAllies)
        {
            if (shieldAlly.activeInHierarchy == false)
            {
                return;
            }
            shieldAlly.GetComponent<BlockerAI>().health = 100f;
            shieldAlly.GetComponent<BlockerAI>().healthBar.sizeDelta = new Vector2(100,20);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }

    public void StartHealAnimation()
    {
        audioSource.PlayOneShot(healSound);
    }
    public void EndHealAnimation()
    {
        isMoving = true;
        animator.SetBool("isHealing", false);
    }
}

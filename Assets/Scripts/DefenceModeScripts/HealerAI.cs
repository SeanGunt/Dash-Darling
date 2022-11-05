using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerAI : MonoBehaviour
{
    private GameObject[] shieldAllies;
    private float healSpeed = 8f, speed = 0.5f;
    private bool isMoving;
    private Rigidbody2D rb;
    private Coroutine coroutine = null;
    private Animator animator;

    private void Awake()
    {
        isMoving = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isMoving)
        {
            Move();
            animator.SetBool("isHealing", false);
        }
        Heal();
    }

    private void Move()
    {
        Vector2 direction = Vector2.right;
        rb.transform.position = new Vector2(rb.transform.position.x + direction.x * Time.deltaTime * speed, rb.transform.position.y);
    }
    public void Heal()
    {
        healSpeed -= Time.deltaTime;
        if (healSpeed <= 0f)
        {
            isMoving = false;
            StartCoroutine("PlayAnimation");
            shieldAllies = GameObject.FindGameObjectsWithTag("Shield");

            foreach(GameObject shieldAlly in shieldAllies)
            {
                if (shieldAlly.activeInHierarchy == false)
                {
                    return;
                }
                shieldAlly.GetComponent<BlockerAI>().health = 100f;
                shieldAlly.GetComponent<BlockerAI>().healthBar.sizeDelta = new Vector2(100,20);
                healSpeed = 8f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator PlayAnimation()
    {
        animator.SetBool("isHealing", true);
        yield return new WaitForSeconds(1f);
        isMoving = true;
    }
}

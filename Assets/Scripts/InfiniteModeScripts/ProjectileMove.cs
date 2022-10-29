using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float speed;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(this.transform.right * speed, ForceMode2D.Impulse);
    }

    void Update()
    {
        DestroyAfterTime();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.gameObject);
    }

    void DestroyAfterTime()
    {
        Destroy(this.gameObject, 1f);
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}

using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 direction = Vector2.left;
        rb.transform.position = new Vector2(rb.transform.position.x + direction.x * Time.deltaTime * speed, rb.transform.position.y);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}

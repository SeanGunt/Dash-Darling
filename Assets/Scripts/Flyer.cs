using UnityEngine;
using UnityEngine.UI;

public class Flyer : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int health;
    [SerializeField] RectTransform healthBar;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
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
        if (other.gameObject.tag == "Pistol")
        {
            health = health - GunDamage.Instance.PistolDamage;
            healthBar.sizeDelta = healthBar.sizeDelta -  new Vector2(GunDamage.Instance.PistolDamage,0);
            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
            Debug.Log(health);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class Flyer : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int health;
    [SerializeField] RectTransform healthBar;
    private GameObject player;
    private Rigidbody2D rb;
    private State state;

    enum State
    {
        moving, diving
    }
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        state = State.moving;
        rb = this.GetComponent<Rigidbody2D>();
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
    }

    void Update()
    {
        switch (state)
        {
            default:
            case State.moving:
                Move();
            break;

            case State.diving:
                Diving();
            break;
        }
    }

    private void Move()
    {
        Vector2 direction = Vector2.left;
        rb.transform.position = new Vector2(rb.transform.position.x + direction.x * Time.deltaTime * speed, rb.transform.position.y);
        
        float distanceToPlayer = Vector2.Distance(player.transform.position, this.transform.position);
        if (distanceToPlayer < 7)
        {
            state = State.diving;
        }
    }

    private void Diving()
    {
        rb.transform.position = Vector2.Lerp(this.transform.position, player.transform.position, Time.deltaTime * 3);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Destroy(this.gameObject);
            PlayerController player = other.gameObject.GetComponent<PlayerController>();

            if(player != null)
            {
                player.PlayerDeath();
            }
        }
        if (other.gameObject.tag == "Pistol")
        {
            health = health - GameDataHolder.pistolDamage;
            healthBar.sizeDelta = healthBar.sizeDelta -  new Vector2(GameDataHolder.pistolDamage,0);
            if (health <= 0)
            {
                GameDataHolder.money += 100;
                MoneyHolderUI.instance.moneyUI.text = GameDataHolder.money.ToString();
                Destroy(this.gameObject);
            }
        }
    }
}

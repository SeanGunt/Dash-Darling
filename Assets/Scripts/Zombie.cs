using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int health;
    [SerializeField] RectTransform healthBar;
    private Rigidbody2D rb;
    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip spawnSound, deathSound;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
        PlaySound(spawnSound);
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
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
                GameDataHolder.money += 250;
                MoneyHolderUI.instance.moneyUI.text = GameDataHolder.money.ToString();
                Destroy(this.gameObject);
                PlaySound(deathSound);
            }
        }
    }
}

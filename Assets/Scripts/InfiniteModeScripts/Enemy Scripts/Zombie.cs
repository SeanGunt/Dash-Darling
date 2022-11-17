using UnityEngine;
using UnityEngine.SceneManagement;

public class Zombie : MonoBehaviour
{
    [SerializeField] public static float speed = 1.5f;
    [SerializeField] int health;
    [SerializeField] RectTransform healthBar;
    private Rigidbody2D rb;
    [SerializeField] AudioSource audioSource;
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] AudioClip spawnSound;
    private State state;
    enum State
    {
        moving, blocked
    }

    void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            speed = 1.5f;
        }
        state = State.moving;
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
        switch (state)
        {
            default:
            case State.moving:
                Move();
            break;

            case State.blocked:
                Blocked();
            break;
        }
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

            if(player != null && !player.invincible)
            {
                player.PlayerDeath();
            }
            else
            {
                Death();
            }
        }
        if (other.gameObject.tag == "Pistol")
        {
            health = health - GameDataHolder.pistolDamage;
            healthBar.sizeDelta = healthBar.sizeDelta -  new Vector2(GameDataHolder.pistolDamage,0);
            
            if (health <= 0)
            {
                Death();
            }
        }

        if (other.gameObject.tag == "Turret")
        {
            health = health - GameDataHolder.turretDamage;
            healthBar.sizeDelta = healthBar.sizeDelta - new Vector2(GameDataHolder.turretDamage,0);
            if (health <= 0)
            {
                deathParticles.transform.position = this.transform.position;
                Instantiate(deathParticles, this.transform.position, Quaternion.identity);
                ChocoCoinsManager.coins += 2;
                Destroy(this.gameObject);
            }
        }
        if (other.gameObject.tag == "Shield")
        {
            state = State.blocked;
        }
        
        if (other.gameObject.tag  == "Sniper")
        {
            health = health - 50;
            healthBar.sizeDelta = healthBar.sizeDelta -  new Vector2(50,0);
            if (health <= 0)
            {
                deathParticles.transform.position = this.transform.position;
                Instantiate(deathParticles, this.transform.position, Quaternion.identity);
                ChocoCoinsManager.coins += 2;
                Destroy(this.gameObject);
            
            }
        }
        if (other.gameObject.tag == "Platform")
        {
            deathParticles.transform.position = this.transform.position;
            Instantiate(deathParticles, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Shield")
        {
            state = State.moving;
        }
    }
    private void Blocked()
    {
        GameObject[] blockers = GameObject.FindGameObjectsWithTag("Shield");
        foreach (GameObject blocker in blockers)
        {
            if (!blocker.activeInHierarchy)
            {
                state = State.moving;
            }
        }

    }

    private void Death()
    {
        GameDataHolder.money += 250;
        MoneyHolderUI.instance.moneyUI.text = GameDataHolder.money.ToString();
        deathParticles.transform.position = this.transform.position;
        Instantiate(deathParticles, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class HatZombie : MonoBehaviour
{
    [SerializeField] public static float speed = 2.0f;
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
            speed = 2.0f;
        }
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
            health = health - GameDataHolder.pistolDamage/2;
            healthBar.sizeDelta = healthBar.sizeDelta -  new Vector2(GameDataHolder.pistolDamage/2,0);
            
            if (health <= 0)
            {
                Death();
            }
        }

        if (other.gameObject.tag == "Turret")
        {
            health = health - GameDataHolder.turretDamage/2;
            healthBar.sizeDelta = healthBar.sizeDelta - new Vector2(GameDataHolder.turretDamage/2,0);
            if (health <= 0)
            {
                deathParticles.transform.position = this.transform.position;
                Instantiate(deathParticles, this.transform.position, Quaternion.identity);
                ChocoCoinsManager.coins += 5;
                Destroy(this.gameObject);
            }
        }
        if (other.gameObject.tag == "Shield")
        {
            state = State.blocked;
        }

        if (other.gameObject.tag  == "Sniper")
        {
            health = health - 50/2;
            healthBar.sizeDelta = healthBar.sizeDelta -  new Vector2(50/2,0);
            if (health <= 0)
            {
                deathParticles.transform.position = this.transform.position;
                Instantiate(deathParticles, this.transform.position, Quaternion.identity);
                ChocoCoinsManager.coins += 5;
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
    private void Blocked()
    {
        GameObject[] blockers = GameObject.FindGameObjectsWithTag("Shield");
        foreach (GameObject blocker in blockers)
        {
            if (blocker.activeInHierarchy == false)
            {
                state = State.moving;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Shield")
        {
            state = State.moving;
        }
    }

    private void Death()
    {
        GameDataHolder.money += 1000;
        MoneyHolderUI.instance.moneyUI.text = GameDataHolder.money.ToString();
        deathParticles.transform.position = this.transform.position;
        Instantiate(deathParticles, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}

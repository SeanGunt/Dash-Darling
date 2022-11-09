using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellBat : MonoBehaviour
{
    [SerializeField] public static float speed = 4.0f;
    [SerializeField] int health;
    [SerializeField] RectTransform healthBar;
    private GameObject player, platform;
    private Rigidbody2D rb;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip spawnSound;
    [SerializeField] private ParticleSystem deathParticles;
    private State state;

    enum State
    {
        moving, diving
    }
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        platform = GameObject.FindWithTag("Platform");
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

            case State.diving:
                Diving();
            break;
        }
    }

    private void Move()
    {
        Vector2 direction = Vector2.left;
        rb.transform.position = new Vector2(rb.transform.position.x + direction.x * Time.deltaTime * speed, rb.transform.position.y);
        
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(player.transform.position, this.transform.position);
            if (distanceToPlayer <7)
            {
                state = State.diving;
            }
        }
        else
        {
            float distanceToPlatform = Vector2.Distance(platform.transform.position, this.transform.position);
            if (distanceToPlatform < 7)
            {
            state = State.diving;
            }
        }
        
    }

    private void Diving()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(player.transform.position, this.transform.position);
            rb.transform.position = Vector2.Lerp(this.transform.position, player.transform.position, Time.deltaTime * 3f);

            if (distanceToPlayer > 7)
            {
                state = State.moving;
            }
        }
        else
        {
            float distanceToPlatform = Vector2.Distance(platform.transform.position, this.transform.position);
            rb.transform.position = Vector2.Lerp(this.transform.position, platform.transform.position, Time.deltaTime * 3);

            if (distanceToPlatform > 7)
            {
            state = State.moving;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
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
        if (other.gameObject.tag  == "Sniper")
        {
            deathParticles.transform.position = this.transform.position;
            Instantiate(deathParticles, this.transform.position, Quaternion.identity);
            ChocoCoinsManager.coins += 2;
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Platform")
        {
            deathParticles.transform.position = this.transform.position;
            Instantiate(deathParticles, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void Death()
    {
        GameDataHolder.money += 200;
        MoneyHolderUI.instance.moneyUI.text = GameDataHolder.money.ToString();
        deathParticles.transform.position = this.transform.position;
        Instantiate(deathParticles, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}

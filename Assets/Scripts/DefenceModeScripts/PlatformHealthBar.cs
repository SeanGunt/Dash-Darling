using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHealthBar : MonoBehaviour
{
    [SerializeField] RectTransform healthBar;
    [SerializeField] private GameObject gameOverMenu;
    private GameObject[] enemies;
    private float health;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip deathMusic;

    private void Awake()
    {
        health = 100;
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            health -= 25;
            healthBar.sizeDelta = healthBar.sizeDelta - new Vector2(25,0);

            if (health <= 0)
            {
                Death();
            }
        }
    }
    private void Update()
    {
        Debug.Log(health);
    }

    private void Death()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);
        audioSource.clip = deathMusic;
        audioSource.Play();
    }
}

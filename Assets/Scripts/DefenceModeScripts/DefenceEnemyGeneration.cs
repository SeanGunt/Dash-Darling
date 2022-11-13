using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceEnemyGeneration : MonoBehaviour
{
    [SerializeField] GameObject bat, hellBat, zombie, helmetZombie;
    [SerializeField] private float spawnBatTimer, spawnHellBatTimer, spawnZombieTimer, spawnHelmetZombieTimer;
    [SerializeField] private float minWaitZombie, minWaitFlyer, minWaitHellBat, maxWaitZombie, maxWaitFlyer, maxWaitHellBat;
    private float randomSpawnHeight;
    private float difficulty = 0.10f;

    private void Update()
    {
        spawnZombieTimer -= Time.deltaTime;
        if (spawnZombieTimer <= 0)
        {
            SpawnZombie();
        }

        spawnBatTimer -= Time.deltaTime;
        if (spawnBatTimer <= 0)
        {
            SpawnBat();
        }

        spawnHellBatTimer -= Time.deltaTime;
        if (spawnHellBatTimer <= 0)
        {
            SpawnHellBat();
        }

        spawnHelmetZombieTimer -= Time.deltaTime;
        if (spawnHelmetZombieTimer <= 0)
        {
            SpawnHelmetZombie();
        }

        DifficultyManagement();
    }

    private void SpawnBat()
    {
        randomSpawnHeight = Random.Range(0.0f, 2.5f);
        Instantiate(bat, new Vector2(15f,randomSpawnHeight), Quaternion.identity);

        spawnBatTimer = Random.Range(minWaitFlyer/difficulty,maxWaitFlyer/difficulty);
    }

    private void SpawnZombie()
    {
        Instantiate(zombie, new Vector2(15f,-2.9f), Quaternion.identity);

        spawnZombieTimer = Random.Range(minWaitZombie/difficulty,maxWaitZombie/difficulty);
    }

    private void SpawnHellBat()
    {
        randomSpawnHeight = Random.Range(0.0f, 2.5f);
        Instantiate(hellBat, new Vector2(15f,randomSpawnHeight), Quaternion.identity);

        spawnHellBatTimer = Random.Range(minWaitHellBat/difficulty,maxWaitHellBat/difficulty);
    }

    private void SpawnHelmetZombie()
    {
        Instantiate(helmetZombie, new Vector2(15f,-2.9f), Quaternion.identity);

        spawnHelmetZombieTimer = 25f;
    }

    private void DifficultyManagement()
    {
        if (difficulty >= 0 && difficulty <= 1.0f)
        {
            difficulty += 0.025f * Time.deltaTime;
        }
    }
}

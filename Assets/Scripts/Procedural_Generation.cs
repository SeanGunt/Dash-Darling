using UnityEngine;

public class Procedural_Generation : MonoBehaviour
{
    [SerializeField] float width, height;
    [SerializeField] GameObject bat, hellBat, zombie, player;
    [SerializeField] float minWaitZombie, maxWaitZombie, minWaitFlyer, maxWaitFlyer, minWaitHellBat, maxWaitHellBat;
    [SerializeField] GameObject[] platforms;
    [SerializeField] GameObject[] backgrounds;
    [SerializeField] private GameObject targetIndicator;
    [SerializeField] Transform generationPoint, destructionPoint;
    private float zombieTimer = 5f, backgroundTimer, flyerTimer = 10f, hellBatTimer = 70f, difficulty, secondDifficulty;
    private int randomOptionBG, randomOptionPL;
    private bool maxDifficultyReached;

    void Awake()
    {
        difficulty = 0;
        secondDifficulty = 0;
    }
    void Update()
    {
        zombieTimer -= Time.deltaTime;
        if (zombieTimer <= 0)
        {
            SpawnZombie();
        }

        flyerTimer -= Time.deltaTime;
        if (flyerTimer <= 0)
        {
            SpawnBat();
        }

        hellBatTimer -= Time.deltaTime;
        if (hellBatTimer <= 0)
        {
            SpawnHellBat();
        }

        DifficultyManagement();
        SpawnEnvironment();
    }
    void SpawnEnvironment()
    {
        if(transform.position.x < generationPoint.position.x)
        {
            randomOptionPL = Random.Range(0, platforms.Length);
            transform.position = new Vector3(transform.position.x + width, transform.position.y, transform.position.z);
            Instantiate(platforms[randomOptionPL], new Vector3(transform.position.x, 5, transform.position.z), Quaternion.identity);

            randomOptionBG = Random.Range(0, backgrounds.Length);
            Instantiate(backgrounds[randomOptionBG], new Vector3(transform.position.x, height, transform.position.z), Quaternion.identity);
        }
    }

    void SpawnZombie()
    {
        GameObject baseZombie = Instantiate(zombie, new Vector2(player.transform.position.x + 25, 3.03f), Quaternion.identity);
        Vector2 targetPosition = new Vector2(baseZombie.transform.position.x - 8.5f, baseZombie.transform.position.y);
        GameObject target = Instantiate(targetIndicator, targetPosition, Quaternion.identity);
        Destroy(target, 1.5f);

        zombieTimer = Random.Range(minWaitZombie/difficulty,maxWaitZombie/difficulty);
    }

    void SpawnBat()
    {
        GameObject baseBat = Instantiate(bat, new Vector2(player.transform.position.x + 25, 7.5f), Quaternion.identity);
        Vector2 targetPosition = new Vector2(baseBat.transform.position.x - 8.5f, baseBat.transform.position.y);
        GameObject target = Instantiate(targetIndicator, targetPosition, Quaternion.identity);
        Destroy(target, 1.5f);

        flyerTimer = Random.Range(minWaitFlyer/difficulty, maxWaitFlyer/difficulty);
    }

    void SpawnHellBat()
    {
        GameObject baseHellBat = Instantiate(hellBat, new Vector2(player.transform.position.x + 25, 7.5f), Quaternion.identity);
        Vector2 targetPosition = new Vector2(baseHellBat.transform.position.x - 8.5f, baseHellBat.transform.position.y);
        GameObject target = Instantiate(targetIndicator, targetPosition, Quaternion.identity);
        Destroy(target, 1.5f);

        hellBatTimer = Random.Range(minWaitHellBat/secondDifficulty, maxWaitHellBat/secondDifficulty);
    }

    void DifficultyManagement()
    {
        if (difficulty >= 0 && difficulty <= 1.0f)
        {
            difficulty += 0.025f * Time.deltaTime;
        }

        if (difficulty >= 1.0f && secondDifficulty <= 1.0f)
        {
            secondDifficulty += 0.025f * Time.deltaTime;
        }
    }
}

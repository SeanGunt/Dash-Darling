using UnityEngine;

public class Procedural_Generation : MonoBehaviour
{
    [SerializeField] float width, height;
    [SerializeField] GameObject flyingEnemy, enemy, player;
    [SerializeField] float minWaitZombie, maxWaitZombie, minWaitFlyer, maxWaitFlyer;
    [SerializeField] GameObject[] platforms;
    [SerializeField] GameObject[] backgrounds;
    [SerializeField] private GameObject targetIndicator, skyLight;
    [SerializeField] Transform generationPoint, destructionPoint;
    private float zombieTimer = 5f, backgroundTimer, flyerTimer = 10f, difficulty;
    private int randomOptionBG, randomOptionPL;
    private bool maxDifficultyReached;

    void Awake()
    {
        difficulty = 0;
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
            SpawnFlyer();
        }
        if (difficulty >= 0 && difficulty <= 1.3f)
        {
            difficulty += 0.025f * Time.deltaTime;
        }
        Debug.Log(difficulty);
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
        GameObject baseZombie = Instantiate(enemy, new Vector2(player.transform.position.x + 25, 3.03f), Quaternion.identity);
        Vector2 targetPosition = new Vector2(baseZombie.transform.position.x - 8.5f, baseZombie.transform.position.y);
        GameObject target = Instantiate(targetIndicator, targetPosition, Quaternion.identity);
        Destroy(target, 1.5f);

        zombieTimer = Random.Range(minWaitZombie/difficulty,maxWaitZombie/difficulty);
    }

    void SpawnFlyer()
    {
        GameObject baseBat = Instantiate(flyingEnemy, new Vector2(player.transform.position.x + 25, 7.5f), Quaternion.identity);
        Vector2 targetPosition = new Vector2(baseBat.transform.position.x - 8.5f, baseBat.transform.position.y);
        GameObject target = Instantiate(targetIndicator, targetPosition, Quaternion.identity);
        Destroy(target, 1.5f);

        flyerTimer = Random.Range(minWaitFlyer/difficulty, maxWaitFlyer/difficulty);
    }
}

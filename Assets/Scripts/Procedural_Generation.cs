using UnityEngine;

public class Procedural_Generation : MonoBehaviour
{
    [SerializeField] float width, height;
    [SerializeField] GameObject flyingEnemy, enemy, player;
    [SerializeField] float minWaitZombie, maxWaitZombie, minWaitFlyer, maxWaitFlyer;
    [SerializeField] GameObject[] platforms;
    [SerializeField] GameObject[] backgrounds;
    [SerializeField] Transform generationPoint, destructionPoint;
    private float zombieTimer = 5f, backgroundTimer, flyerTimer = 10f, difficulty;
    private int randomOptionBG, randomOptionPL;

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
        difficulty += 0.025f * Time.deltaTime;
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
        Instantiate(enemy, new Vector2(player.transform.position.x + 25, 2.05f), Quaternion.identity);

        zombieTimer = Random.Range(minWaitZombie/difficulty,maxWaitZombie/difficulty);
    }

    void SpawnFlyer()
    {
        Instantiate(flyingEnemy, new Vector2(player.transform.position.x + 25, 7.5f), Quaternion.identity);

        flyerTimer = Random.Range(minWaitFlyer/difficulty, maxWaitFlyer/difficulty);
    }
}

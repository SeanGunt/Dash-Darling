using UnityEngine;

public class Procedural_Generation : MonoBehaviour
{
    [SerializeField] float width, height;
    [SerializeField] GameObject flyingEnemy, enemy, player;
    [SerializeField] float minWait, maxWait;
    [SerializeField] GameObject platform;
    [SerializeField] GameObject[] backgrounds;
    [SerializeField] Transform generationPoint, destructionPoint;
    private float zombieTimer = 5f, backgroundTimer, flyerTimer = 10f;
    private int randomOption;

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

        SpawnEnvironment();
    }
    void SpawnEnvironment()
    {
        if(transform.position.x < generationPoint.position.x)
        {
            transform.position = new Vector3(transform.position.x + width, transform.position.y, transform.position.z);
            Instantiate(platform, transform.position, Quaternion.identity);

            randomOption = Random.Range(0, backgrounds.Length);
            Instantiate(backgrounds[randomOption], new Vector3(transform.position.x, height, transform.position.z), Quaternion.identity);
        }
    }

    void SpawnZombie()
    {
        Instantiate(enemy, new Vector2(player.transform.position.x + 25, 0.95f), Quaternion.identity);

        zombieTimer = Random.Range(minWait,maxWait);
    }

    void SpawnFlyer()
    {
        Instantiate(flyingEnemy, new Vector2(player.transform.position.x + 25, 4f), Quaternion.identity);

        flyerTimer = Random.Range(minWait, maxWait);
    }
}

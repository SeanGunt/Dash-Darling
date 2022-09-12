using UnityEngine;

public class Procedural_Generation : MonoBehaviour
{
    [SerializeField] float width, height;
    [SerializeField] GameObject enemy, player;
    [SerializeField] float minWait, maxWait;
    [SerializeField] GameObject platform;
    [SerializeField] GameObject[] backgrounds;
    [SerializeField] Transform generationPoint, destructionPoint;
    private float enemyTimer = 5f, backgroundTimer;
    private int randomOption;

    void Update()
    {
        enemyTimer -= Time.deltaTime;
        if (enemyTimer <= 0)
        {
            SpawnEnemy();
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

    void SpawnEnemy()
    {
        Instantiate(enemy, new Vector2(player.transform.position.x + 25, 0.95f), Quaternion.identity);
        enemyTimer = Random.Range(minWait,maxWait);
    }
}

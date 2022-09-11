using UnityEngine;

public class Procedural_Generation : MonoBehaviour
{
    [SerializeField] int width, height;
    [SerializeField] GameObject enemy, player;
    [SerializeField] float minWait, maxWait;
    [SerializeField] GameObject platform;
    [SerializeField] GameObject[] backgrounds;
    [SerializeField] Transform generationPoint, destructionPoint;
    private float enemyTimer = 5f, backgroundTimer;
    private int randomOption = 1;

    void Update()
    {
        enemyTimer -= Time.deltaTime;
        if (enemyTimer <= 0)
        {
            SpawnEnemy();
        }

        SpawnEnvironment();
        Debug.Log(randomOption);
    }
    void SpawnEnvironment()
    {
        if(transform.position.x < generationPoint.position.x)
        {
            transform.position = new Vector3(transform.position.x + width, transform.position.y, transform.position.z);
            Instantiate(platform, transform.position, Quaternion.identity);

            randomOption = Random.Range(0, backgrounds.Length);
            Instantiate(backgrounds[randomOption], new Vector3(transform.position.x, transform.position.y + 8, transform.position.z), Quaternion.identity);
        }
    }

    void SpawnEnemy()
    {
        enemyTimer = Random.Range(minWait,maxWait);
        Instantiate(enemy, new Vector2(player.transform.position.x + 20, 2), Quaternion.identity);
    }
}

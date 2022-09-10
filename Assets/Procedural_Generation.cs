using UnityEngine;
using UnityEngine.Tilemaps;

public class Procedural_Generation : MonoBehaviour
{
    [SerializeField] int width, height;
    [SerializeField] GameObject enemy, player;
    [SerializeField] float minWait, maxWait;
    [SerializeField] Tilemap backgroundTileMap, platformTileMap;
    [SerializeField] Tile background, platform;
    private float timer;


    void Awake()
    {
        Generation();
        ResetEnemySpawnTimer();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnEnemy();
            ResetEnemySpawnTimer();
        }
        Debug.Log(timer);
    }

    void Generation()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                backgroundTileMap.SetTile(new Vector3Int(x,y + 1,0), background);
            }
        }

        for (int x = 0; x < width; x++)
        {
            platformTileMap.SetTile(new Vector3Int(x,0,0), platform);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemy, new Vector2(player.transform.position.x + 20, 2), Quaternion.identity);
    }

    void ResetEnemySpawnTimer()
    {
        timer = Random.Range(minWait,maxWait);
    }
}

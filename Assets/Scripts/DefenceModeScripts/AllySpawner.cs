using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllySpawner : MonoBehaviour
{
    [SerializeField] private GameObject blocker, sniper;
    [SerializeField] private Button blockerButton, sniperButton, healerButton;
    private float spawnTimer = 0;

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (ChocoCoinsManager.coins < 10 || spawnTimer >= 0)
        {
            blockerButton.interactable = false;
            blockerButton.image.color = Color.gray;
        }
        else
        {
            blockerButton.interactable = true;
            blockerButton.image.color = Color.green;
        }

        if (ChocoCoinsManager.coins < 15 || spawnTimer >= 0)
        {
            sniperButton.interactable = false;
            sniperButton.image.color = Color.gray;
        }
        else
        {
            sniperButton.interactable = true;
            sniperButton.image.color = Color.green;
        }
    }
    public void SpawnBlocker()
    {
        if (spawnTimer <= 0 && ChocoCoinsManager.coins >= 10)
        {
            Instantiate(blocker, new Vector2(-5,-3), Quaternion.identity);
            ChocoCoinsManager.coins -= 10;
            spawnTimer = 5f;
        }
    }

    public void SpawnSniper()
    {
        if (spawnTimer <= 0 && ChocoCoinsManager.coins >= 15)
        {
            Instantiate(sniper, new Vector2(-5,-3), Quaternion.identity);
            ChocoCoinsManager.coins -= 15;
            spawnTimer = 5f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllySpawner : MonoBehaviour
{
    [SerializeField] private GameObject blocker, sniper, healer;
    [SerializeField] private Button blockerButton, sniperButton, healerButton;
    private float spawnTimer = 0;
    private bool canBeSpawned, beingRefilled;

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            
            canBeSpawned = true;
        }

        if (!canBeSpawned && !beingRefilled)
        {
            blockerButton.image.fillAmount = 0;
            sniperButton.image.fillAmount = 0;
            healerButton.image.fillAmount = 0;
            beingRefilled = true;
        }

        if (beingRefilled)
        {
            blockerButton.image.fillAmount += 1f/5f * Time.deltaTime;
            sniperButton.image.fillAmount += 1f/5f * Time.deltaTime;
            healerButton.image.fillAmount += 1f/5f * Time.deltaTime;
        }

        if (ChocoCoinsManager.coins < 10 || !canBeSpawned)
        {
            blockerButton.interactable = false;
            blockerButton.image.color = Color.gray;
        }
        else
        {
            blockerButton.interactable = true;
            blockerButton.image.color = Color.green;
        }

        if (ChocoCoinsManager.coins < 15 || !canBeSpawned)
        {
            sniperButton.interactable = false;
            sniperButton.image.color = Color.gray;
        }
        else
        {
            sniperButton.interactable = true;
            sniperButton.image.color = Color.green;
        }

        if (ChocoCoinsManager.coins < 20 || !canBeSpawned)
        {
            healerButton.interactable = false;
            healerButton.image.color = Color.gray;
        }
        else
        {
            healerButton.interactable = true;
            healerButton.image.color = Color.green;
        }
    }
    public void SpawnBlocker()
    {
        if (spawnTimer <= 0 && ChocoCoinsManager.coins >= 10)
        {
            Instantiate(blocker, new Vector2(-5,-2.735f), Quaternion.identity);
            ChocoCoinsManager.coins -= 10;
            spawnTimer = 5f;
            canBeSpawned = false;
            beingRefilled = false;
        }
    }

    public void SpawnSniper()
    {
        if (spawnTimer <= 0 && ChocoCoinsManager.coins >= 15)
        {
            Instantiate(sniper, new Vector2(-5,-3), Quaternion.identity);
            ChocoCoinsManager.coins -= 15;
            spawnTimer = 5f;
            canBeSpawned = false;
            beingRefilled = false;
        }
    }

    public void SpawnHealer()
    {
        if (spawnTimer <= 0 && ChocoCoinsManager.coins >= 20)
        {
            Instantiate(healer, new Vector2(-7.3f,-3), Quaternion.identity);
            ChocoCoinsManager.coins -= 20;
            spawnTimer = 5f;
            canBeSpawned = false;
            beingRefilled = false;
        }
    }
}

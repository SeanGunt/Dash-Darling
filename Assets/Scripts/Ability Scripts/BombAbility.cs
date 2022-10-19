using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BombAbility : MonoBehaviour
{
    private float activeTimer = 3f;
    private float cooldownTimer = 15f;
    private State state;
    [SerializeField] private Button abilityButton;
    private bool clicked;
    [SerializeField] private TextMeshProUGUI abilityText;
    private GameObject[] enemies;
    enum State
    {
        OnCooldown, ReadyToActivate, InProgress, NotPurchased
    }

    private void Awake()
    {
        clicked = false;
        if ( GameDataHolder.bombAbilityPurchased == false)
        {
            abilityButton.enabled = false;
            abilityButton.GetComponent<Image>().color = Color.gray;
            state = State.NotPurchased;
        }
        else
        {
            abilityButton.enabled = true;
            abilityButton.GetComponent<Image>().color = new Color(0.0f,0.8f,0.0f,0.6f);
            state = State.ReadyToActivate;
        }
    }
    public void Click()
    {
        clicked = true;
    }

    private void Activate()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        GameObject.Destroy(enemy);
    }

    private void Update()
    {
        switch (state)
        {
            case State.NotPurchased:

            break;
            case State.ReadyToActivate:
                abilityText.text = "Bomb Ready";
                cooldownTimer = 20f;
                abilityButton.enabled = true;
                abilityButton.GetComponent<Image>().color = new Color(0.0f,0.8f,0.0f,0.6f);
                if (clicked)
                {
                    Activate();
                    state = State.InProgress;
                }
            break;

            case State.InProgress:
                activeTimer -= Time.deltaTime;
                abilityText.text = "In Progress " + activeTimer.ToString("n0");
                abilityButton.enabled = false;
                abilityButton.GetComponent<Image>().color = Color.gray;
                if (activeTimer < 0)
                {
                    state = State.OnCooldown;
                }
            break;

            case State.OnCooldown:
                cooldownTimer -= Time.deltaTime;
                abilityText.text = "On Cooldown " + cooldownTimer.ToString("n0");
                abilityButton.enabled = false;
                abilityButton.GetComponent<Image>().color = Color.gray;
                if (cooldownTimer < 0)
                {
                    activeTimer = 3f;
                    clicked = false;
                    state = State.ReadyToActivate;
                }
            break;
        }
    }
}

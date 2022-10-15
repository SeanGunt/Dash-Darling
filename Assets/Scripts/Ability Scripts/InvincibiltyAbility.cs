using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InvincibiltyAbility : MonoBehaviour
{
    private float activeTimer = 5f;
    private float cooldownTimer = 10f;
    private State state;
    [SerializeField] private Button abilityButton;
    private bool clicked;
    [SerializeField] private TextMeshProUGUI abilityText;
    private GameObject pc;
    enum State
    {
        OnCooldown, ReadyToActivate, InProgress
    }

    private void Awake()
    {
        pc = GameObject.FindWithTag("Player");
        state = State.ReadyToActivate;
        clicked = false;
        if ( GameDataHolder.invincibilityAbilityPurchased == false)
        {
            abilityButton.enabled = false;
            abilityButton.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            abilityButton.enabled = true;
            abilityButton.GetComponent<Image>().color = new Color(0.0f,0.8f,0.0f,0.6f);
        }
    }
    public void Click()
    {
        clicked = true;
    }

    private void Activate()
    {
        pc.GetComponent<PlayerController>().movementSpeed = 7.5f;
        pc.GetComponent<PlayerController>().invincible = true;
    }

    private void Update()
    {
        switch (state)
        {
            case State.ReadyToActivate:
                abilityText.text = "Invincibility Ready";
                abilityButton.enabled = true;
                abilityButton.GetComponent<Image>().color = new Color(0.0f,0.8f,0.0f,0.6f);
                cooldownTimer = 10f;
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
                    pc.GetComponent<PlayerController>().movementSpeed = 5f;
                    pc.GetComponent<PlayerController>().invincible = false;
                    state = State.OnCooldown;
                }
            break;

            case State.OnCooldown:
                cooldownTimer -= Time.deltaTime;
                abilityButton.enabled = false;
                abilityText.text = "On Cooldown " + cooldownTimer.ToString("n0");
                abilityButton.GetComponent<Image>().color = Color.gray;
                if (cooldownTimer < 0)
                {
                    activeTimer = 5f;
                    clicked = false;
                    state = State.ReadyToActivate;
                }
            break;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InvincibiltyAbility : MonoBehaviour
{
    private float activeTimer = 5f, startingActiveTimer;
    private float cooldownTimer = 10f, startingCooldownTimer;
    private State state;
    [SerializeField] private Button abilityButton;
    private bool clicked;
    private GameObject pc;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    private bool hasBeenPlayed;
    enum State
    {
        OnCooldown, ReadyToActivate, InProgress, NotPurchased
    }

    private void Awake()
    {
        startingActiveTimer = activeTimer;
        startingCooldownTimer = cooldownTimer;
        pc = GameObject.FindWithTag("Player");
        clicked = false;
        if ( GameDataHolder.invincibilityAbilityPurchased == false)
        {
            abilityButton.enabled = false;
            abilityButton.GetComponent<Image>().color = Color.gray;
            state = State.NotPurchased;
        }
        else
        {
            abilityButton.enabled = true;
            state = State.ReadyToActivate;
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
        if (!hasBeenPlayed)
        {
            audioSource.PlayOneShot(audioClip);
            hasBeenPlayed = true;
        }
    }

    private void Update()
    {
        switch (state)
        {
            case State.NotPurchased:

            break;
            case State.ReadyToActivate:
                abilityButton.enabled = true;
                abilityButton.GetComponent<Image>().color = Color.green;
                cooldownTimer = startingCooldownTimer;
                if (clicked)
                {
                    Activate();
                    state = State.InProgress;
                    abilityButton.GetComponent<Image>().color = Color.blue;
                }
            break;

            case State.InProgress:
                activeTimer -= Time.deltaTime;
                abilityButton.enabled = false;
                abilityButton.GetComponent<Image>().fillAmount -= 1f/startingActiveTimer * Time.deltaTime;
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
                abilityButton.GetComponent<Image>().color = Color.red;
                abilityButton.GetComponent<Image>().fillAmount += 1f/startingCooldownTimer * Time.deltaTime;
                if (cooldownTimer < 0)
                {
                    activeTimer = startingActiveTimer;
                    clicked = false;
                    hasBeenPlayed = false;
                    state = State.ReadyToActivate;
                }
            break;
        }
    }
}

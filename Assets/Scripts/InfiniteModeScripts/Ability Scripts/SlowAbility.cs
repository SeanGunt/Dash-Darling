using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlowAbility : MonoBehaviour
{
    private float slowAmount = 0.25f;
    private float activeTimer = 10f, startingActiveTimer;
    private float cooldownTimer = 15f, startingCooldownTimer;
    private State state;
    [SerializeField] private Button abilityButton;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    private bool hasBeenPlayed;
    private bool clicked;
    enum State
    {
        OnCooldown, ReadyToActivate, InProgress, NotPurchased
    }

    private void Awake()
    {
        startingActiveTimer = activeTimer;
        startingCooldownTimer = cooldownTimer;
        clicked = false;
        if ( GameDataHolder.slowAbilityPurchased == false)
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
        Zombie.speed = slowAmount;
        HellBat.speed = slowAmount;
        Bat.speed = slowAmount;
        HatZombie.speed = slowAmount;
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
                cooldownTimer = startingCooldownTimer;
                abilityButton.enabled = true;
                abilityButton.GetComponent<Image>().color = Color.green;
                Zombie.speed = 1.5f;
                Bat.speed = 3.0f;
                HellBat.speed = 4.0f;
                HatZombie.speed = 2.0f;
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
                    Zombie.speed = 1.5f;
                    Bat.speed = 3.0f;
                    HellBat.speed = 4.0f;
                    HatZombie.speed = 2.0f;
                    state = State.OnCooldown;
                    abilityButton.GetComponent<Image>().color = Color.red;
                }
            break;

            case State.OnCooldown:
                cooldownTimer -= Time.deltaTime;
                abilityButton.enabled = false;
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

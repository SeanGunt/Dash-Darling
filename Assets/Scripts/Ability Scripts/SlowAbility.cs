using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlowAbility : MonoBehaviour
{
    private float slowAmount = 0.25f;
    private float slowTimer = 10f;
    private float cooldownTimer = 10f;
    private State state;
    [SerializeField] private Button slowButton;
    private bool clicked;
    [SerializeField] private TextMeshProUGUI slowAbilityText;
    enum State
    {
        OnCooldown, ReadyToActivate, InProgress
    }

    private void Awake()
    {
        state = State.ReadyToActivate;
        clicked = false;
    }
    public void Click()
    {
        clicked = true;
    }

    private void Activate()
    {
        Zombie.speed = slowAmount;
        Flyer.speed = slowAmount;
    }

    private void Update()
    {
        switch (state)
        {
            case State.ReadyToActivate:
                slowAbilityText.text = "Slow Ability Ready";
                if (clicked)
                {
                    Activate();
                    state = State.InProgress;
                }
            break;

            case State.InProgress:
                slowTimer -= Time.deltaTime;
                slowAbilityText.text = "In Progress " + slowTimer.ToString("n0");
                if (slowTimer < 0)
                {
                    Zombie.speed = 1.5f;
                    Flyer.speed = 3.0f;
                    state = State.OnCooldown;
                }
            break;

            case State.OnCooldown:
                cooldownTimer -= Time.deltaTime;
                slowAbilityText.text = "On Cooldown " + cooldownTimer.ToString("n0");
                if (cooldownTimer < 0)
                {
                    slowTimer = 10f;
                    clicked = false;
                    state = State.ReadyToActivate;
                }
            break;
        }

        Debug.Log(state);
    }
}

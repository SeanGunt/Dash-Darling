using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfiniteAmmoAbility : MonoBehaviour
{
    private float activeTimer = 5f;
    private float cooldownTimer = 30f;
    private State state;
    [SerializeField] private Button abilityButton;
    private bool clicked;
    [SerializeField] private TextMeshProUGUI abilityText;
    [SerializeField] private TextMeshProUGUI currentMagazineText;
    private GameObject pc;
    enum State
    {
        OnCooldown, ReadyToActivate, InProgress, NotPurchased
    }

    private void Awake()
    {
        pc = GameObject.FindWithTag("Player");
        clicked = false;
        if ( GameDataHolder.infiniteAmmoPurchased == false)
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
        DataPersistenceManager.instance.SaveGame();
        pc.GetComponent<PlayerController>().currentPistolMagazine = 999;
        currentMagazineText.text = pc.GetComponent<PlayerController>().currentPistolMagazine.ToString();
    }

    private void Update()
    {
        switch (state)
        {
            case State.NotPurchased:

            break;
            case State.ReadyToActivate:
                abilityText.text = "Infinite Ammo Ready";
                abilityButton.enabled = true;
                abilityButton.GetComponent<Image>().color = new Color(0.0f,0.8f,0.0f,0.6f);
                cooldownTimer = 30f;
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
                    pc.GetComponent<PlayerController>().currentPistolMagazine = GameDataHolder.pistolMagazine;
                    currentMagazineText.text = pc.GetComponent<PlayerController>().currentPistolMagazine.ToString();
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

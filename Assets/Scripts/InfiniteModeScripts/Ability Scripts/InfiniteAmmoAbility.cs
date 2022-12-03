using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfiniteAmmoAbility : MonoBehaviour
{
    private float activeTimer = 5f, startingActiveTimer;
    private float cooldownTimer = 30f, startingCooldownTimer;
    private State state;
    [SerializeField] private Button abilityButton;
    private bool clicked;
    [SerializeField] private TextMeshProUGUI currentMagazineText;
    [SerializeField] private Image reticleOutline;
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
        if ( GameDataHolder.infiniteAmmoPurchased == false)
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
        DataPersistenceManager.instance.SaveGame();
        pc.GetComponent<PlayerController>().currentPistolMagazine = 999;
        currentMagazineText.text = pc.GetComponent<PlayerController>().currentPistolMagazine.ToString();
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
                reticleOutline.fillAmount = 1f;
                if (activeTimer < 0)
                {
                    pc.GetComponent<PlayerController>().currentPistolMagazine = GameDataHolder.pistolMagazine;
                    currentMagazineText.text = pc.GetComponent<PlayerController>().currentPistolMagazine.ToString();
                    reticleOutline.fillAmount = 1f;
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

using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Scoreboard sb;
    ScoreboardEntryData newEntry = new ScoreboardEntryData();
    [SerializeField] float movementSpeed, lookSpeed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform gun, ejectionPoint, muzzleFlashPoint;
    [SerializeField] GameObject projectile, gameOverObj, reticle;
    [SerializeField] GameObject[] muzzleFlashes;
    [SerializeField] TextMeshProUGUI pistolMagazineText, timeText, deathTimeText;
    private int randomOption, currentPistolMagazine;
    private float timeTillNextAttack, pistolReloadTime;
    [SerializeField] private float time;
    private SpriteRenderer reticleRenderer;
    [SerializeField] private AudioSource playerSounds, bgmSource;
    [SerializeField] AudioClip fireSound, reloadSound, deathSound;
    private bool soundPlayed = false, timeAdded;
    private Color reticleStartColor = new Color(0,0,1,1);
    private Color reticleMiddleColor = new Color(1,0.5f,0,1);
    private Color reticleEndColor = new Color(1,0,0,1);
    private State state;
    enum State
    {
        alive, dead
    }

    private void Awake()
    {
        timeAdded = false;
        reticleRenderer = reticle.GetComponent<SpriteRenderer>();
        Cursor.visible = false;
        state = State.alive;
        SetPistolStats();
        Time.timeScale = 1;
        soundPlayed = false;
    }
    private void Update()
    {
        switch (state)
        {
            default:
            case State.alive:
                CountTime();
                Reticle();
                Scroll();
                GunLook();
                PistolReload();
                if (Time.time >= timeTillNextAttack && currentPistolMagazine > 0 && reticle.activeInHierarchy == true)
                {
                    PistolShoot();
                    timeTillNextAttack = Time.time + 1f/GameDataHolder.pistolFireRate;
                }
                Debug.Log(reticle.activeInHierarchy);
            break;

            case State.dead:
                PlayerDeath();
            break;
        }
            
    }

    private void Scroll()
    {
        Vector2 direction = Vector2.right;
        rb.transform.position = new Vector2(rb.transform.position.x + direction.x * Time.deltaTime * movementSpeed, rb.transform.position.y);
    }

    private void GunLook()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gun.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(Mathf.Clamp(angle, -35, 90), Vector3.forward);
        gun.rotation = Quaternion.Slerp(gun.rotation, rotation, lookSpeed * Time.deltaTime);
    }

    private void PistolShoot()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Instantiate(projectile, ejectionPoint.position, gun.rotation);

            randomOption = Random.Range(0, muzzleFlashes.Length);
            Instantiate(muzzleFlashes[randomOption], muzzleFlashPoint.position, gun.rotation);

            currentPistolMagazine -= 1;
            pistolMagazineText.text = currentPistolMagazine.ToString();

            PlaySound(fireSound);

        }
    }

    private void PistolReload()
    {
        if (currentPistolMagazine == 0)
        {
            if(!soundPlayed)
            {
                PlaySound(reloadSound);
                soundPlayed = true;
            }
            reticleRenderer.color = Color.Lerp(reticleStartColor, reticleEndColor, pistolReloadTime);
            pistolReloadTime -= Time.deltaTime;
            if (pistolReloadTime <= 0)
            {
                currentPistolMagazine = GameDataHolder.pistolMagazine;
                    pistolMagazineText.text = currentPistolMagazine.ToString();
                        pistolReloadTime = GameDataHolder.pistolReloadTime;
                        soundPlayed = false;   
            }
    
        }
    }

    private void SetPistolStats()
    {
        pistolMagazineText.text = GameDataHolder.pistolMagazine.ToString();
            currentPistolMagazine = GameDataHolder.pistolMagazine;
                pistolReloadTime = GameDataHolder.pistolReloadTime;
    }

    private void PlaySound(AudioClip clip)
    {
        playerSounds.PlayOneShot(clip);
    }

    public void PlayerDeath()
    {

        Debug.Log("Player is Dead");
        gameOverObj.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        deathTimeText.text = time.ToString("n1");
        if (timeAdded == false)
        {
            sb.AddEntry(newEntry = new ScoreboardEntryData(){ entryTime = time});
            timeAdded = true;
        }

        bgmSource.Stop();
        if(!soundPlayed)
        {
            PlaySound(deathSound);
            soundPlayed = true;
        }
        
    }
    private void CountTime()
    {
        time += Time.deltaTime;
        timeText.text = time.ToString("n1");
    }

    private void Reticle()
    {
        Vector2 mouseCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        reticle.transform.position = mouseCursorPos;
        if (currentPistolMagazine == GameDataHolder.pistolMagazine)
        {
            reticleRenderer.color = reticleStartColor;
        }
        else if (currentPistolMagazine == GameDataHolder.pistolMagazine/2)
        {
            reticleRenderer.color = reticleMiddleColor;
        }
        else if (currentPistolMagazine == GameDataHolder.pistolMagazine/4)
        {
            reticleRenderer.color = reticleEndColor;
        }
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            state = State.dead;
        }
    }
}

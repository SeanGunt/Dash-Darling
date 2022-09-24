using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed, lookSpeed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform gun, ejectionPoint, muzzleFlashPoint;
    [SerializeField] GameObject projectile, gameOverObj;
    [SerializeField] GameObject[] muzzleFlashes;
    [SerializeField] TextMeshProUGUI pistolMagazineText;
    private int randomOption, currentPistolMagazine;
    private float timeTillNextAttack, pistolReloadTime;
    [SerializeField] private AudioSource playerSounds;
    [SerializeField] AudioClip fireSound, reloadSound;
    private bool soundPlayed = false;
    private State state;
    enum State
    {
        alive, dead
    }

    private void Awake()
    {
        state = State.alive;
        SetPistolStats();
    }
    private void Update()
    {
        switch (state)
        {
            default:
            case State.alive:
                Scroll();
                GunLook();
                PistolReload();
                if (Time.time >= timeTillNextAttack && currentPistolMagazine > 0)
                {
                    PistolShoot();
                    timeTillNextAttack = Time.time + 1f/GameDataHolder.pistolFireRate;
                }
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
        Quaternion rotation = Quaternion.AngleAxis(Mathf.Clamp(angle, -90, 90), Vector3.forward);
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

    private void PlayerDeath()
    {
        movementSpeed = 0;
        gameOverObj.SetActive(true);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            state = State.dead;
        }
    }
}

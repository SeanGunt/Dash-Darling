using UnityEngine;

public class PlayerController : MonoBehaviour, IDataPersistence
{
    [SerializeField] float movementSpeed, lookSpeed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform gun, ejectionPoint, muzzleFlashPoint;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject[] muzzleFlashes;
    private int randomOption;
    public static int money;
    public static int pistolDamage;

    public void LoadData(GameData data)
    {
        money = data.money;
        pistolDamage = data.pistolDamage;
    }

    public void SaveData(GameData data)
    {
        data.money = money;
        data.pistolDamage = pistolDamage;
    }
    void Update()
    {
        Scroll();
        GunLook();
        GunShoot();
    }

    void Scroll()
    {
        Vector2 direction = Vector2.right;
        rb.transform.position = new Vector2(rb.transform.position.x + direction.x * Time.deltaTime * movementSpeed, rb.transform.position.y);
    }

    void GunLook()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gun.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(Mathf.Clamp(angle, -90, 90), Vector3.forward);
        gun.rotation = Quaternion.Slerp(gun.rotation, rotation, lookSpeed * Time.deltaTime);
    }

    void GunShoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(projectile, ejectionPoint.position, gun.rotation);

            randomOption = Random.Range(0, muzzleFlashes.Length);
            Instantiate(muzzleFlashes[randomOption], muzzleFlashPoint.position, gun.rotation);
        }
    }
}

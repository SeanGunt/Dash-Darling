using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed, lookSpeed, timeTillNextAttack, attackRate;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform gun, ejectionPoint, muzzleFlashPoint;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject[] muzzleFlashes;
    private int randomOption;

    void Update()
    {
        Scroll();
        GunLook();
        if (Time.time >= timeTillNextAttack)
        {
            GunShoot();
            timeTillNextAttack = Time.time + 1f/attackRate;
        }
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
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Instantiate(projectile, ejectionPoint.position, gun.rotation);

            randomOption = Random.Range(0, muzzleFlashes.Length);
            Instantiate(muzzleFlashes[randomOption], muzzleFlashPoint.position, gun.rotation);
        }
    }
}

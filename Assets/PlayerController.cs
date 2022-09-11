using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed, lookSpeed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform gun;

    void Update()
    {
        Scroll();
        GunLook();
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
        Debug.Log(angle);
    }
}

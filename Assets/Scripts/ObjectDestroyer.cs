using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    
    void OnBecameVisible()
    {
        Destroy(this.gameObject, 0.03f);
    }
}

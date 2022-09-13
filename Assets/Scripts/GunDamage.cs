using UnityEngine;
using System;

public class GunDamage : MonoBehaviour
{
    public static GunDamage Instance;
    [NonSerialized] public int PistolDamage = 25;
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this.gameObject);
        }
    }
}

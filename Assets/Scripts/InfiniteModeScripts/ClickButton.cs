using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButton : MonoBehaviour
{
    [SerializeField] private GameObject reticle;

    public void OnHover()
    {
        Cursor.visible = true;
        reticle.SetActive(false);
    }

    public void OnHoverExit()
    {
        Cursor.visible = false;
        reticle.SetActive(true);
    }
}

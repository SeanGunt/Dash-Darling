using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicator : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(LowerOpacity(0.0f, 1.5f));
    }
    IEnumerator LowerOpacity(float aValue, float aTime)
    {
        yield return new WaitForSeconds(0.5f);
        float alpha = spriteRenderer.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Lerp(alpha,aValue,t));
            spriteRenderer.color = newColor;
            yield return null;
        }
    }
}

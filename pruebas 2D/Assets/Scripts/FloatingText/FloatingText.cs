using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [Header("Variables")]
    public float offsetY = 2f;
    public int daño = 0;

    private float max = .1f;
    private float min = -.1f;

    public void destroy()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        var nuevaPosicion = transform.position + Vector3.up * offsetY;
        nuevaPosicion += nuevaPosicion*Random.Range(min, max);

        TextMesh actualText = GetComponent<TextMesh>();
        actualText.text = "+ " + daño;
    }
 
}

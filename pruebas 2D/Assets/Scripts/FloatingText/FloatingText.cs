using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [Header("Variables")]
    public float offsetY = 0f;

    [SerializeField] private TextMesh actualText;

    private float max = .1f;
    private float min = -.1f;
    
    public void destroy()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        posicionAleatoria();
    }

    private void posicionAleatoria()
    {
        var nuevaPosicion = transform.position + new Vector3(0, offsetY, 0);
        nuevaPosicion += nuevaPosicion * Random.Range(min, max);

        transform.position = nuevaPosicion;
    }

    public void cambiarColor(Color color)
    {
        actualText.color = color;
    }

    public void cambiarTexto(string nuevoTexto)
    {
        if (actualText == null) Debug.Log("Nulo");
        actualText.text = nuevoTexto;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveManager : MonoBehaviour
{
    [SerializeField] private float maxVida = 100f;
    [SerializeField] private GameObject textoFlotante;
    private float vida;
    // Start is called before the first frame update
    void Start()
    {
        vida = maxVida;  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void curarVida(float curacion)
    {
        if (vida != maxVida)
        {
            vida = Mathf.Clamp(vida + curacion, 0, maxVida);
            //Mostrar texto "+ " + curación (color verde).
            mostrarTexto(curacion, Color.green);

        }
    }

    public void reducirVida(float damage)
    {

        vida = Mathf.Clamp(vida - damage, 0, maxVida);
        //Mostrar texto "- " + damage (color rojo).
        mostrarTexto(damage, Color.red);

        if (vida == 0)
        {
            gameObject.SetActive(false);
            //Eliminar enemigo o deshabilitar.
        }
    }

    private void mostrarTexto(float num, Color color)
    {
        var texto = Instantiate(textoFlotante, transform.position, Quaternion.identity);
        texto.GetComponent<FloatingText>().cambiarTexto((color == Color.red) ? "- " + num : "+ " + num);
        texto.GetComponent<FloatingText>().cambiarColor(color);
    }
}

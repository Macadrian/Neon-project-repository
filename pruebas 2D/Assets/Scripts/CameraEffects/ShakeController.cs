using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeController : MonoBehaviour
{

    public float velocidadLimite = 0;
    [Space(10)]
    public Rigidbody2D Target;

    private bool terremoto = false;

    // Update is called once per frame
    private void Update()
    {
        if (terremoto && Mathf.Abs(Target.velocity.y) < .05f)
        {
            Debug.Log("Terremoto Ejecutado");
            CameraShakeCM.Temblor(5, 2, .3f);
        }
    }
    private void FixedUpdate()
    {
        ComprobarTerremotoPorVelocidad();
    }

    public void ComprobarTerremotoPorVelocidad()
    {
        if (-Target.velocity.y > velocidadLimite)
        {
            terremoto = true;
        }
        else if (terremoto)
        {
            terremoto = false;
        }
    }
}

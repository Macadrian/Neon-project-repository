using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnhideSecrets : MonoBehaviour {

    [SerializeField] private string Target = "Player";
    [SerializeField][Range(.1f,.5f)] private float AlphaPorciento = .15f;

    private bool transparentar = false;
    private Color color;
    

    private void Start()
    {
        color = gameObject.GetComponent<Tilemap>().color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag(Target)) transparentar = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag(Target)) transparentar = false;
    }

    private void FixedUpdate()
    {
        if (transparentar) VolverTransparente();
        else VolverOpaco();
    }

    private void VolverTransparente()
    {
        if (color.a > 0.05f)
        {
            color.a -= (color.a * AlphaPorciento);
            GetComponent<Tilemap>().color = color;
        }
    }
    private void VolverOpaco()
    {
        if (color.a < 1f)
        {
            color.a += (color.a * AlphaPorciento);
            GetComponent<Tilemap>().color = color;
        }
    }
}

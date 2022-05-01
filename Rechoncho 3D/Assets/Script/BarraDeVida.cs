using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{

    public Image barraDeVida;

    public float vidaActual;

    public float vidaMaxima;

    void Start()
    {
        
    }

    void Update()
    {
        barraDeVida.fillAmount = vidaActual / vidaMaxima;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            recibirdaño();
        }
        if (collision.gameObject.CompareTag("Trap"))
        {
            recibirdaño();
        }
    }

    public void recibirdaño()
    {
        vidaActual = vidaActual - 100;

        if (vidaActual == 0)
        {
            //reiniciar
            Destroy(this.gameObject);
        }
    }
}

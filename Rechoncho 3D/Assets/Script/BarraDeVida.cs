using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{

    public Image barraDeVida;
    private float vidaActual;
    public float vidaMaxima;

    private bool dpsOn;

    void Start()
    {
        vidaActual = vidaMaxima;
    }

    private void Update()
    {
        CheckAlive();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            StartCoroutine(ReduceHp());
        }
        if (collision.gameObject.CompareTag("Trap"))
        {
            StartCoroutine(ReduceHp());
        }
    }

    public void CheckAlive()
    {
        barraDeVida.fillAmount = vidaActual / vidaMaxima;

        if (vidaActual == 0)
        {
            //reiniciar
            Destroy(this.gameObject);
        }
    }

    IEnumerator ReduceHp()
    {
        if (dpsOn) {
            vidaActual = vidaActual - 1;
            dpsOn = false;
        }
        yield return new WaitForSeconds(0.5f);
        dpsOn = true;
    }
}

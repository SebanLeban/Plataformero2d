using System.Collections;
using UnityEngine;

public class ControlBotDispara : MonoBehaviour
{
    public GameObject bala;
    public Camera Camara;
    private GameObject jugador;


    void Start()
    {

        jugador = GameObject.Find("Player");
        StartCoroutine("Esperar");
    }

    private void Update()
    {
        transform.LookAt(jugador.transform);
    }
    IEnumerator Esperar()
    {
        bool a = true;
        while (a)
        {
            yield return new WaitForSeconds(1);
            Ray rayo = Camara.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            GameObject pro;
            pro = Instantiate(bala, rayo.origin, transform.rotation);

            Rigidbody rb = pro.GetComponent<Rigidbody>();
            rb.AddForce(Camara.transform.forward * 15, ForceMode.Impulse);

            Destroy(pro, 3);
        }

    }


}
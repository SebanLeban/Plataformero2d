using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlJugador : MonoBehaviour
{
    public static float rapidezDesplazamiento = 5.0f;

    float movimientoAdelanteAtras;
    float movimientoCostados;

    public Vector3 mov;
    public Animator anim;

    //Movement
    public float MinFuerzaSalto;
    public static float FuerzaDeSalto;
    public static float maxfuerzasalto = 10;
    public float aceleracionFuerzaSalto;

    public Transform GameObject;
    public Rigidbody rb;
    public PhysicMaterial pm;

    //Powerups
    public GameObject jetpack;
    public int jetpackForce;
    public TrailRenderer jetpacktr;

    void Start()
    {
        jetpack.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.GetComponent<Rigidbody>();
        anim.GetComponent<Animator>();
    }

    void Update()
    {
        //Equipar y remover jetpack del player model
        if (AllowActions.equippedJetpack)
        {
            jetpack.SetActive(true);
        }
        else
        {
            jetpack.SetActive(false);
        }
        //Rotate Left
        if (Input.GetKey(KeyCode.Q))
        {
            rb.AddTorque(rb.transform.up * 2, ForceMode.Impulse);
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            StartCoroutine(Stop());
        }

        //Rotate Right
        if (Input.GetKey(KeyCode.E))
        {
            rb.AddTorque(rb.transform.up * -2, ForceMode.Impulse);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            StartCoroutine(Stop());
        }

        //Salto
        if (Input.GetKey(KeyCode.Space) && AllowActions.grounded == true)
        {
            anim.SetBool("chargingBounce", true);
            FuerzaDeSalto += Time.deltaTime * aceleracionFuerzaSalto;
        }
        else
        {
            anim.SetBool("chargingBounce", false);
        }

        if ((Input.GetKeyUp(KeyCode.Space) || FuerzaDeSalto >= maxfuerzasalto) && AllowActions.grounded)
        {
            StartCoroutine(Bounce());
        }


        //Coroutines

        IEnumerator Stop()
        {
            yield return new WaitForSeconds(.02f);
            rb.angularVelocity = new Vector3(0, 0, 0);
        }

        IEnumerator Bounce()
        {
            yield return new WaitForSeconds(.05f);
            anim.SetBool("chargingBounce", false);
            rb.AddForce(rb.transform.up * FuerzaDeSalto, ForceMode.Impulse);
            FuerzaDeSalto = 0f;
        }

    }



    private void FixedUpdate()
    {

        //Mov Jugador

        if (!AllowActions.hasGroundpounded) //Chequea que el user no este en groundpound state
        {
            movimientoCostados = Input.GetAxis("Horizontal") * rapidezDesplazamiento * -1;
            movimientoAdelanteAtras = Input.GetAxis("Vertical") * rapidezDesplazamiento;

            rb.AddTorque(transform.forward * movimientoCostados, ForceMode.Impulse);
            rb.AddTorque(transform.right * movimientoAdelanteAtras, ForceMode.Impulse);

            movimientoAdelanteAtras *= Time.deltaTime;
            movimientoCostados *= Time.deltaTime;

            if (movimientoCostados == 0)
            {
                rb.angularVelocity = new Vector3(0, rb.angularVelocity.y, rb.angularVelocity.z);
            }
            if (movimientoAdelanteAtras == 0)
            {
                rb.angularVelocity = new Vector3(rb.angularVelocity.x, rb.angularVelocity.y, 0);
            }

        }
        //Jetpack
        if (Input.GetKey(KeyCode.Space) && !AllowActions.grounded && AllowActions.equippedJetpack && AllowActions.jetpackFuel >= 0)
        {

            jetpacktr.time = 3f;
            rb.AddForce(rb.transform.up * jetpackForce / 5, ForceMode.Impulse);
            AllowActions.jetpackFuel--;
        }
        jetpacktr.time -= 0.1f;

        //GroundPound

        float Rotation;
        if (GameObject.eulerAngles.y <= 180f)
        {
            Rotation = GameObject.eulerAngles.y;
        }
        else
        {
            Rotation = GameObject.eulerAngles.y - 360f;
        }


        if (Input.GetKey(KeyCode.LeftControl) && !AllowActions.grounded && !AllowActions.hasGroundpounded && AllowActions.equippedCan)
        {
            rb.transform.rotation = Quaternion.Euler(0, Rotation, 0);
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(rb.transform.up * -10, ForceMode.Impulse);
            AllowActions.hasGroundpounded = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("InvertirControles"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                rb.AddTorque(rb.transform.up * 2, ForceMode.Impulse);
            }

            //Rotate Right
            if (Input.GetKey(KeyCode.Q))
            {
                rb.AddTorque(rb.transform.up * -2, ForceMode.Impulse);
            }
        }

        if (collision.gameObject.CompareTag("NormalizarControles"))
        {
            if (Input.GetKey(KeyCode.Q))
            {
                rb.AddTorque(rb.transform.up * 2, ForceMode.Impulse);
            }

            //Rotate Right
            if (Input.GetKey(KeyCode.E))
            {
                rb.AddTorque(rb.transform.up * -2, ForceMode.Impulse);
            }
        }

        if (collision.gameObject.CompareTag("TP"))
        {
            rb.GetComponent<Rigidbody>().position = new Vector3(42f, 0f, -3f);
        }

    }
}



﻿using System.Collections;
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

    public static bool hasGroundpounded = false;
    public float MinFuerzaSalto;
    public static float FuerzaDeSalto;
    public static float maxfuerzasalto = 10;
    public float aceleracionFuerzaSalto;

    public Transform GameObject;
    public Rigidbody rb;
    public PhysicMaterial pm;

    Vector3 jump;


    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        gameObject.GetComponent<Rigidbody>();
        anim.GetComponent<Animator>();
    }

    void Update()
    {
        Debug.Log(FuerzaDeSalto);
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

        if ((Input.GetKeyUp(KeyCode.Space) || FuerzaDeSalto >= maxfuerzasalto) && AllowActions.grounded == true)
        {
            StartCoroutine(Bounce());
        }

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

        //Debug.Log(Rotation);

        if (Input.GetKeyUp(KeyCode.LeftControl) && !AllowActions.grounded && hasGroundpounded == false)
        {
            //rb.transform.rotation = Quaternion.Euler(0, Rotation, 0);
            Debug.Log("control pressed");
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(rb.transform.up * -10, ForceMode.Impulse);
            hasGroundpounded = true;
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
            Debug.Log(jump);
            FuerzaDeSalto = 0f;
        }

    }



    private void FixedUpdate()
    {

        //Mov Jugador
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
}


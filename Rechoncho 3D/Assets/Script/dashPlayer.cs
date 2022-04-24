using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashPlayer : MonoBehaviour
{

    public float dashSpeed;
    bool isDashing;
    Rigidbody rb;

    public GameObject dashEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isDashing = true;
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            Dashing();
        }
    }

    private void Dashing()
    {
        rb.AddForce(transform.forward * dashSpeed, ForceMode.Impulse);
        isDashing = false;

        GameObject effect = Instantiate(dashEffect, Camera.main.transform.position, dashEffect.transform.rotation);
        effect.transform.parent = Camera.main.transform;
        effect.transform.LookAt(transform);
    }
}


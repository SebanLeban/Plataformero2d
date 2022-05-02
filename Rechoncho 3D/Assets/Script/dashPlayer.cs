using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashPlayer : MonoBehaviour
{

    public float dashSpeed;
    public static bool isDashing;
    Rigidbody rb;

    public GameObject dashEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftControl) && AllowActions.equippedDash && !isDashing)
        {
            isDashing = true;
            Dashing();
        }
    }

    private void Dashing()
    {
        rb.AddForce(transform.forward * dashSpeed * -1, ForceMode.Impulse);

        //GameObject effect = Instantiate(dashEffect, Camera.main.transform.position, dashEffect.transform.rotation);
        //effect.transform.parent = Camera.main.transform;
        //effect.transform.LookAt(transform);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowActions : MonoBehaviour
{

    public Animator anim;
    public LayerMask whatIsGround;
    public static bool grounded;
    public float maxSlopeAngle = 35f;
    private bool cancellingGrounded;


    public static bool equippedJetpack = false;
    public static int jetpackFuel = 10;

    public static bool hasGroundpounded = false;
    public static bool equippedCan = false;

    public static bool equippedInverse = false;

    public static bool equippedTP = false;

    public static bool equippedDash = false;

    private void FixedUpdate()
    {
        if (hasGroundpounded && grounded)
        {
            StartCoroutine("GroundPoundBounce");
            ControlJugador.FuerzaDeSalto = ControlJugador.maxfuerzasalto - 1;
        }
    }
    private bool IsFloor(Vector3 v)
    {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }

    private void OnCollisionStay(Collision other)
    {
        //Make sure we are only checking for walkable layers
        int layer = other.gameObject.layer;
        if (whatIsGround != (whatIsGround | (1 << layer))) return;

        //Iterate through every collision in a physics update
        for (int i = 0; i < other.contactCount; i++)
        {
            Vector3 normal = other.contacts[i].normal;
            //FLOOR
            if (IsFloor(normal))
            {
                anim.SetBool("grounded", true);
                grounded = true;
                dashPlayer.isDashing = false;
                cancellingGrounded = false;
                CancelInvoke(nameof(StopGrounded));
                jetpackFuel = 100;
            }
        }

        //Invoke ground/wall cancel, since we can't check normals with CollisionExit
        float delay = 5f;
        if (!cancellingGrounded)
        {
            cancellingGrounded = true;
            Invoke(nameof(StopGrounded), Time.deltaTime * delay);
        }
    }

    IEnumerator GroundPoundBounce()
    {
        hasGroundpounded = false;
        ControlJugador.FuerzaDeSalto = ControlJugador.maxfuerzasalto - 1;
        yield return new WaitForSeconds(0.2f);
        ControlJugador.FuerzaDeSalto = 0;
    }

    private void StopGrounded()
    {
        anim.SetBool("grounded", false);
        grounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jetpack"))
        {
            UnequipAll();
            equippedJetpack = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Can"))
        {
            UnequipAll();
            equippedCan = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("InvertirControles"))
        {
            UnequipAll();
            equippedInverse = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("NormalizarControles"))
        {
            UnequipAll();
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("TP"))
        {
            UnequipAll();
            equippedTP = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Dash"))
        {
            UnequipAll();
            equippedDash = true;
            Destroy(other.gameObject);
        }
    }


    private void UnequipAll()
    {
        equippedJetpack = false;
        equippedCan = false;
        equippedInverse = false;
        equippedTP = false;
        equippedDash = false;

    }
}

